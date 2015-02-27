using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.Model.Response
{
    /// <summary>
    /// DataTables表格插件分页请求返回值
    /// </summary>
    public class DataTablePager<T>
    {
        public int draw { get; set; }

        public long recordsTotal { get; set; }

        public long recordsFiltered { get; set; }

        public List<T> data { get; set; }
    }
}
