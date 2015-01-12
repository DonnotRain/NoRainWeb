using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.HOP.Model.Request
{
    public class RequestContract
    {
        public string title { get; set; }
        public string detail { get; set; }
        public string creator { get; set; }
        public long fileID { get; set; }

    }
}
