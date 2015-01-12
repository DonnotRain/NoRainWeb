using System;
using System.Collections.Generic;
namespace HuaweiSoftware.HOP.Model
{
    /// <summary>
    /// 各商品的销售额变化情况
    /// </summary>
    public class StatisticsChange
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string TimeOfDay { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 总销售额
        /// </summary>
        public string Total { get; set; }
    }
}
