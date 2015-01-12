using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using HuaweiSoftware.WQT.WebBase;
using WQTWeb.Filters;
using HuaweiSoftware.WQT.IBll;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class StatisticsAttendenceController : ApiController
    {
        private IStatisticsAttendenceBll m_Bll;
        public StatisticsAttendenceController()
        {
            m_Bll = DPResolver.Resolver<IStatisticsAttendenceBll>();
        }

        public object GetPager(string time, int? userId, int page, int rows)
        {
            return m_Bll.GetPager(time, userId,page,rows);
        }
    }
}