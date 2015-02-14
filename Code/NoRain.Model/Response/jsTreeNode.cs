using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoRain.Business.Models
{
    public class jsTreeNode
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public object children { get; set; }
        public bool @checked { get; set; }
        public string icon { get; set; }
    }
} 