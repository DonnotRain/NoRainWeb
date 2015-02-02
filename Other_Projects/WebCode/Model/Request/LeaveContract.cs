using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.HOP.Model.Request
{
    public class LeaveContract
    {
        public DateTime beginDate { get; set; }

        public DateTime endDate { get; set; }

        public double duration { get; set; }

        public string reason { get; set; }

    }
}
