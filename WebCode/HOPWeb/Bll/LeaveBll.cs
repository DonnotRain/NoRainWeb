using DefaultConnection;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class LeaveBll : CommonBLL, ILeaveBll
    {
        public PetaPoco.Page<DefaultConnection.ATD_Leave> GetLeavePager(int pageIndex, int pageSize, DateTime? beginDate, DateTime? endDate, string state)
        {

            var sql = Sql.Builder.Append("Where CorpCode=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And BeginDate >=0", beginDate.Value);
            }
            //开始时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And BeginDate <=@0", endDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(state))
            {
                sql.Append(string.Format(" And State in ({0})", state));
            }

            sql.OrderBy("BeginDate desc");

            var page = FindAllByPage<ATD_Leave>(sql.SQL.ToString(), pageSize, pageIndex, sql.Arguments);
            page.Items.ForEach(m =>
            {
                var user = Find<SEC_User>("Where Id=@0", m.UserId);
                if (user == null)
                {
                    m.Name = "无相关用户";
                    m.StoreName = "无相关用户";
                }
                else
                {
                    var store = Find<SEC_Store>("Where ID=@0", user.StoreId);
                    m.StoreName = store != null ? store.StoreName : "无相关门店";
                    m.Name = user.UserName;
                }
            });
            return page;
        }


        public List<HOP.Model.LeaveInfo> GetLeaveData(string userName)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", userName, SysContext.CorpCode);
            var sql = Sql.Builder.Append("Where CorpCode=@0 ", SysContext.CorpCode);
            sql.Append("AND UserId=@0", user.ID);
            sql.Append("ORDER BY BeginDate DESC");

            return ATD_Leave.Fetch(sql.SQL, sql.Arguments).Select(m => new LeaveInfo()
            {
                BeginDate = m.BeginDate,
                Duration = m.Duration,
                EndDate = m.EndDate,
                ID = m.ID,
                Name = m.Name,
                Reason = m.Reason,
                State = m.State
            }).ToList();
        }

        public bool AddLeave(HOP.Model.Request.LeaveContract leave)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);
            ATD_Leave leaveInfo = new ATD_Leave();
            leaveInfo.BeginDate = leave.beginDate;
            leaveInfo.EndDate = leave.endDate;
            leaveInfo.State = -1;
            leaveInfo.Duration = leave.duration;
            leaveInfo.Reason = leave.reason;
            leaveInfo.Name = SysContext.UserName;
            leaveInfo.UserId = (int)user.ID;
            leaveInfo.CorpCode = SysContext.CorpCode;
            Insert(leaveInfo);
            return true;
        }

        public void PutLeave(bool isPass, int ID)
        {
            ATD_Leave attendance = Find<ATD_Leave>("WHERE ID=@0", Convert.ToInt32(ID));
            if (isPass)
            {
                attendance.State = 1;
            }
            else
            {
                attendance.State = 0;
            }

            attendance.Update();
        }
    }
}
