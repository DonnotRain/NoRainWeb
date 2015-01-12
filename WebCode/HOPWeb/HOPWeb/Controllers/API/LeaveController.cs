using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class LeaveController : ApiController
    {
        private ILeaveBll m_leaveBll;
        public LeaveController()
        {
            m_leaveBll = DPResolver.Resolver<ILeaveBll>();
        }

        public object GetPager(int page, int rows, string beginDate, string endDate, string state)
        {
            var pageResult = m_leaveBll.GetLeavePager(page, rows, string.IsNullOrEmpty(beginDate) ? null : (DateTime?)DateTime.Parse(beginDate),
                string.IsNullOrEmpty(endDate) ? null : (DateTime?)DateTime.Parse(endDate), state);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        /// <summary>
        /// 添加请假申请
        /// </summary>
        /// <param name="isPass"></param>
        /// <param name="ID"></param>
        public void PutLeave(bool isPass, int ID)
        {
            m_leaveBll.PutLeave(isPass, ID);
        }

    }
}
