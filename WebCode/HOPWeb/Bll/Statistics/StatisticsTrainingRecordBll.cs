using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuaweiSoftware.WQT.IBll.Statistics;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.HOP.Model.Response.Statistics;
using HuaweiSoftware.WQT.WebBase;

namespace HuaweiSoftware.WQT.Bll.Statistics
{
    public class StatisticsTrainingRecordBll : CommonBLL, IStatisticsTrainingRecordBll
    {
        ICommonBLL m_commonBll;
        public StatisticsTrainingRecordBll(ICommonBLL bll)
        {
            m_commonBll = bll;
        }

        public object GetPager(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, int? userID,string corpCode)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT COUNT(T.ID) AS ExamCount,UserName FROM TRN_ExamScore AS T").Append("LEFT JOIN SEC_User AS S ON T.UserID = S.ID").Append("WHERE T.CorpCode=@0",corpCode);

            if (beginTime != null && endTime != null)
            {
                sql.Append("AND T.CreateTime >= @0 AND T.CreateTime <= @1",beginTime,endTime);
            }
            else if (beginTime != null)
            {
                sql.Append("AND T.CreateTime >= @0", beginTime);
            }
            else if (endTime != null)
            {
                sql.Append("AND T.CreateTime <= @0", endTime);
            }

            if (userID.HasValue)
            {
                sql.Append("AND UserID = @0",userID.Value);
            }

            sql.Append("GROUP BY UserID,UserName");

            var totalSql = PetaPoco.Sql.Builder.Append("SELECT SUM(ExamCount) AS ExamCount").Append("FROM (");
            totalSql = totalSql.Append(sql.SQL, sql.Arguments);
            totalSql = totalSql.Append(") AS T");

            var result = new SaleStatistics();

            var judgeObject = FindAll<object>(sql.SQL, sql.Arguments);
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
