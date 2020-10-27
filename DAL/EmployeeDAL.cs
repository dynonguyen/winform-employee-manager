using _2020_Nhom16_TH1_18120634.DAL;
using _2020_Nhom16_TH1_18120634;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Nhom16_TH1_18120634.DAL
{
  class EmployeeDAL
  {
    #region singleton patern
    private static EmployeeDAL instance;

    internal static EmployeeDAL Instance
    {
      get { if (instance == null) instance = new EmployeeDAL(); return instance; }
      set => instance = value;
    }

    private EmployeeDAL() { }
    #endregion

    #region function
    //lấy danh sách nhân viên
    public DataTable GetEmployeeList()
    {
      try
      {
        string query = @"EXEC [dbo].[Sp_GetEmployeeList]";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }

    //lấy danh sách lịch sử trả lương
    public DataTable GetEmployeePayHistoryList()
    {
      try
      {
        string query = @"EXEC [dbo].[Sp_GetEmployeePayHistoryList]";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }

    //lấy danh sách lịch sử  làm việc
    public DataTable GetEmployeeWorkHistoryList()
    {
      try
      {
        string query = @"EXEC [dbo].[Sp_GetWorkHistoryList]";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }

    //lấy tình trạng còn đi làm của 1 nhân viên
    public string GetCurrentFlag(string employeeId)
    {
      try
      {
        string query = @"EXEC [dbo].[Sp_GetCurrentFlag] @employeeId=" + employeeId;
        return DataProvider.Instance.ExecutScalarSql(query).ToString();
      }
      catch(Exception exc)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }
    
    //Cập nhật lương nhân viên
    public bool UpdateEmployeeSalary(string employeeId, string rate, string updateDate, string payFreq = "1")
    {
      try
      {
        string query = $"EXEC [dbo].[Sp_UpdateRate] @EmployeeId = {employeeId}, @Rate = {rate}, @UpdateDate = \"{updateDate}\", @PayFreq = {payFreq}";
        int result = DataProvider.Instance.ExecuteNoQuerySql(query);
        return result > 0;
      }
      catch (Exception exc)
      {
        throw exc;
      }
    }

    //tìm kiếm nhân viên cho tab Update
    public DataTable SearchEmployeeUpdate(string employeeId)
    {
      try
      {
        string query = $@"EXEC [dbo].[Sp_SearchPayHistoryEmployee] @employeeId = {employeeId}";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }

    //tìm kiếm nhân viên đa điều kiện
    public DataTable SearchEmployee(string departmentId, string shiftId, string gender)
    {
      try
      {
        string query = $@"EXEC [dbo].[Sp_SearchEmployee] @departmentId = {departmentId}, @shiftId = {shiftId}, @gender = {gender}";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }
    
    //lấy danh sách năm thống kê
    public DataTable GetStatisticYearList()
    {
      try
      {
        string query = @"EXEC [dbo].[Sp_GetStatisticYearList]";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase("Warning", "Không thể lấy danh sách năm thống kê");
        throw;
      }
    }
    
    //Kiểm tra 1 nhân viên có tồn tại
    public bool isExistEmployee(string employeeId)
    {
      try
      {
        object isExist = DataProvider.Instance.ExecutScalarSql($@"EXEC [dbo].[Sp_IsExistEmployee] @employeeId = {employeeId}");
        //nhân viên không tồn tại
        if (isExist.ToString() != "1")
        {
          MessageBox.Show($"Không tồn tại nhân viên có id = {employeeId}");
          return false;
        }
        return true;
      }
      catch (Exception)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }

    //Thống kê lương 1 nhân viên trong 1 năm
    public DataTable GetSalaryInYear(string employeeId, string year)
    {
      try
      {
        if (!isExistEmployee(employeeId))
          return null;
        string query = $@"SELECT {year} AS N'Năm', [dbo].[GetSalaryEmployee]({employeeId}, {year}) AS N'Tổng lương'";
        return (DataProvider.Instance.ExecuteQuerySql(query));   
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }

    //Thống kê lương 1 nhân viên trong từng năm
    public DataTable GetSalaryEachForYear(string employeeId)
    {
      try
      {
        if (!isExistEmployee(employeeId))
          return null;
        string query = $@"EXEC [dbo].[Sp_SalaryStatisticEachYear] @employeeId = {employeeId}";
        return (DataProvider.Instance.ExecuteQuerySql(query));
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }
    #endregion
  }
}
