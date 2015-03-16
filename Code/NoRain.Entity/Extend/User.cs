using DefaultConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultConnection
{
    public partial class SysUser
    {
        public IEnumerable<Role> Roles { get; set; }

        public string[] RoleIds { get; set; }
    }
}
