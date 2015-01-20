using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NoRainRights;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class WebAuthorityController : ApiController
    {
        ICommonSecurityBLL m_bll;
        public WebAuthorityController()
        {
            m_bll = DPResolver.Resolver<ICommonSecurityBLL>();
        }


        public object Get(string userName, string pwd)
        {
            pwd=NoRain.Business.CommonToolkit.CommonToolkit.GetMD5Password(pwd);
            var user = m_bll.Find<SysUser>("WHERE Name=@0 AND Password=@1", userName, pwd);
            if (user != null)
            {
               
                return new { Name = user.FullName, Id = user.ID };
            }
            return null;
        }

    }
}
