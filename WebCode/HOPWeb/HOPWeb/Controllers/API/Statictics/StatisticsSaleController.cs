using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using HuaweiSoftware.WQT.CommonToolkit;
using HuaweiSoftware.WQT.IBll.Statistics;
using HuaweiSoftware.WQT.WebBase;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API.Statictics
{
    [ServiceValidate()]
    public class StatisticsSaleController : ApiController
    {
        private IStatisticsSaleBll m_SaleBll;
        public StatisticsSaleController()
        {
            m_SaleBll = DPResolver.Resolver<IStatisticsSaleBll>();
        }


        [Route("API/StatisticsSale/RealTimeAmount")]
        public object GetRealTimeAmount(int page, int rows, string beginDate, string endDate, string name, string code, string type)
        {
            return m_SaleBll.GetRealTimeAmount(page, rows, beginDate.ToDateTime(),
              endDate.ToDateTime(), name, code, type);
        }

        [Route("API/StatisticsSale/SalersPerformance")]
        public object GetSalersPerformance(int page, int rows, string beginDate, string endDate, string name, string code)
        {
            return m_SaleBll.GetSalersPerformance(page, rows, beginDate.ToDateTime(),
              endDate.ToDateTime(), name, code);
        }
    }
}
