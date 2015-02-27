using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using NoRain.Business.IDal;

namespace NoRain.Business.Bll
{
    public class CommonService : BaseService, ICommonService
    {
        public CommonService()
            : base(DPResolver.Resolver<ICommonDAL>())
        {

        }
    }
}
