using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using HuaweiSoftware.WQT.IDal;

namespace HuaweiSoftware.WQT.Bll
{
    public class CommonBLL : BaseBll, ICommonBLL
    {
        public CommonBLL()
            : base(DPResolver.Resolver<ICommonDAL>())
        {

        }
    }
}
