using DefaultConnection;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.HOP.Model.Request;
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

namespace WWQTWeb.Controllers.MobileAPI
{
    [MobileValidate()]
    [MobileServiceMark]
    public class MobileLeaveController : ApiController
    {
        private ILeaveBll m_LeaveBll;
        public MobileLeaveController()
        {
            m_LeaveBll = DPResolver.Resolver<ILeaveBll>();
        }

        /// <summary>
        /// 获取请假记录列表
        /// </summary>
        /// <param name="userName">员工名称</param>
        /// <returns>请假记录列表</returns>
        public List<LeaveInfo> GetLeaveData()
        {
            return m_LeaveBll.GetLeaveData(SysContext.UserName);
        }

        /// <summary>
        /// 添加请假记录
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="duration">时长</param>
        /// <param name="reason">请假原因</param>
        /// <param name="name">请假人</param>
        /// <returns>是否请假成功</returns>
        public bool AddLeave(LeaveContract leave)
        {
            return m_LeaveBll.AddLeave(leave);
        }
    }
}
