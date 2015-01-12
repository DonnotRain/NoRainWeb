using DefaultConnection;
using System;
using System.Collections.Generic;

namespace HuaweiSoftware.HOP.Model
{
    public class StatisticsTraining
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
        /// 前X条考试安排
        /// </summary>
        public List<TRN_StudyExamArrange> TopItems { get; set; }
    }
}
