using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQTRights
{
    public partial class User
    {
        public IEnumerable<Role> Roles { get; set; }

        public string[] RoleIds { get; set; }
    }
}
