using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Nhom16_TH1_18120634.BLL
{
  class Employee
  {
    #region Attribute
    private string jobTitle;
    private string loginID;
    private int id;
    private string nationalIDNumber;
    private DateTime birthDate;
    private char maritalStatus;
    private char gender;
    private DateTime hireDate;
    private bool salaryFlag;
    private short vacationHours;
    private short sickLeaveHours;
    private bool currentFlag;
    private DateTime modifiedDate;

    public int Id { get => id; set => id = value; }
    public string NationalIDNumber { get => nationalIDNumber; set => nationalIDNumber = value; }  
    public string LoginID { get => loginID; set => loginID = value; }
    public string JobTitle { get => jobTitle; set => jobTitle = value; }
    public DateTime BirthDate { get => birthDate; set => birthDate = value; }
    public char MaritalStatus { get => maritalStatus; set => maritalStatus = value; }
    public char Gender { get => gender; set => gender = value; }
    public DateTime HireDate { get => hireDate; set => hireDate = value; }
    public bool SalaryFlag { get => salaryFlag; set => salaryFlag = value; }
    public short VacationHours { get => vacationHours; set => vacationHours = value; }
    public short SickLeaveHours { get => sickLeaveHours; set => sickLeaveHours = value; }
    public bool CurrentFlag { get => currentFlag; set => currentFlag = value; }
    public DateTime ModifiedDate { get => modifiedDate; set => modifiedDate = value; }

    #endregion

    #region Contructor Function

    #endregion
  }
}
