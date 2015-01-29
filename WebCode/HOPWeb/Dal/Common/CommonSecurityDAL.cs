using NoRain.Business.IDal;
using NoRain.Business.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoRain.Business.Dal
{
    public class CommonSecurityDAL : BaseDAL, ICommonSecurityDAL
    {
        public CommonSecurityDAL() : base("NoRainRights")
        {
        }
    }
}
