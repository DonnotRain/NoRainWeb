using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.HOP.Model.Response.Statistics
{
    public class SaleStatistics
    {
        public long total { get; set; }

        public List<dynamic> rows { get; set; }

        public List<dynamic> footer { get; set; }

    }
}
