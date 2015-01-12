using System.Collections.Generic;
using DefaultConnection;
namespace HuaweiSoftware.HOP.Model
{
    public class StatisticsReturn
    {
        /// <summary>
        /// 上月数量
        /// </summary>
        public int LastMonthAmount { get; set; }

        /// <summary>
        /// 当月数量
        /// </summary>
        public int CurrentMonthAmount { get; set; }

        /// <summary>
        /// 上升百分比
        /// </summary>
        public double UpPercent { get; set; }

        /// <summary>
        /// 前X条退货数据
        /// </summary>
        public List<MKT_Return> TopItems { get; set; }
    }
}
