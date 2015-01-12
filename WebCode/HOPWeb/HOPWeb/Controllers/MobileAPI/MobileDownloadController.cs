using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WQTWeb.Filters;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;

namespace WQTWeb.Controllers.MobileAPI
{
    [MobileServiceMark]
    public class MobileDownloadController : ApiController
    {
        private IAppDownloadBll m_Bll;

        public MobileDownloadController()
        {
            m_Bll = DPResolver.Resolver<IAppDownloadBll>();
        }

        [Route("app/Download")]
        public void GetDownloadMobileApp(string type)
        {
            m_Bll.DownloadApp(type);
        }

        [Route("app/Version")]
        public string GetAppVersion(string type)
        {
            return m_Bll.GetAppVersion(type);
        }
    }
}