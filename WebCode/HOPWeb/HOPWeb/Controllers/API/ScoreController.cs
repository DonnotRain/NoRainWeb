using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using HuaweiSoftware.WQT.WebBase;
using WQTWeb.Filters;
using HuaweiSoftware.WQT.IBll;
using DefaultConnection;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class ScoreController : ApiController
    {
       private IScoreBll m_Bll;
       public ScoreController()
        {
            m_Bll = DPResolver.Resolver<IScoreBll>();
        }

       public object GetPager(int? userId, DateTime? createTime, int page, int rows)
       {
           var pageResult = m_Bll.GetPager(userId, createTime, page, rows);

           return new { total = pageResult.TotalItems, rows = pageResult.Items };
       }
    }
}