using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2020_Nhom16_TH1_18120634
{
  // chứa hàm tiện ích dùng lại cho chương trình
  class Helper
  {
    private static Helper instance;

    private Helper() { }

    internal static Helper Instance
    {
      get
      {
        if (instance == null)
          instance = new Helper();
        return instance;
      }
      private set => instance = value;
    }

    //show lỗi khi kết nối data base
    public void ShowErrorDatabase(string title = "Error", string message = "Không thể lấy dữ liệu. Thoát chương trình.", bool isError = true)
    {
      var result = MessageBox.Show(message, title, 
        isError ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, isError ? MessageBoxIcon.Error : MessageBoxIcon.Warning);
      if(result == DialogResult.Yes)
      {
        Environment.Exit(-1);
      }
    }

    //show khi gặp lỗi hệ thống
    public void ShowErrorSystem(string title = "Error", string message = "Lỗi hệ thống. Thoát chương trình.")
    {
      var result = MessageBox.Show(message, title,
        MessageBoxButtons.OK, MessageBoxIcon.Error);
      if (result == DialogResult.OK)
      {
        Environment.Exit(-1);
      }
    }


  }
}
