using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBangTeam.Business.IBll;
using WebBase;
using iBangTeam.Business.IDal;

namespace iBangTeam.Business.Bll
{
    public class CommonBLL : BaseBll, ICommonBLL
    {
        public CommonBLL()
            : base(DPResolver.Resolver<ICommonDAL>())
        {

        }
    }
}
