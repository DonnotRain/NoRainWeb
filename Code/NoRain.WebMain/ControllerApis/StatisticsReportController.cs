using NoRain.Business.IBll;
using NoRain.Business.WebBase;
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

using MainWeb.Filters;
using NoRain.Business.Model.Request;
using NoRain.Business.Model.Response;
using DefaultConnection;
using AttributeRouting.Web.Http;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class StatisticsReportController : ApiController
    {
        private IStatisticsReportService m_Bll;
        public StatisticsReportController()
        {
            m_Bll = DPResolver.Resolver<IStatisticsReportService>();
        }

        [GET("API/StatisticsReport/DataTablePager")]
        public object GetDataTablePager([FromUri]DataTablesRequest reqestParams, [FromUri]RolePagerCondition condition)
        {
            var pageResult = m_Bll.GetReportResults();


            return pageResult;
        }

    }
}
