using Security_ConnectionString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security_ConnectionString
{
    public partial class Operator
    {
        public IEnumerable<Role> Roles { get; set; }

        public string[] RoleIds { get; set; }
    }
}
