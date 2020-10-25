using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Nhom16_TH1_18120634.BLL
{
  class Shift
  {
    #region Attribute
    private int id;
    private string name;
    private DateTime startTime;
    private DateTime endTime;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public DateTime StartTime { get => startTime; set => startTime = value; }
    public DateTime EndTime { get => endTime; set => endTime = value; }
    #endregion

    #region Constructor
    public Shift(int id, string name)
    {
      this.id = id;
      this.name = name;
    }
    #endregion

  }
}
