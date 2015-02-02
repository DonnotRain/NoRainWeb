using MPAPI.IBll;
using MPAPI.IDal;
using MPAPI.WebBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace MPAPI.Bll
{
    public class CommonSecurityBLL :BaseBll, ICommonSecurityBLL
    {
        private IBaseDAL m_baseDAL;

        public CommonSecurityBLL()
            : base(DPResolver.Resolver<ICommonSecurityDAL>())
        {
        }
    }
}
