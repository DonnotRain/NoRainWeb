using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoRain.Business.Models
{
    public class JsTreeNode
    {
        public string id { get; set; }
        public string text { get; set; }
        public object state { get; set; }
        public object children { get; set; }
        public bool @checked { get; set; }
        public string icon { get; set; }
    }
} 