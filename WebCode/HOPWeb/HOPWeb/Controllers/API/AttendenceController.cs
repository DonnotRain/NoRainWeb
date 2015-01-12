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
    public class AttendenceController : ApiController
    {
        private IAttendenceBll m_AttendenceBll;
        public AttendenceController()
        {
            m_AttendenceBll = DPResolver.Resolver<IAttendenceBll>();
        }

        public object GetPager(int page, int rows, string beginDate, string endDate, string userName)
        {
            var pageResult = m_AttendenceBll.GetAttendencePager(page, rows, string.IsNullOrEmpty(beginDate) ? null : (DateTime?)DateTime.Parse(beginDate),
                string.IsNullOrEmpty(endDate) ? null : (DateTime?)DateTime.Parse(endDate), userName);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        

    }
}
