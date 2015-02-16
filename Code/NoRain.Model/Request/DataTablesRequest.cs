using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.Model.Request
{
    /// <summary>
    /// 用于datatables插件请求数据时，获取请求参数
    /// </summary>
    public class DataTablesRequest
    {
        /// <summary>
        /// 初始化默认值
        /// </summary>
        public DataTablesRequest()
        {
            start = 1;
            length = 10;
        }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }

        public string action { get; set; }

        /// <summary>
        /// 动态的请求列参数
        /// </summary>
        public dynamic[] columns { get; set; }

        /// <summary>
        /// 动态的请求排序参数
        /// </summary>
        public dynamic[] order { get; set; }

        public dynamic search { get; set;  }
    }
}
