using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SomnusLogistic.Model
{
    [Serializable]
    public class Cost
    {
        /// <summary>
        /// 花费ID
        /// </summary>
        public int CostID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        public UserInfo UserInfo { get; set; }
        /// <summary>
        /// 批准人ID
        /// </summary>
        public int RatifyID { get; set; }
        public UserInfo RatifyPersonInfo { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name="日期")]
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        [Display(Name="项目")]
        public string Project { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "详细内容")]
        public string Detail { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        public decimal Money { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 执行人ID
        /// </summary>
        [Display(Name = "执行人")]
        public int ExecutorID { get; set; }

        public UserInfo ExecutorInfo { get; set; }

        /// <summary>
        /// 是否有发票
        /// </summary>
        [Display(Name = "是否有发票")]
        public bool IsHasInvoice { get; set; }

        public int HasInvoice { get; set; }

        /// <summary>
        ///  归属作业部ID
        /// </summary>
        [Display(Name = "归属作业部")]
        public int OperDepartmentID { get; set; }

        public OperDepartment OperDepartment { get; set; }
    }
}
