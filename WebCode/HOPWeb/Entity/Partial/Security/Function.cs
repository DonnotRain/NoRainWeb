using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQTRights
{
    public partial class Function
    {
        public IEnumerable<Role> Roles { get; set; }

        public string[] RoleIds { get; set; }

        //树的一些扩展属性
        public IEnumerable<Function> children { get; set; }

        public string iconCls { get; set; }
    }
}
