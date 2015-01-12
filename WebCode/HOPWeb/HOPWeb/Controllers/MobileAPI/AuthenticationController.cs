using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.MobileAPI
{
    //[MobileValidate()]
    [MobileServiceMark]
    public class AuthenticationController : ApiController
    {
        IUserBll m_Bll;
        public AuthenticationController()
        {
            m_Bll = DPResolver.Resolver<IUserBll>();
        }

        public SEC_User GetUser(string name, string password, string corpcode)
        {
            return m_Bll.Login(name, password, corpcode);
        }
    }
}
