using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WQTWeb.Filters;
using HuaweiSoftware.WQT.IBll.Statistics;
using HuaweiSoftware.WQT.WebBase;
using DefaultConnection;

namespace WQTWeb.Controllers.API.Statictics
{
    [ServiceValidate()]
    public class StatisticsLeaveController : ApiController
    {
        private IStatisticsLeaveBll m_SaleBll;
       public StatisticsLeaveController()
        {
            m_SaleBll = DPResolver.Resolver<IStatisticsLeaveBll>();
        }

       public object GetLeaveData(int page, int rows, DateTime? beginDate, DateTime? endDate, int? userId,string corpCode)
       {
           return m_SaleBll.GetPager(page, rows, beginDate, endDate, userId,corpCode);
       }
    }
}