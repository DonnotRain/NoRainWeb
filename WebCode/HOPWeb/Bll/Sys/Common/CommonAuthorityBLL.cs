using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.IDal;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace HuaweiSoftware.WQT.Bll
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
