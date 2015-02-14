using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoRain.Business.Models
{
    public class ApiTreeNode
    {
        public string id { get; set; } // 
        public string pId { get; set; } // 
        public string name { get; set; } // 
        public bool open { get; set; } // 是否展开
        public bool nocheck { get; set; } // 能否勾选
        public bool isOrg { get; set; } // 是否是目录
        public int order { get; set; } // 同级下的序号
        public string remark { get; set; } // 其他信息
        public bool @checked { get; set; } // 是否勾选

        public string iconSkin { get;set;}
    }
}