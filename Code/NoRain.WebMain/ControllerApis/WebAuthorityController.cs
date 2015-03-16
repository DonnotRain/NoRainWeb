using NoRain.Business.IService;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MainWeb.Filters;
using DefaultConnection;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class WebAuthorityController : ApiController
    {
        ICommonService m_Service;
        public WebAuthorityController()
        {
            m_Service = DPResolver.Resolver<ICommonService>();
        }


        public object Get(string userName, string pwd)
        {
            pwd=NoRain.Business.CommonToolkit.CommonToolkit.GetMD5Password(pwd);
            var user = m_Service.Find<SysUser>("WHERE Name=@0 AND Password=@1", userName, pwd);
            if (user != null)
            {
               
                return new { Name = user.FullName, Id = user.ID };
            }
            return null;
        }

    }
}
