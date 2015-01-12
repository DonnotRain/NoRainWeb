using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPAPI.IBll;
using MPAPI.WebBase;
using MPAPI.IDal;

namespace MPAPI.Bll
{
    public class CommonBLL : BaseBll, ICommonBLL
    {
        public CommonBLL()
            : base(DPResolver.Resolver<ICommonDAL>())
        {

        }
    }
}
