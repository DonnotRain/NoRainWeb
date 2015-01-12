using iBangTeam.Business.IBll;
using iBangTeam.Business.IDal;
using WebBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace iBangTeam.Business.Bll
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
