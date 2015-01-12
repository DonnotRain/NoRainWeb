using System.Collections.Generic;
using DefaultConnection;
namespace HuaweiSoftware.HOP.Model
{
    public class StatisticsLeave
    {
        /// <summary>
        /// 上周/月年/数量
        /// </summary>
        public int LastMonthAmount { get; set; }

        /// <summary>
        /// 当周/月年/数量
        /// </summary>
        public int CurrentMonthAmount { get; set; }

        /// <summary>
        /// 前X条请假数据
        /// </summary>
        public List<ATD_Leave> TopItems { get; set; }
    }
}
