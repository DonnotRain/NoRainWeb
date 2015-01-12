using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WQTRights;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.WQT.CommonToolkit;
using HuaweiSoftware.WQT.IBll.Statistics;
using HuaweiSoftware.HOP.Model.Response.Statistics;

namespace HuaweiSoftware.WQT.Bll
{
    public class StatisticsSaleBll : CommonBLL, IStatisticsSaleBll
    {
        ICommonBLL m_commonBll;
        public StatisticsSaleBll(ICommonBLL bll)
        {
            m_commonBll = bll;
        }

        public object GetRealTimeAmount(int page, int rows, DateTime? beginDate, DateTime? endDate, string name, string code, string type)
        {
            var sql = Sql.Builder.Append(@"SELECT CI.Content as TypeName, I.Name,I.TYPE as TypeCode,SS.* FROM (SELECT  s.Code,SUM(s.TotalPrice) as Total,count(1) as TotalCount FROM MKT_Sell s  LEFT JOIN MKT_Items MI ON MI.Code=s.Code");

            sql.Append("Where S.CORPCODE=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And  s.Time >=@0", beginDate.Value);
            }

            //结束时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And s.Time <=@0", endDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And MI.Name like @0", name);
            }

            if (!string.IsNullOrEmpty(code))
            {
                code = "%" + code + "%";
                sql.Append(" And MI.Code like @0", code);
            }

            sql.Append("group by s.Code,MI.TYPE  ) SS LEFT JOIN MKT_Items I ON ss.Code=I.Code LEFT JOIN CategoryItems CI ON CI.Code=I.TYPE ");


            var totalSql = Sql.Builder.Append("SELECT  SUM(T.Total) as Total,sum(T.TotalCount) as TotalCount FROM ( ");
            totalSql.Append(sql.SQL, sql.Arguments);
            totalSql.Append(") T ");

            //分页和总销售额
            var result = new SaleStatistics();
            var pageResult = DBManage.DB.Page<dynamic>(page, rows, sql);
            result.rows = pageResult.Items;
            result.total = pageResult.TotalItems;
            result.footer = DBManage.DB.Fetch<dynamic>(totalSql);

            return result;
        }


        public object GetSalersPerformance(int page, int rows, DateTime? beginDate, DateTime? endDate, string name, string code)
        {
            var sql = Sql.Builder.Append(@"SELECT  I.UserName as UserName,I.Code, I.YearAge as YearAge,SS.* FROM (SELECT  s.UserId,SUM(s.TotalPrice) as Total,count(1) as TotalCount FROM MKT_Sell s");

            sql.Append("Where CORPCODE=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And  s.Time >=@0", beginDate.Value);
            }

            //结束时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And s.Time <=@0", endDate.Value.AddDays(1));
            }

            sql.Append("group by s.UserId  ) SS LEFT JOIN SEC_User I ON ss.UserId=I.ID WHERE 1=1        ");

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And I.UserName like @0", name);
            }

            if (!string.IsNullOrEmpty(code))
            {
                code = "%" + code + "%";
                sql.Append(" And I.Code like @0", code);
            }

            var totalSql = Sql.Builder.Append("SELECT  SUM(T.Total) as Total,sum(T.TotalCount) as TotalCount FROM ( ");
            totalSql.Append(sql.SQL, sql.Arguments);
            totalSql.Append(") T ");

            //分页和总销售额
            var result = new SaleStatistics();
            var pageResult = DBManage.DB.Page<dynamic>(page, rows, sql);
            result.rows = pageResult.Items;
            result.total = pageResult.TotalItems;
            result.footer = DBManage.DB.Fetch<dynamic>(totalSql);

            return result;
        }
    }
}
