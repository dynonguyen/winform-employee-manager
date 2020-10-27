using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Nhom16_TH1_18120634.DAL
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

    public string ServerName { get => serverName; set => serverName = value; }
    public string DbName { get => dbName; set => dbName = value; }

    // đường dẫn kết nối database
    private  string serverName = "";
    private string dbName = "";

    //hàm thực thi query -> return bảng dữ liệu (dùng để đọc)
    public DataTable ExecuteQuerySql(string query)
    {
      string connectionStr = $@"Data Source={ServerName};Initial Catalog={DbName};Integrated Security=True";
      DataTable dataResult = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionStr))
        {
          connection.Open();

          SqlCommand command = new SqlCommand(query, connection);
          SqlDataAdapter sda = new SqlDataAdapter(command);
          sda.Fill(dataResult);

          connection.Close();
        }
      }
      catch(Exception exc)
      {
        throw exc;
      }
      return dataResult;
    }

    //hàm thực thi query -> return số dòng thay đổi (dùng để thay đổi data)
    public int ExecuteNoQuerySql(string query)
    {
      string connectionStr = $@"Data Source={ServerName};Initial Catalog={DbName};Integrated Security=True";
      int nModified = 0;
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionStr))
        {
          connection.Open();

          SqlCommand command = new SqlCommand(query, connection);
          nModified = command.ExecuteNonQuery();

          connection.Close();
        }
      }
      catch(Exception exc)
      {
        throw exc;
      }
      return nModified;
    }

    //hàm thực thi query -> return dòng đầu tiên (thường dùng cho câu select count())
    public object ExecutScalarSql(string query)
    {
      string connectionStr = $@"Data Source={ServerName};Initial Catalog={DbName};Integrated Security=True";
      object dataResult = null;
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionStr))
        {
          connection.Open();

          SqlCommand command = new SqlCommand(query, connection);
          dataResult = command.ExecuteScalar();

          connection.Close();
        }
      }
      catch(Exception ex)
      {
        throw ex;
      }
      return dataResult;
    }
  }
}
