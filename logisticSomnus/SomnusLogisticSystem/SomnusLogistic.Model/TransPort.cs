using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SomnusLogistic.Model
{
    public class TransPort
    {
        /// <summary>
        /// ID
        /// </summary>
        public int TransportID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public Customer Customer { get; set; }


        [Display(Name = "发运地")]
        /// <summary>
        /// 发运地
        /// </summary>
        public string SendPoint { get; set; }

        //[DataType(DataType.DateTime)]
        [Display(Name = "发货时间")]
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime SendDate { get; set; }

         [Display(Name = "吨位")]
        /// <summary>
        /// 吨位
        /// </summary>
        public decimal Tonnage { get; set; }

        [Display(Name = "车皮号")]
        /// <summary>
        /// 车皮号
        /// </summary>
        public string TrainNumber { get; set; }

        [Display(Name = "票号")]
        /// <summary>
        /// 票号
        /// </summary>
        public string Ticket { get; set; }

        [Display(Name = "短盘成本(元/吨)")]
        /// <summary>
        /// 短盘成本(元/吨)
        /// </summary>
        public decimal Cost1 { get; set; }

        [Display(Name = "短盘成本(元/车皮)")]
        /// <summary>
        /// 短盘成本(元/车皮)
        /// </summary>
        public decimal Cost2 { get; set; }

        [Display(Name = "铁路大票费用")]
        /// <summary>
        /// 铁路大票费用
        /// </summary>
        public decimal BigCost { get; set; }

        [Display(Name = "车站费用")]
        /// <summary>
        /// 车站费用
        /// </summary>
        public decimal StationCost { get; set; }

        [Display(Name = "总成本")]
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }

        [Display(Name = "客户支付")]
        /// <summary>
        /// 客户支付
        /// </summary>
        public decimal CustomerPay { get; set; }

        [Display(Name = "利润")]
        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "到达时间")]
        /// <summary>
        /// 到达时间
        /// </summary>
        public DateTime ArriveDate { get; set; }

        [Display(Name = "到达地")]
        /// <summary>
        /// 到达地
        /// </summary>
        public string ArrivePoint { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "货损明细")]
        /// <summary>
        /// 货损明细
        /// </summary>
        public string CargoLossDetail { get; set; }

        [Display(Name = "货损价值")]
        /// <summary>
        /// 货损价值
        /// </summary>
        public decimal CargoLossPrice { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        [Display(Name = "派车单号")]
        /// <summary>
        /// 派车单号
        /// </summary>
        public string CarNumber { get; set; }

        [Display(Name="件数")]
        /// <summary>
        /// 件数
        /// </summary>
        public int Piece { get; set; }

        [Display(Name = "防护费用")]
        /// <summary>
        /// 防护费用
        /// </summary>
        public decimal ProtectCost { get; set; }

        [Display(Name="快递费用")]
        /// <summary>
        /// 快递费用
        /// </summary>
        public decimal ExpressCost { get;set;}
    }
}
