using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomnusLogistic.Model
{
    [Serializable]
   public class CostStat
    {
        public string Date { get; set; }
        public UserInfo ExecutorInfo { get; set; }
        public decimal Money { get; set; }
        public OperDepartment OperDepartment { get; set; }
    }
}
