using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.HOP.Model.Request
{
    public class AttendenceContract
    {
        public string position { get; set; }

        public int type { get; set; }

        public int fileId { get; set; }

        public string userName { get; set; }

        public string corpCode { get; set; }

        public Guid mark { get; set; }
    }
}
