using HuaweiSoftware.WQT.IBll.Statistics;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API.Statictics
{
    [ServiceValidate()]
    public class StatisticsTrainingRecordController : ApiController
    {
        private IStatisticsTrainingBll m_SaleBll;
        public StatisticsTrainingRecordController()
        {
            m_SaleBll = DPResolver.Resolver<IStatisticsTrainingBll>();
        }
        // GET api/<controller>
        public object GetPager(int page, int rows, DateTime? beginDate, DateTime? endDate, int? userId,string corpCode)
        {
            return m_SaleBll.GetPager(page, rows, beginDate, endDate, userId, corpCode);
        }
    }
}