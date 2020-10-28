USE [EmployeeMng2012]
GO	

------------------------------
-- B.1) CẬP NHẬT LƯƠNG CHO 1 NHÂN VIÊN --
------------------------------
-- Phải còn đang đi làm
-- RateDateChange >= HireDate
CREATE PROC Sp_UpdateRate
(
    @EmployeeId INT,
    @Rate MONEY,
    @UpdateDate DATETIME,
		@PayFreq INT = 2	-- default = 2 (Hourly)
)
AS
BEGIN
    BEGIN TRAN UpdateRateTrans;
    -- try
    BEGIN TRY
        -- kiểm tra nhân viên có tồn tại
        IF EXISTS
        (
            SELECT 1
            FROM [dbo].[Employee]
            WHERE @EmployeeId = [BusinessEntityID]
        )
        BEGIN
            DECLARE @currentFlag INT = 0;
            DECLARE @hireDate DATE;

            -- kiểm tra nv còn đi làm hay không?
            SELECT @currentFlag = [CurrentFlag],
                   @hireDate = [HireDate]
            FROM [dbo].[Employee]
            WHERE [BusinessEntityID] = @EmployeeId;
            IF (@currentFlag = 0)
                THROW 50001, N'Nhân viên Này đã nghỉ làm', 1;

            -- ngày cập nhật phải lớn hơn ngày thuê
            IF (CAST(@UpdateDate AS DATE) < @hireDate)
                THROW 50001, N'Ngày cập nhật phải lớn hơn ngày thuê', 1;

            -- cập nhật lương = thêm record mới
						INSERT INTO	[dbo].[EmployeePayHistory]
						(
						    [BusinessEntityID],
						    [RateChangeDate],
						    [Rate],
						    [PayFrequency],
						    [ModifiedDate]
						)
						VALUES
						(   @EmployeeId,         
						    @UpdateDate, 
						    @Rate,     
						    @PayFreq,         -- PayFrequency - tinyint
						    GETDATE()  -- ModifiedDate - datetime
						)
            -- commit --
            COMMIT TRAN [UpdateRateTrans];
        END;
        -- nếu không tồn tại thì throw error
        ELSE
            THROW	50001, N'Nhân viên không tồn tại', 1;

    END TRY
    --catch
    BEGIN CATCH
        -- rollback		
        ROLLBACK TRAN [UpdateRateTrans];
				IF(ERROR_NUMBER() >= 50000)
					THROW;
				ELSE
					THROW 50001, N'Cập nhật thất bại.', 1;
    END CATCH;
END;
GO

------------------------------
-- B.2) TÌM KIẾM NHÂN VIÊN THEO PHÒNG BAN, THEO CA LÀM VIỆC, GIỚI TÍNH (TÌM ĐA ĐIỀU KIỆN) --
------------------------------
CREATE PROC Sp_SearchEmployee
(
    @departmentId INT = NULL,
    @shiftId INT = NULL,
    @gender NCHAR(1) = NULL
)
AS
BEGIN
    -- try
    BEGIN TRY
        -- Nếu cả là NULL thì không tìm
        IF (@departmentId IS NULL AND @shiftId IS NULL AND @gender IS NULL)
            RETURN;
				-- Ngược lại
        SELECT DISTINCT
               [EMP].[BusinessEntityID] AS	'Employee ID',
							 [EMP].[JobTitle], [EMP].[HireDate], [EMP].[Gender], 
							 [EDH].[DepartmentID], [EDH].[ShiftID],
							 [EDH].[StartDate], [EDH].[EndDate]
        FROM [dbo].[Employee] AS EMP
            JOIN [dbo].[EmployeeDepartmentHistory] AS EDH
                ON [EMP].[BusinessEntityID] = [EDH].[BusinessEntityID]
        WHERE ( -- tìm kiếm phòng ban nếu có option theo phòng ban
                  @departmentId IS NULL
                  OR
                  (
                      @departmentId IS NOT NULL
                      AND [EDH].[DepartmentID] = @departmentId
                  )
              )
              AND
              ( -- tìm kiếm ca làm nếu có option theo ca làm
                  @shiftId IS NULL
                  OR
                  (
                      @shiftId IS NOT NULL
                      AND [EDH].[ShiftID] = @shiftId
                  )
              )
              AND
              ( -- tìm kiếm giới tính nếu có option theo giới tính
                  @gender IS NULL
                  OR
                  (
                      @gender IS NOT NULL
                      AND [EMP].[Gender] = @gender
                  )
              );
    -- catch
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

------------------------------
-- B.3) KHÔNG CHO PHÉP XOÁ NHÂN VIÊN, KHI NHÂN VIÊN NGHỈ VIỆC CHỈ CẬP NHẬT LẠI ENDDATE. --
------------------------------
CREATE TRIGGER Tg_DeleteEmployee
ON [dbo].[EmployeeDepartmentHistory]
FOR DELETE
AS
BEGIN
		DECLARE @EmployeeDeleted TABLE(id INT);
		INSERT INTO @EmployeeDeleted SELECT [Deleted].[BusinessEntityID] FROM [Deleted]

		-- rollback trạng thái cũ
		ROLLBACK	TRAN;
		PRINT N'Không thể xoá nhân viên, chỉ cập nhật lại EndDate';
	
		-- cập nhật lại endDate
		UPDATE [dbo].[EmployeeDepartmentHistory] SET [EndDate] = CAST(GETDATE() AS	DATE)
		FROM [dbo].[EmployeeDepartmentHistory] JOIN	@EmployeeDeleted ON [BusinessEntityID] = [id]
		WHERE [EndDate] IS NULL;
END
GO

------------------------------
-- B.4) NHÂN VIÊN CHỈ LÀM VIỆC Ở 1 PHÒNG BAN TẠI 1 THỜI ĐIỂM (VÍ DỤ: NHÂN VIÊN A LÀM
-- VIỆC TẠI PHÒNG BAN B, NẾU CHƯA CÓ ENDDATE TẠI PHÒNG BAN B THÌ KHÔNG ĐƯỢC PHÂN
-- CÔNG THÊM TẠI PHÒNG BAN KHÁC) --
------------------------------
CREATE TRIGGER Tg_AddDeparmentForEmployee
ON [dbo].[EmployeeDepartmentHistory]
FOR INSERT, UPDATE
AS
BEGIN
		DECLARE @countInserted INT = 0;
		SELECT @countInserted = COUNT(*) FROM [Inserted]
		DECLARE @count INT = 0;
		-- Đếm số phòng ban của nhân viên vừa insert vào đang làm việc
		SELECT @count = COUNT(*) FROM [dbo].[EmployeeDepartmentHistory] AS EDH JOIN [Inserted]
										ON	[Inserted].[BusinessEntityID] = [EDH].[BusinessEntityID]
		GROUP BY [EDH].[BusinessEntityID], [EDH].[EndDate]
		HAVING [EDH].[EndDate] IS NULL
		
		-- nếu số phòng của n nhân viên đang làm việc > số nhân viên insert vào thì rollback
		IF(@count > @countInserted)
		BEGIN
				PRINT N'Tồn tại Nhân viên đang làm việc tại phòng ban khác !';
		    ROLLBACK TRAN;
		END	
END
GO	

------------------------------
-- B.5) THỐNG KÊ LƯƠNG ĐÃ TRẢ CHO CÁC NHÂN VIÊN THEO TỪNG NĂM --
------------------------------
-- Hàm lấy lương gần nhất trước khi update năm nào đó
-- vd: xét năm 2008 của nv id=5, nv này có { RateChangeDate: 2007-12-15, Rate: 50 } => result = 50 
CREATE FUNCTION [GetRatePreUpdate]
(
    @EmployeeId INT,
    @year INT
)
RETURNS MONEY
AS
BEGIN
    DECLARE @result MONEY;
    SELECT @result = [Rate]
    FROM [dbo].[EmployeePayHistory]
    WHERE [BusinessEntityID] = @EmployeeId
          AND YEAR([RateChangeDate]) < @year
          AND [RateChangeDate]>=ALL
          (
              SELECT [EPH].[RateChangeDate]
              FROM [dbo].[EmployeePayHistory] [EPH]
              WHERE YEAR([EPH].[RateChangeDate]) < @year
                    AND [EPH].[BusinessEntityID] = @EmployeeId
          );
    -- Nếu không có trả về 0
    IF (@result IS NULL)
        SET @result = 0;
    RETURN @result;
END;
GO

-- Hàm lấy ngày update đầu tiên của năm nào đó
CREATE FUNCTION [GetFirstRateChangeDate]
(
    @EmployeeId INT,
    @year INT
)
RETURNS DATETIME
AS
BEGIN
    DECLARE @result DATETIME;
    SELECT @result = [RateChangeDate]
    FROM [dbo].[EmployeePayHistory]
    WHERE [BusinessEntityID] = @EmployeeId
          AND YEAR([RateChangeDate]) = @year
          AND [RateChangeDate]<=ALL
          (
              SELECT [EPH].[RateChangeDate]
              FROM [dbo].[EmployeePayHistory] [EPH]
              WHERE YEAR([EPH].[RateChangeDate]) = @year
                    AND [EPH].[BusinessEntityID] = @EmployeeId
          );
    RETURN @result;
END;
GO

-- Hàm lấy ngày thay đổi lương kế tiếp ở trong năm
CREATE FUNCTION [GetNextRateChangeDate]
(
    @EmployeeId INT,
    @currentRateChangeDate DATETIME
)
RETURNS DATETIME
AS
BEGIN
    DECLARE @result DATETIME;
    -- Ngày thuộc năm @year && Ngày > @current...&& Ngày MIN([Ngày > @current...])
    SELECT @result = MIN([RateChangeDate])
    FROM [dbo].[EmployeePayHistory]
    WHERE [BusinessEntityID] = @EmployeeId
          AND YEAR([RateChangeDate]) = YEAR(@currentRateChangeDate)
          AND [RateChangeDate] > @currentRateChangeDate;

    -- Nếu NULL thì trả về ngày cuối năm, để tính datediff sau này
    IF (@result IS NULL)
    BEGIN
        DECLARE @lastDate CHAR(10) = CAST(YEAR(@currentRateChangeDate) AS CHAR(4)) + '-12-31';
        SET @result = CAST(@lastDate AS DATETIME);
    END;
    RETURN @result;
END;
GO

-- Hàm lấy danh sách lương và số ngày nhận mức lương đó trong năm
-- return [{ Rate: MONEY, DayNumbers: INT }]
CREATE FUNCTION [GetRateAndDayNumbers]
(
    @EmployeeId INT,
    @year INT
)
RETURNS @result TABLE
(
    [Rate] MONEY,
    [RateDayNumbers] INT
)
AS
BEGIN
    -- ngày đầu tiên, cuối năm year
    DECLARE @firstDateOfYear CHAR(10) = CAST(@year AS CHAR(4)) + '-01-01';
    DECLARE @lastDateOfYear CHAR(10) = CAST(@year AS CHAR(4)) + '-12-31';

    -- Lương cũ trước năm @year
    DECLARE @ratePreUpdate MONEY = [dbo].[GetRatePreUpdate](@EmployeeId, @year);

    -- Ngày đầu tiên thay đổi lương trong @year
    DECLARE @firstChangeDate DATETIME = [dbo].[GetFirstRateChangeDate](@EmployeeId, @year);

    -- Nếu NULL thì cả năm không đổi lương (dùng rate cũ của năm) -> RETURN
    IF (@firstChangeDate IS NULL)
    BEGIN
        INSERT INTO @result
        (
            [Rate],
            [RateDayNumbers]
        )
        VALUES
        (@ratePreUpdate, DATEDIFF(DAY, CAST(@firstDateOfYear AS DATETIME), CAST(@lastDateOfYear AS DATETIME)));
        RETURN;
    END;

    -- Ngược lại
    -- vd: year = 2008, Rate(2007-05-01) = 50 -> Rate(2008-02-01) = 70 => { Rate: 50, RateDayNumbers: 31}
    DECLARE @numberDayOldRate INT = DATEDIFF(DAY, @firstDateOfYear, @firstChangeDate);
    INSERT INTO @result
    (
        [Rate],
        [RateDayNumbers]
    )
    VALUES
    (@ratePreUpdate, @numberDayOldRate);

    -- Thêm mức lương kế tiếp trong năm
    INSERT INTO @result
    (
        [Rate],
        [RateDayNumbers]
    )
    SELECT [Rate],
           DATEDIFF(DAY, [RateChangeDate], [dbo].[GetNextRateChangeDate](@EmployeeId, [RateChangeDate]))
    FROM [dbo].[EmployeePayHistory]
    WHERE [BusinessEntityID] = @EmployeeId
          AND YEAR([RateChangeDate]) = @year;

    RETURN;
END;
GO

-- Hàm tính lương nhân viên trong năm nào đó
CREATE FUNCTION [GetSalaryEmployee]
(
    @EmployeeId INT,
    @year INT
)
RETURNS MONEY
AS
BEGIN
    DECLARE @result MONEY = 0;

    -- mảng lưu các việc làm, ca làm trong năm @year
    -- điều kiện: trong năm @year thì nv đó có làm
    DECLARE [workList] CURSOR FOR
    SELECT [ShiftID],
           [StartDate],
           [EndDate]
    FROM [dbo].[EmployeeDepartmentHistory]
    WHERE [BusinessEntityID] = @EmployeeId
          AND YEAR([StartDate]) <= @year
          AND
          (
              [EndDate] IS NULL
              OR YEAR([EndDate]) >= @year
          );

    -- duyệt mảng
    DECLARE @shiftId INT,
            @startDate DATE,
            @endDate DATE;
    OPEN [workList];
    FETCH NEXT FROM [workList]
    INTO @shiftId,
         @startDate,
         @endDate;
    WHILE (@@FETCH_STATUS = 0)
    BEGIN
        -- Lấy số giờ làm trong 1 ca làm
        DECLARE @workHours INT = 0;
        SELECT @workHours = DATEDIFF(HOUR, [StartTime], [EndTime])
        FROM [dbo].[Shift]
        WHERE [ShiftID] = @shiftId;

        -- Nếu số giờ âm (vd: start: 23h, end: 7h -> wordHours = -16) => workHours += 24
        IF (@workHours < 0)
            SET @workHours += 24;

        -- result = sum (mức lương * số ngày làm với mức lương đó * số giờ 1 ngày)
        SELECT @result = @result + SUM([Rate] * [RateDayNumbers] * @workHours)
        FROM [dbo].[GetRateAndDayNumbers](@EmployeeId, @year);

        -- lấy dữ liệu từ cursor
        FETCH NEXT FROM [workList]
        INTO @shiftId,
             @startDate,
             @endDate;
    END;
    CLOSE [workList];
    DEALLOCATE [workList];

    -- reutrn...
    RETURN @result;
END;
GO

-- Store lấy danh sách năm cần thống kê
CREATE PROC Sp_GetStatisticYearList
AS
BEGIN
    DECLARE @startYear INT, @endYear INT;
		SELECT @startYear = MIN(YEAR([StartDate])), @endYear = MAX(YEAR([EndDate]))
		FROM [dbo].[EmployeeDepartmentHistory]
		-- nếu có năm kết thúc là NULL thì lấy năm hiện tại
		IF(EXISTS((SELECT * FROM [dbo].[EmployeeDepartmentHistory] WHERE [EndDate] IS NULL)))
			SET @endYear = YEAR(GETDATE())
		-- return
		DECLARE @result TABLE(year INT);
		DECLARE @i INT = @startYear;
		WHILE(@i <= @endYear)
		BEGIN
		    INSERT INTO @result
		    VALUES(@i);
				SET @i += 1;
		END
		SELECT * FROM @result;
END
GO

-- Store thống kê lương cho mỗi nhân viên theo từng năm
CREATE PROC Sp_SalaryStatisticEachYear(@employeeId INT)
AS
BEGIN
	BEGIN TRY
		DECLARE @result	TABLE(EmployeeID INT, Year INT, SalaryTotal MONEY);
		-- Lấy bảng danh sách các năm
		DECLARE @yearTable TABLE(year INT);
		INSERT INTO @yearTable EXEC [dbo].[Sp_GetStatisticYearList];

		-- thêm danh sách năm vào cursor và duyệt nó
		DECLARE yearList CURSOR FOR SELECT * FROM @yearTable;
		DECLARE @year INT;
		OPEN [yearList];
		FETCH NEXT FROM [yearList] INTO @year;

		-- thông kê cho 1 nhân viên
		WHILE(@@FETCH_STATUS = 0)
		BEGIN
			DECLARE @salaryTotal MONEY = [dbo].[GetSalaryEmployee](@employeeId, @year);
			IF(@salaryTotal > 0)
				INSERT INTO @result
				(
						[EmployeeID],
						[Year],
						[SalaryTotal]
				)
				VALUES
				(   @employeeId,
						@year,
						@salaryTotal
				)
			FETCH NEXT FROM [yearList] INTO @year;
		END
		-- thống kê cho tất cả nhân viên
		
		CLOSE [yearList];
		DEALLOCATE [yearList];
		SELECT [Year] N'Năm', [SalaryTotal] N'Tổng Lương' FROM @result;
	END TRY
	BEGIN CATCH
		THROW 50001, N'Lỗi Hệ Thống', 1;
	END CATCH
END
GO

------------------------------
-- B.6) THỐNG KÊ LƯƠNG THEO TỪNG PHÒNG BAN TRONG 1 NĂM NÀO ĐÓ --
------------------------------
CREATE PROC Sp_SalaryStatisticEachDepartment(@year INT)
AS
BEGIN
		DECLARE @result TABLE(DepartmentID INT, SalaryTotal MONEY);
		-- danh sách phòng ban
    DECLARE departmentList CURSOR FOR SELECT [DepartmentID] FROM [dbo].[Department];

		OPEN [departmentList]
		DECLARE @departmentId INT;
		FETCH NEXT FROM [departmentList] INTO @departmentId;
		WHILE(@@FETCH_STATUS = 0)
		BEGIN
				DECLARE @salaryTotal MONEY = 0;

				SELECT @salaryTotal += [dbo].[GetSalaryEmployee]([BusinessEntityID], @year)
				FROM [dbo].[EmployeeDepartmentHistory]
				WHERE [DepartmentID] = @departmentId;	

		    INSERT INTO @result
		    (
		        [DepartmentID],
		        [SalaryTotal]
		    )
		    VALUES
		    (  
						@departmentId,
		        @salaryTotal
		    )

				FETCH NEXT FROM [departmentList] INTO @departmentId;
		END
		CLOSE [departmentList]
		DEALLOCATE [departmentList]

		SELECT * FROM @result
END
GO	

----------------------------------------
-- STORE PROCEDURE FOR EMPLOYEE MANAGER APP --
----------------------------------------
-- store kiểm tra 1 nhân viên có tồn tại
CREATE PROC Sp_IsExistEmployee(@employeeId INT)
AS
BEGIN
    IF EXISTS(SELECT 1 FROM [dbo].[Employee] WHERE [BusinessEntityID] = @employeeId)
			SELECT 1;
		ELSE
			SELECT 0;
END;
GO	

-- Store Lấy ds nhân viên
CREATE PROC Sp_GetEmployeeList
AS
BEGIN
    SELECT [BusinessEntityID] AS	'ID', [NationalIDNumber],
		[JobTitle], [BirthDate], [MaritalStatus],
		[Gender], [HireDate], [SalariedFlag],
		[VacationHours], [CurrentFlag], [ModifiedDate]
		FROM [dbo].[Employee]
END
GO

-- Store lấy ds phòng ban
CREATE PROC Sp_GetDeparmentList
AS
BEGIN
    SELECT * FROM [dbo].[Department]
END
GO

-- Store lấy ds lịch sử lương
CREATE PROC Sp_GetEmployeePayHistoryList
AS
BEGIN
    SELECT [BusinessEntityID] AS 'ID',[RateChangeDate] AS	'Effective Date', [Rate] AS 'Salary',
    [PayFrequency] 'Pay Frequency'
		FROM [dbo].[EmployeePayHistory]
END
GO

-- Store lấy lịch sử lương của 1 nhân viên
CREATE PROC Sp_SearchPayHistoryEmployee(@employeeId INT)
AS
BEGIN
    SELECT [BusinessEntityID] AS 'ID',[RateChangeDate] AS	'Effective Date', [Rate] AS 'Salary',
    [PayFrequency] 'Pay Frequency'
		FROM [dbo].[EmployeePayHistory]
		WHERE [BusinessEntityID] = @employeeId
END
GO

-- Store lấy ds ca làm việc
CREATE PROC Sp_GetShiftList
AS
BEGIN
    SELECT * FROM [dbo].[Shift]
END
GO

-- Store lấy ds lịch sử làm việc
CREATE PROC Sp_GetWorkHistoryList
AS
BEGIN
    SELECT * FROM [dbo].[EmployeeDepartmentHistory]
END
GO	

-- Store lấy tình trạng còn đi làm của 1 nhân viên
CREATE PROC Sp_GetCurrentFlag(@employeeId INT)
AS
BEGIN
    SELECT [CurrentFlag] FROM [dbo].[Employee] WHERE	[BusinessEntityID] = @employeeId
END
GO

----------------------------------------
---------------- TEST ------------------
----------------------------------------
-- b.1)
EXEC [dbo].[Sp_UpdateRate] @EmployeeId = 1,                     -- int
                           @Rate = 70,                        -- money
                           @UpdateDate = '2010-10-27 11:45:48', -- datetime
                           @PayFreq = 2                         -- int
GO
-- b.2)
EXEC [dbo].[Sp_SearchEmployee] @departmentId = 1, -- int
                               @shiftId = NULL,      -- int
                               @gender = N'M'      -- nchar(1)
GO
-- b.3)
DELETE FROM [dbo].[EmployeeDepartmentHistory] WHERE [BusinessEntityID] = 1
GO	
-- b.4)
INSERT INTO [dbo].[EmployeeDepartmentHistory]
(
    [BusinessEntityID],
    [DepartmentID],
    [ShiftID],
    [StartDate],
    [EndDate],
    [ModifiedDate]
)
VALUES
(   10,         -- BusinessEntityID - int
    6,         -- DepartmentID - smallint
    1,         -- ShiftID - tinyint
    GETDATE(), -- StartDate - date
    NULL, -- EndDate - date
    GETDATE()  -- ModifiedDate - datetime
)
GO	
-- b.5)
EXEC [dbo].[Sp_SalaryStatisticEachYear] @employeeId = 1 -- int
GO	
-- b.6)
EXEC [dbo].[Sp_SalaryStatisticEachDepartment] @year = 2009 -- int
GO