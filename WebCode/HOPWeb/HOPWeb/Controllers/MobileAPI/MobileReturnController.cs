using DefaultConnection;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.HOP.Model.Request;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WQTWeb.Filters;

namespace WWQTWeb.Controllers.MobileAPI
{
    [MobileValidate()]
    [MobileServiceMark]
    public class MobileReturnController : ApiController
    {
        private IReturnBll m_ReturnBll;
        public MobileReturnController()
        {
            m_ReturnBll = DPResolver.Resolver<IReturnBll>();
        }

        public List<MKT_Return> GetReturn(string name)
        {
            return m_ReturnBll.GetReturnByName(SysContext.UserName,name);
        }

        public MKT_Return PostReturn(ReturnContract model)
        {
            return m_ReturnBll.AddReturn(model);
        }
    }
}
