using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Nhom16_TH1_18120634.DataAccessLayer
{
  class DataProvider
  {
    //singleton patern, đảm bảo chỉ có 1 connection được khởi tạo
    private static DataProvider instance;

    private DataProvider() { }

    internal static DataProvider Instance 
    { 
      get 
      {
        if (instance == null)
          instance = new DataProvider();
        return instance;
      }
      private set => instance = value;
    }

    // đường dẫn kết nối database
    private const string serverName = ".";
    private const string dbName = "EmployeeMng2012";
    private string connectionStr = $@"Data Source={serverName};Initial Catalog={dbName};Integrated Security=True";

    //hàm thực thi query -> return bảng dữ liệu (dùng để đọc)
    public DataTable ExecuteQuerySql(string query)
    {
      DataTable dataResult = new DataTable();
      using (SqlConnection connection = new SqlConnection(this.connectionStr))
      {
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        SqlDataAdapter sda = new SqlDataAdapter(command);
        sda.Fill(dataResult);

        connection.Close();
      }
      return dataResult;
    }

    //hàm thực thi query -> return số dòng thay đổi (dùng để thay đổi data)
    public int ExecuteNoQuerySql(string query)
    {
      int nModified = 0;
      using (SqlConnection connection = new SqlConnection(this.connectionStr))
      {
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        nModified = command.ExecuteNonQuery();

        connection.Close();
      }
      return nModified;
    }

    //hàm thực thi query -> return dòng đầu tiên (thường dùng cho câu select count())
    public object ExecutScalarSql(string query)
    {
      object dataResult = null;
      using (SqlConnection connection = new SqlConnection(this.connectionStr))
      {
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        dataResult = command.ExecuteScalar();

        connection.Close();
      }
      return dataResult;
    }
  }
}
