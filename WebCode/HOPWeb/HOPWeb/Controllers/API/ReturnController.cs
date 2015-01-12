using DefaultConnection;
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

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class ReturnController : ApiController
    {
        private IReturnBll m_ReturnBll;
        public ReturnController()
        {
            m_ReturnBll = DPResolver.Resolver<IReturnBll>();
        }

        public object GetPager(int page, int rows, string beginDate, string endDate, string name, string code, string type, string storeName)
        {
            var pageResult = m_ReturnBll.GetReturnPager(page, rows, string.IsNullOrEmpty(beginDate) ? null : (DateTime?)DateTime.Parse(beginDate),
                string.IsNullOrEmpty(endDate) ? null : (DateTime?)DateTime.Parse(endDate), name, code, type, storeName);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }
    }
}
