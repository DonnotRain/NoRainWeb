using System;
using System.Collections.Generic;
namespace HuaweiSoftware.HOP.Model
{
    public class StatisticsSale
    {
        /// <summary>
        /// 上月销售额
        /// </summary>
        public decimal LastMonthAmount { get; set; }

        /// <summary>
        /// 当月的销售额
        /// </summary>
        public decimal CurrentMonthAmount { get; set; }

        /// <summary>
        /// 上升百分比
        /// </summary>
        public double UpPercent { get; set; }


        /// <summary>
        /// 上月销售笔数
        /// </summary>
        public decimal LastMonthTime { get; set; }

        /// <summary>
        /// 当月的销售笔数
        /// </summary>
        public decimal CurrentMonthTime { get; set; }

        /// <summary>
        /// 上升百分比
        /// </summary>
        public double UpPercentTime { get; set; }

        /// <summary>
        /// 排名前X的销售员及其销售额
        /// </summary>
        public Dictionary<string, decimal> TopSalers { get; set; }

        /// <summary>
        /// 近X个月来各商品的销售金额情况
        /// </summary>
        public Dictionary<string, Dictionary<int, decimal>> MonthSales { get; set; }


    }
}
