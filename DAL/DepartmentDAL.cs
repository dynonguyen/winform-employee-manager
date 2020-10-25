using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Nhom16_TH1_18120634.DAL
{
  class DepartmentDAL
  {
    #region singleton patern
    private static DepartmentDAL instance;

    internal static DepartmentDAL Instance
    {
      get { if (instance == null) instance = new DepartmentDAL(); return instance; }
      set => instance = value;
    }

    private DepartmentDAL() { }
    #endregion
    #region function
    public DataTable GetDepartmentList()
    {
      try
      {
        string query = @"EXEC[dbo].[Sp_GetDeparmentList]";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }
    #endregion
  }
}
