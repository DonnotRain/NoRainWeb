using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuaweiSoftware.WQT.IBll.Statistics;
using HuaweiSoftware.WQT.IBll;
using DefaultConnection;
using HuaweiSoftware.HOP.Model.Response.Statistics;
using HuaweiSoftware.WQT.WebBase;

namespace HuaweiSoftware.WQT.Bll.Statistics
{
    public class StatisticsAttendenceBll : CommonBLL, IStatisticsAttendenceBll
    {
        ICommonSecurityBLL m_commonBll;

        public StatisticsAttendenceBll(ICommonSecurityBLL bll)
        {
            m_commonBll = bll;
        }

        public object GetPager(string time, int? userId,int pageIndex, int pageSize)
        {
            string workHour = Find<Parameter>("WHERE Name=@0", "上班时间").Value;
            string closedHour = Find<Parameter>("WHERE Name=@0", "下班时间").Value;

            //var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM (SELECT COUNT(ID) AS LateTimes,YEAR([Time]) AS FYear,MONTH([Time]) AS FMonth, UserId  AS FUserId FROM ATD_Attendence WHERE DATEDIFF(MI,@0,CONVERT(VARCHAR(12),[Time],108)) > 0  AND Type=0 GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS T", workHour).Append("FULL JOIN (SELECT COUNT(ID) AS EarlyTimes,YEAR([Time]) AS SYear,MONTH([Time]) AS SMonth,UserId AS SUserId FROM ATD_Attendence WHERE DATEDIFF(MI,@0,CONVERT(VARCHAR(12),[Time],108)) < 0  AND Type=1 GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS S", closedHour).Append("ON T.FYear = S.SYear AND T.FMonth = S.SMonth AND T.FUserId = S.SUserId").Append("LEFT JOIN SEC_User AS U").Append("ON S.SUserId = U.ID OR T.FUserId = U.ID");

            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM SEC_User AS U").Append("LEFT JOIN").Append("(SELECT YEAR([Time]) AS TYear,MONTH([Time]) AS TMonth,UserId AS TUserId FROM ATD_Attendence GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS R").Append("ON U.ID = R.TUserId").Append("LEFT JOIN").Append("(SELECT COUNT(ID) AS LateTimes,YEAR([Time]) AS FYear,MONTH([Time]) AS FMonth, UserId  AS FUserId FROM ATD_Attendence WHERE DATEDIFF(MI,@0,CONVERT(VARCHAR(12),[Time],108)) > 0  AND Type=0 GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS T", workHour).Append("ON R.TUserId = T.FUserId AND R.TMonth = T.FMonth AND R.TYear = T.FYear").Append("LEFT JOIN").Append("(SELECT COUNT(ID) AS EarlyTimes,YEAR([Time]) AS SYear,MONTH([Time]) AS SMonth,UserId AS SUserId FROM ATD_Attendence WHERE DATEDIFF(MI,@0,CONVERT(VARCHAR(12),[Time],108)) < 0  AND Type=1 GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS S", closedHour).Append("ON R.TUserId = S.SUserId AND R.TMonth = S.SMonth AND R.TYear = S.SYear");
            var noOrderSql = PetaPoco.Sql.Builder.Append("SELECT * FROM SEC_User AS U").Append("LEFT JOIN").Append("(SELECT YEAR([Time]) AS TYear,MONTH([Time]) AS TMonth,UserId AS TUserId FROM ATD_Attendence GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS R").Append("ON U.ID = R.TUserId").Append("LEFT JOIN").Append("(SELECT COUNT(ID) AS LateTimes,YEAR([Time]) AS FYear,MONTH([Time]) AS FMonth, UserId  AS FUserId FROM ATD_Attendence WHERE DATEDIFF(MI,@0,CONVERT(VARCHAR(12),[Time],108)) > 0  AND Type=0 GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS T", workHour).Append("ON R.TUserId = T.FUserId AND R.TMonth = T.FMonth AND R.TYear = T.FYear").Append("LEFT JOIN").Append("(SELECT COUNT(ID) AS EarlyTimes,YEAR([Time]) AS SYear,MONTH([Time]) AS SMonth,UserId AS SUserId FROM ATD_Attendence WHERE DATEDIFF(MI,@0,CONVERT(VARCHAR(12),[Time],108)) < 0  AND Type=1 GROUP BY YEAR([Time]),MONTH([Time]),UserId) AS S", closedHour).Append("ON R.TUserId = S.SUserId AND R.TMonth = S.SMonth AND R.TYear = S.SYear");

            sql = sql.Append("LEFT JOIN (SELECT Value AS LateDeductMoney FROM Parameters WHERE Name ='迟到扣钱' ) AS P1 ON 1=1").Append("LEFT JOIN (SELECT Value AS EarlyDeductMoney FROM Parameters WHERE Name = '早退扣钱') AS P2 ON 1=1").Append("WHERE 1=1");
            noOrderSql = noOrderSql.Append("LEFT JOIN (SELECT Value AS LateDeductMoney FROM Parameters WHERE Name ='迟到扣钱' ) AS P1 ON 1=1").Append("LEFT JOIN (SELECT Value AS EarlyDeductMoney FROM Parameters WHERE Name = '早退扣钱') AS P2 ON 1=1").Append("WHERE 1=1");

            if (userId != null)
            {
                sql.Append("AND R.TUserId = @0",userId);
                noOrderSql.Append("AND R.TUserId = @0", userId);
            }

            if (time != null)
            {
                if (time.Length > 4)
                {
                    string[] dateTime = time.Split('-');
                    sql.Append("AND R.TYear = @0 AND R.TMonth = @1", dateTime[0], dateTime[1]);
                    noOrderSql.Append("AND R.TYear = @0 AND R.TMonth = @1", dateTime[0], dateTime[1]);
                }
                else
                {
                    sql.Append("AND R.TYear = @0", time);
                    noOrderSql.Append("AND R.TYear = @0", time);
                }
            }

            sql = sql.Append(" ORDER BY R.TYear,R.TMonth DESC");

            var totalSql = PetaPoco.Sql.Builder.Append("SELECT SUM(LateTimes) AS LateTimes,SUM(EarlyTimes)AS EarlyTimes,(ISNULL(SUM(Wage),0)- ISNULL(SUM(LateTimes),0)*ISNULL(LateDeductMoney,0) - ISNULL(SUM(EarlyTimes),0)*ISNULL(EarlyDeductMoney,0)) AS ActualWage").Append("FROM (");
            totalSql = totalSql.Append(noOrderSql.SQL, noOrderSql.Arguments);
            totalSql = totalSql.Append(") AS F GROUP BY LateDeductMoney,EarlyDeductMoney");

            var result = new SaleStatistics();
            var totalItems = FindAll<object>(sql.SQL, sql.Arguments).ToList();

            if (totalItems.Count > 0)
            {
                var resultPage = FindAllByPage<object>(sql.SQL, pageSize, pageIndex, sql.Arguments);
                result.rows = resultPage.Items;
                result.total = totalItems.Count;
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
