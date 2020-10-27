using _2020_Nhom16_TH1_18120634.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Nhom16_TH1_18120634.DAL
{
  class ShiftDAL
  {
    #region singleton patern
    private static ShiftDAL instance;

    internal static ShiftDAL Instance
    {
      get { if (instance == null) instance = new ShiftDAL(); return instance; }
      set => instance = value;
    }

    private ShiftDAL() { }
    #endregion

    #region function
    public DataTable GetShiftList()
    {
      try
      {
        string query = @"EXEC[dbo].[Sp_GetShiftList]";
        return DataProvider.Instance.ExecuteQuerySql(query);
      }
      catch
      {
        Helper.Instance.ShowErrorDatabase();
        throw;
      }
    }

    public List<Shift> GetShiftTypeList()
    {
      try
      {
        List<Shift> result = new List<Shift>();

        string query = @"SELECT[ShiftID], [Name] FROM[dbo].[Shift]";
        DataTable queryRes = DataProvider.Instance.ExecuteQuerySql(query);
        foreach (DataRow item in queryRes.Rows)
        {
          Shift shift = new Shift(Convert.ToInt32(item["ShiftID"]), item["Name"].ToString());
          result.Add(shift);
        }
        result.Add(new Shift(result.Count + 1, "All"));
        return result;
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
