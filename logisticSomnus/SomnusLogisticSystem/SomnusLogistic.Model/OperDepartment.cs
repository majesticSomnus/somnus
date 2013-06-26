using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomnusLogistic.Model
{
    /// <summary>
    /// 作业部
    /// </summary>
    [Serializable]
    public class OperDepartment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
    }
}
