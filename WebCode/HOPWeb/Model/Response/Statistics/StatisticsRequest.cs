using DefaultConnection;
using System;
using System.Collections.Generic;

namespace HuaweiSoftware.HOP.Model
{
    public class StatisticsRequest
    {
        /// <summary>
        /// 上个月的店面需求数量
        /// </summary>
        public int LastMonthAmount { get; set; }

        /// <summary>
        /// 当月的店面需求数量
        /// </summary>
        public int CurrentMonthAmount { get; set; }

        /// <summary>
        /// 需求的上升百分比
        /// </summary>
        public double UpPercent { get; set; }

        /// <summary>
        /// 前X条退货数据
        /// </summary>
        public List<MKT_Request> TopItems { get; set; }
    }
}
