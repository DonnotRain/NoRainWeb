using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.HOP.Model.Request
{
    public class ReturnContract
    {

        public string code { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public string reason { get; set; }
        public int type { get; set; }
        public string barcode { get; set; }

        public string creator { get; set; }
    }
}
