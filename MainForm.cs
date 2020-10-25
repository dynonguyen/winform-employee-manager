using _2020_Nhom16_TH1_18120634.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2020_Nhom16_TH1_18120634
{
  // Các loại danh sách
  struct ListType
  {
    private int type;
    private string value;

    public int Type { get => type; set => type = value; }
    public string Value { get => value; set => this.value = value; }
  }

  public partial class MainForm : Form
  {
    BindingSource payHistoryUpdateBS = new BindingSource();

    //danh sách hiện tại đang xem
    private int currentListType = -1;

    //main
    public MainForm()
    {
      try
      {
        InitializeComponent();  
        Loading();
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorSystem();
        throw exc;
      }
    }

    #region hàm xử lý
    //Load data
    void Loading()
    {
      LoadTypeList();
      dgvUpdateList.DataSource = payHistoryUpdateBS;
      payHistoryUpdateBS.DataSource = EmployeeDAL.Instance.GetEmployeePayHistoryList();
      AddBindingUpdate();
      LoadShiftTypeList();
    }

    // Load các loại danh sách vào trong comboBox
    void LoadTypeList()
    {
      List<ListType> typeList = new List<ListType>
      {
        new ListType(){ Type = 0, Value = "Nhân viên"},
        new ListType(){ Type = 1, Value = "Phòng ban"},
        new ListType(){ Type = 2, Value = "Ca làm"},
        new ListType(){ Type = 3, Value = "Lịch sử lương"},
        new ListType(){ Type = 4, Value = "Lịch sử công tác"},
      };
      cbTypeList.DataSource = typeList;
      cbTypeList.DisplayMember = "Value";
    }

    //hàm hiển thị danh sách
    void ShowList(int listType)
    {
      switch (listType)
      {
        case 0:
          dgvShowList.DataSource = EmployeeDAL.Instance.GetEmployeeList();
          break;
        case 1:
          dgvShowList.DataSource = DepartmentDAL.Instance.GetDepartmentList();
          break;
        case 2:
          dgvShowList.DataSource = ShiftDAL.Instance.GetShiftList();
          break;
        case 3:
          dgvShowList.DataSource = EmployeeDAL.Instance.GetEmployeePayHistoryList();
          break;
        case 4:
          dgvShowList.DataSource = EmployeeDAL.Instance.GetEmployeeWorkHistoryList();
          break;
        default:
          return;
      }
    }

    // hàm add binding
    void AddBindingUpdate()
    {
      txbIdUpdate.DataBindings.Add(new Binding("Text", dgvUpdateList.DataSource, "ID", true, DataSourceUpdateMode.Never));
      txbSalaryUpdate.DataBindings.Add(new Binding("Text", dgvUpdateList.DataSource, "Salary", true, DataSourceUpdateMode.Never));
      dtpEffectiveDateUpdate.DataBindings.Add(new Binding("Value", dgvUpdateList.DataSource, "Effective Date", true, DataSourceUpdateMode.Never));
      txbPayFreqUpdate.DataBindings.Add(new Binding("Text", dgvUpdateList.DataSource, "Pay Frequency", true, DataSourceUpdateMode.Never));
    }

    //lấy danh sách các loại ca làm cho cb search
    void LoadShiftTypeList()
    {
      cbShiftSearch.DataSource = ShiftDAL.Instance.GetShiftTypeList();
      cbShiftSearch.DisplayMember = "Name";
    }
    #endregion

    #region event
    //sự kiện xem 1 danh sách
    private void cbTypeList_SelectedValueChanged(object sender, EventArgs e)
    {
      int type = ((ListType)(cbTypeList.SelectedValue)).Type;
      if (this.currentListType != type)
      {
        this.ShowList(type);
        this.currentListType = type;
      }
    }

    #endregion
    // refresh list
    private void btnRefreshList_Click(object sender, EventArgs e)
    {
      int currentListType = cbTypeList.SelectedIndex;
      payHistoryUpdateBS.DataSource = EmployeeDAL.Instance.GetEmployeePayHistoryList();
    }

    // hiển thị current flag của 1 nhân viên
    private void txbIdUpdate_TextChanged(object sender, EventArgs e)
    {
      string currentFlag = EmployeeDAL.Instance.GetCurrentFlag(txbIdUpdate.Text);
      if (currentFlag == "True" || currentFlag == "1")
      {
        txbCurrentFlagUpdate.Text = "Đang làm việc";
      }
      else
      {
        txbCurrentFlagUpdate.Text = "Đã nghỉ việc";
      }
    }

    //update lương nhân viên
    private void btnUpdate_Click(object sender, EventArgs e)
    {
      try
      {
        string employeeId = txbIdUpdate.Text;
        string rate = txbSalaryUpdate.Text;
        string updateDate = dtpEffectiveDateUpdate.Value.ToString();
        string payFreq = txbPayFreqUpdate.Text;
        if (rate == "")
        {
          MessageBox.Show("Nhập mức lương!", "Cảnh báo");
          return;
        }
        if (payFreq == "")
        {
          MessageBox.Show("Nhập hình thức nhận lương!", "Cảnh báo");
          return;
        }
        bool result = EmployeeDAL.Instance.UpdateEmployeeSalary(employeeId, rate, updateDate, payFreq);
        if (result)
        {
          MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
          //reload
          payHistoryUpdateBS.DataSource = EmployeeDAL.Instance.GetEmployeePayHistoryList();
        }
      }
      catch (Exception exc)
      {
        Helper.Instance.ShowErrorDatabase("Warning", exc.Message, false);
      } 
    }

    //tìm kiếm nhân viên để update
    private void btnSearchUpdate_Click(object sender, EventArgs e)
    {
      string employeeId = txbUpdateSearch.Text;
      int outNum;
      if(Int32.TryParse(employeeId, out outNum))
      {
        DataTable result = EmployeeDAL.Instance.SearchEmployeeUpdate(employeeId);
        if(result.Rows.Count > 0)
        {
          payHistoryUpdateBS.DataSource = result;
        }
        else
        {
          MessageBox.Show($"Không tìm thấy nhân viên có id = {employeeId}", "Thông báo");
          return;
        }
      }
      else
      {
        MessageBox.Show("Id Nhân viên là một số");
        txbUpdateSearch.Text = "";
      }
    }

    //tìm kiếm nhân viên đa điều kiện
    private void btnTabSearch_Click(object sender, EventArgs e)
    {
      string deparmentId = txbDepartmentSearch.Text != "" ? txbDepartmentSearch.Text : "NULL";
      string shiftId = (cbShiftSearch.SelectedIndex + 1).ToString();
      string gender = rbGenderBoth.Checked ? "NULL" : (rbGenderF.Checked ? "F" : "M");
      DataTable result = EmployeeDAL.Instance.SearchEmployee(deparmentId, shiftId, gender);

      //hiển thị kết quả tìm kiếm
      string genderResultDisplay = "";
      switch (gender)
      {
        case "F":
          genderResultDisplay = "Nữ";
          break;
        case "M":
          genderResultDisplay = "Nam";
          break;
        default:
          genderResultDisplay = "Cả hai";
          break;
      }
      string shiftResultDisplay = "";
      switch (shiftId)
      {
        case "1":
          shiftResultDisplay = "Ban ngày";
          break;
        case "2":
          shiftResultDisplay = "Ban đêm";
          break;
        default:
          shiftResultDisplay = "Chiều tối";
          break;
      }

      int nRowResult = result.Rows.Count;
      lbSearchResult.Text = $"Có {nRowResult} kết quả cho:";
      lbSearchInfo.Text = $"Nhân viên có Phòng ban Id =\"{deparmentId}\", Ca làm là \"{shiftResultDisplay}\", Giới tính là \"{genderResultDisplay}\"";
      dgvSearchResult.DataSource = result;
    }

    //validation textbox
    private void txbDepartmentSearch_Validating(object sender, CancelEventArgs e)
    {
      int outNum;
      if(txbDepartmentSearch.Text != "" && !Int32.TryParse(txbDepartmentSearch.Text, out outNum))
      {
        MessageBox.Show("ID Phòng ban là một số", "Thông báo");
        txbDepartmentSearch.Text = "";
      }
    }

    private void txbSalaryUpdate_Validating(object sender, CancelEventArgs e)
    {
      double outNum;
      if (txbSalaryUpdate.Text != "" && !Double.TryParse(txbSalaryUpdate.Text, out outNum))
      {
        MessageBox.Show("Lương là một con số");
        txbSalaryUpdate.Text = "";
      }
    }

    private void txbPayFreqUpdate_Validating(object sender, CancelEventArgs e)
    {
      string payFreq = txbPayFreqUpdate.Text;
      if (payFreq == "1" || payFreq == "2")
        return;
      MessageBox.Show("Hình thức nhận không hợp lệ!", "Cảnh báo");
      txbPayFreqUpdate.Text = "1";
    }
  }
}
