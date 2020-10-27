using _2020_Nhom16_TH1_18120634.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2020_Nhom16_TH1_18120634
{
  public partial class FormConnectDB : Form
  {
    public FormConnectDB()
    {
      InitializeComponent();
    }

    //connect
    private void btnConnect_Click(object sender, EventArgs e)
    {
      string serverName = tbxServerName.Text;
      if (serverName == "")
      {
        MessageBox.Show("Nhập Server Name", "Thông báo");
        return;
      }
      string dbName = txbDBName.Text;
      if (dbName == "")
      {
        MessageBox.Show("Nhập Database Name", "Thông báo");
        return;
      }

      string connectionStr = $@"Data Source={serverName};Initial Catalog={dbName};Integrated Security=True";
      using (SqlConnection connection = new SqlConnection(connectionStr))
      {
        try
        {
          this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
          connection.Open();
          SqlCommand command = new SqlCommand("SELECT 1", connection);
          object resultQuery = command.ExecuteScalar();
          if (resultQuery.ToString() != "1")
            throw(new Exception());
          connection.Close();
        }
        catch (Exception exc)
        {
          this.Cursor = System.Windows.Forms.Cursors.Default;
          MessageBox.Show("Kết Nối Thất Bại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
     
      //gán kết nối vào data provider
      DataProvider.Instance.ServerName = serverName;
      DataProvider.Instance.DbName = dbName;
      MainForm f = new MainForm();
      //ẩn form connect, show mainform. Khi đóng mainform thì kết thúc chương trình
      this.Hide();
      f.ShowDialog();
      Application.Exit();
    }
    //exit
    private void btnExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }
  }  
 }
