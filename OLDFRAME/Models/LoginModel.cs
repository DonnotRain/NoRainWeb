using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAPI.Models
{
    public class LoginModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string IsRemembered { get; set; }

        public string Url { get; set; }
    }
}
