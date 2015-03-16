using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoRain.Business.IService;
using NoRain.Business.WebBase;
using NoRain.Business.IDal;

namespace NoRain.Business.Service
{
    public class CommonService : BaseService, ICommonService
    {
        public CommonService()
            : base(DPResolver.Resolver<ICommonDao>())
        {

        }
    }
}
