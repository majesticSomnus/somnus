using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SomnusLogistic.Model
{
    [Serializable]
    public class Customer
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 邮寄地址
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 到站
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        /// 收货单位
        /// </summary>
        public string CargoUnit { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string Fphone { get; set; }

        public int UserID { get; set; }

        /// <summary>
        ///  归属作业部ID
        /// </summary>
        [Display(Name = "归属作业部")]
        public int OperDepartmentID { get; set; }

        public OperDepartment OperDepartment { get; set; }
    }
}
