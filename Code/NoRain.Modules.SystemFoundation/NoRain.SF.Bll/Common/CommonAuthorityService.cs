using NoRain.Business.IBll;
using NoRain.Business.IDal;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace NoRain.Business.Bll
{
    public class CommonSecurityBLL :BaseService, ICommonSecurityBLL
    {
        private IBaseDao m_baseDAL;

        public CommonSecurityBLL()
            : base(DPResolver.Resolver<ICommonSecurityDao>())
        {
        }
    }
}
