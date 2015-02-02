using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPAPI.WebBase.Models
{
    public class EasyuiTreeNode
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public object children { get; set; }
        public bool @checked { get; set; }
        public string iconCls { get; set; }
    }
}