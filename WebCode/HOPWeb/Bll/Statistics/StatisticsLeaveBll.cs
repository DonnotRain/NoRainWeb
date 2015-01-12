using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuaweiSoftware.WQT.IBll.Statistics;
using HuaweiSoftware.WQT.IBll;
using DefaultConnection;
using HuaweiSoftware.WQT.WebBase;
using HuaweiSoftware.HOP.Model.Response.Statistics;

namespace HuaweiSoftware.WQT.Bll.Statistics
{
    public class StatisticsLeaveBll : CommonBLL, IStatisticsLeaveBll
    {
        ICommonBLL m_commonBll;
        public StatisticsLeaveBll(ICommonBLL bll)
        {
            m_commonBll = bll;
        }

        /// <summary>
        /// 获取请假人员统计信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetPager(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, int? userID,string corpCode)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT UserId,COUNT(A.ID) AS LeaveCount,SUM(Duration) AS Duration,UserName FROM ATD_Leave AS A").Append("LEFT JOIN SEC_User AS S ON A.UserId = S.ID").Append("WHERE [State] = 1 AND A.CorpCode=@0",corpCode);

            if (beginTime != null && endTime != null)
            {
                sql = sql.Append("AND EndDate>@0 AND BeginDate<@1",beginTime,endTime);
            }
            else if (beginTime != null)
            {
                sql = sql.Append("AND EndDate>@0", beginTime);
            }
            else if (endTime != null)
            {
                sql = sql.Append("AND BeginDate<@0",endTime);
            }

            if (userID.HasValue)
            {
                sql = sql.Append("AND UserId=@0", userID.Value);
            }

            sql.Append("GROUP BY UserId,UserName");

            var totalSql = PetaPoco.Sql.Builder.Append("SELECT SUM(LeaveCount) AS LeaveCount,SUM(Duration) AS Duration").Append("FROM (");
            totalSql = totalSql.Append(sql.SQL, sql.Arguments);
            totalSql = totalSql.Append(") AS T");

            var result = new SaleStatistics();

            var judgeObject =  FindAll<object>(sql.SQL, sql.Arguments);
            if (judgeObject.ToList().Count > 0)
            {
                var resultPage = FindAllByPage<object>(sql.SQL, pageSize, pageIndex, sql.Arguments);
                result.rows = resultPage.Items;
                result.total = judgeObject.ToList().Count;
                result.footer = DBManage.DB.Fetch<object>(totalSql);
            }
            else
            {
                result.rows = new List<dynamic>();
                result.total = 0;
                result.footer = new List<dynamic>();
            }

            return result;
        }
    }
}
