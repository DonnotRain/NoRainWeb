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
using HuaweiSoftware.WQT.IDal;

namespace HuaweiSoftware.WQT.Bll
{
    public class StatisticsBll : CommonBLL, IStatisticsBll
    {
        ICommonSecurityBLL m_commonBll;
        public StatisticsBll(ICommonSecurityBLL bll)
        {
            m_commonBll = bll;
        }

        public HOP.Model.StatisticsReturn GetReturnInfo(int? number, string dateType)
        {
            var returnDal = DPResolver.Resolver<IReturnDal>();

            DateTime[] times = CommonToolkit.CommonToolkit.GetTypeTime(dateType);

            StatisticsReturn result = new StatisticsReturn();

            result.CurrentMonthAmount = 0;

            //先获取前x条数据
            var sql = Sql.Builder.Append("SELECT * FROM MKT_Return WHERE");
            sql.Append("CORPCODE=@0", SysContext.CorpCode);
            sql.OrderBy("Time DESC");
            var pagerResult = MKT_Return.Page(1, number ?? 10, sql);

            //附加上其他信息
            result.TopItems = returnDal.GetOtherInfo(pagerResult.Items);

            //获取本月的数量
            var currentSql = Sql.Builder.Append("SELECT COUNT(1) FROM MKT_Return WHERE");

            currentSql.Append("CORPCODE=@0 AND ", SysContext.CorpCode);

            var beginDate = times[0];

            var endDate = times[1];
            currentSql.Append("Time >=@0 And Time<=@1", beginDate, endDate);
            result.CurrentMonthAmount = MKT_Return.repo.ExecuteScalar<int>(currentSql);

            //获取上月的数量
            var lastSql = Sql.Builder.Append("SELECT COUNT(1) FROM MKT_Return WHERE");

            beginDate = times[2];

            endDate = times[3];

            lastSql.Append("Time >=@0 And Time<=@1", beginDate, endDate);

            result.LastMonthAmount = MKT_Return.repo.ExecuteScalar<int>(lastSql);

            //计算上升的比例（百分比）,可能是负数
            result.UpPercent = 100 * (result.LastMonthAmount != 0 ? ((result.CurrentMonthAmount - result.LastMonthAmount) / result.LastMonthAmount) : 1);
            result.UpPercent = Math.Round(result.UpPercent, 2);
            return result;
        }

        public HOP.Model.StatisticsSale GetSaleInfo(string dateType)
        {
            DateTime[] times = CommonToolkit.CommonToolkit.GetTypeTime(dateType);
            StatisticsSale result = new StatisticsSale();

            result.CurrentMonthAmount = 0;
            #region 销售额
            //获取本月的数量
            var currentSql = Sql.Builder.Append("SELECT Sum(TotalPrice) as Total FROM MKT_Sell WHERE");

            currentSql.Append("CORPCODE=@0 AND ", SysContext.CorpCode);

            var beginDate = times[0];

            var endDate = times[1];

            currentSql.Append("Time >=@0 And Time<=@1", beginDate, endDate);

            var number = MKT_Sell.repo.ExecuteScalar<decimal?>(currentSql);

            result.CurrentMonthAmount = number ?? 0;

            //获取上月的数量
            var lastSql = Sql.Builder.Append("SELECT Sum(TotalPrice) as Total FROM MKT_Sell WHERE");

            beginDate = times[2];

            endDate = times[3];


            lastSql.Append("Time >=@0 And Time<=@1", beginDate, endDate);

            number = MKT_Sell.repo.ExecuteScalar<decimal?>(lastSql);
            result.LastMonthAmount = number ?? 0;

            //计算上升的比例（百分比）,可能是负数
            result.UpPercent = (double)((result.LastMonthAmount != 0 ? ((result.CurrentMonthAmount - result.LastMonthAmount) / result.LastMonthAmount) : 1) * 100);
            result.UpPercent = Math.Round(result.UpPercent, 2);
            #endregion

            #region 销售笔数
            //获取本月的数量
            var currentTimeSql = Sql.Builder.Append("SELECT COUNT(1) as Total FROM MKT_Sell WHERE");

            currentTimeSql.Append("Time >=@0 And Time<=@1", times[0], times[1]);
            var numberTime = MKT_Sell.repo.ExecuteScalar<decimal?>(currentTimeSql);
            result.CurrentMonthTime = numberTime ?? 0;

            //获取上月的数量
            var lastSqlTime = Sql.Builder.Append("SELECT COUNT(1) as Total FROM MKT_Sell WHERE");

            lastSqlTime.Append("Time >=@0 And Time<=@1", times[2], times[3]);

            numberTime = MKT_Sell.repo.ExecuteScalar<decimal?>(lastSqlTime);
            result.LastMonthTime = numberTime ?? 0;

            //计算上升的比例（百分比）,可能是负数
            result.UpPercentTime = (double)((result.LastMonthTime != 0 ? ((result.CurrentMonthTime - result.LastMonthTime) / result.LastMonthTime) : 1) * 100);
            result.UpPercentTime = Math.Round(result.UpPercentTime, 2);
            #endregion

            return result;
        }

        public HOP.Model.StatisticsTraining GetTrainingInfo(int? number, string dateType)
        {
            DateTime[] times = CommonToolkit.CommonToolkit.GetTypeTime(dateType);
            StatisticsTraining result = new StatisticsTraining();

            result.CurrentMonthAmount = 0;

            //先获取前x条数据
            var sql = Sql.Builder.Append("SELECT * FROM TRN_StudyExamArrange WHERE");
            sql.Append("CORPCODE=@0", SysContext.CorpCode);
            var pagerResult = TRN_StudyExamArrange.Page(1, number ?? 10, sql);
            result.TopItems = pagerResult.Items;

            //获取本月的数量
            var currentSql = Sql.Builder.Append("SELECT COUNT(1) FROM TRN_StudyExamArrange WHERE");

            currentSql.Append("CORPCODE=@0 AND ", SysContext.CorpCode);

            currentSql.Append("ExamTime >=@0 And ExamTime<=@1", times[0], times[1]);

            result.CurrentMonthAmount = TRN_StudyExamArrange.repo.ExecuteScalar<int>(currentSql);

            //获取上月的数量
            var lastSql = Sql.Builder.Append("SELECT COUNT(1) FROM TRN_StudyExamArrange WHERE");

            lastSql.Append("ExamTime >=@0 And ExamTime<=@1", times[2], times[3]);

            result.LastMonthAmount = TRN_StudyExamArrange.repo.ExecuteScalar<int>(lastSql);

            //计算上升的比例（百分比）,可能是负数
            result.UpPercent = 100 * (result.LastMonthAmount != 0 ? ((result.CurrentMonthAmount - result.LastMonthAmount) / result.LastMonthAmount) : 1);
            result.UpPercent = Math.Round(result.UpPercent, 2);
            return result;
        }

        public List<BRD_Board> GetBoardInfo(int? number)
        {
            //先获取前x条数据
            var sql = Sql.Builder.Append("SELECT * FROM BRD_Board WHERE");
            sql.Append("CORPCODE=@0", SysContext.CorpCode);
            var pagerResult = BRD_Board.Page(1, number ?? 10, sql);    //默认取前10条

            return pagerResult.Items;
        }

        public List<MKT_Infomation> GetInformation(int? number)
        {
            var infoDal = DPResolver.Resolver<IInfomationDal>();

            //先获取前x条数据
            var sql = Sql.Builder.Append("SELECT * FROM MKT_Infomation WHERE");
            sql.Append("CORPCODE=@0", SysContext.CorpCode);
            sql.OrderBy("CreateTime DESC");
            var pagerResult = MKT_Infomation.Page(1, number ?? 10, sql);    //默认取前10条

            return infoDal.GetOtherInfo(pagerResult.Items);
        }

        public StatisticsRequest GetRequestInfo(int? number, string dateType)
        {
            DateTime[] times = CommonToolkit.CommonToolkit.GetTypeTime(dateType);

            StatisticsRequest result = new StatisticsRequest();

            result.CurrentMonthAmount = 0;

            //先获取前x条数据
            var sql = Sql.Builder.Append("SELECT * FROM MKT_Request WHERE");
            sql.Append("CORPCODE=@0", SysContext.CorpCode);
            var pagerResult = MKT_Request.Page(1, number ?? 10, sql);
            result.TopItems = pagerResult.Items;

            //获取本月的数量
            var currentSql = Sql.Builder.Append("SELECT COUNT(1) FROM MKT_Request WHERE");

            currentSql.Append("CORPCODE=@0 AND ", SysContext.CorpCode);

            currentSql.Append("CreateTime >=@0 And CreateTime<=@1", times[0], times[1]);

            result.CurrentMonthAmount = MKT_Request.repo.ExecuteScalar<int>(currentSql);

            //获取上月的数量
            var lastSql = Sql.Builder.Append("SELECT COUNT(1) FROM MKT_Request WHERE");

            lastSql.Append("CreateTime >=@0 And CreateTime<=@1", times[2], times[3]);

            result.LastMonthAmount = MKT_Request.repo.ExecuteScalar<int>(lastSql);

            //计算上升的比例（百分比）,可能是负数
            result.UpPercent = 100 * (result.LastMonthAmount != 0 ? ((result.CurrentMonthAmount - result.LastMonthAmount) / result.LastMonthAmount) : 1);
            result.UpPercent = Math.Round(result.UpPercent, 2);
            return result;
        }

        /// <summary>
        /// 各种类商品销售比例
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public Dictionary<string, double> GetTypeSaleRate(string dateType)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            DateTime[] times = CommonToolkit.CommonToolkit.GetTypeTime(dateType);

            var sql = Sql.Builder.Append(@"select TBA.*,I.Content ItemName FROM(SELECT  MI.Type,sum(S.TotalPrice) 
as Total  FROM MKT_Sell S   LEFT JOIN MKT_Items  MI ON MI.Code=s.Code WHERE Time >=@0 And Time<=@1 group by  MI.Type) TBA  Left join CategoryItems I on TBA.Type=I.Code", times[0], times[1]);

            var obj = FindAll<dynamic>(sql.SQL, sql.Arguments).ToList();
            var all = obj.Sum(m => (double)m.Total);

            //找不到分类名的话，直接用类型编号做Key
            obj.ForEach(m => result.Add(m.ItemName ?? m.Type, Math.Round((double)m.Total / all * 100, 2)));

            return result;
        }

        public List<StatisticsChange> GetSaleChanges(string dateType)
        {

            DateTime[] times = CommonToolkit.CommonToolkit.GetTypeTime(dateType);

            var sql = Sql.Builder.Append(@"SELECT I.Name,SS.* FROM (SELECT convert(char(10),s.Time,120) TimeOfDay, s.Code,SUM(s.TotalPrice) as Total FROM MKT_Sell s
Where Time>=@0 and time<=@1
group by s.Code,convert(char(10),s.Time,120)) SS LEFT JOIN MKT_Items I ON ss.Code=I.Code", times[0], times[1]);

            var result = FindAll<StatisticsChange>(sql.SQL, sql.Arguments).ToList();


            return result;
        }

        public StatisticsLeave GetLeaveInfo(string dateType)
        {
            DateTime[] times = CommonToolkit.CommonToolkit.GetTypeTime(dateType);

            StatisticsLeave result = new StatisticsLeave();

            result.CurrentMonthAmount = 0;

            //先获取前x条数据
            var sql = Sql.Builder.Append("SELECT * FROM ATD_LEAVE WHERE");
            sql.Append("CORPCODE=@0", SysContext.CorpCode);
            sql.OrderBy("BeginDate DESC");
            var pagerResult = ATD_Leave.Page(1, 10, sql).Items;

            result.TopItems = pagerResult;
            result.TopItems.ForEach(m =>
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

            //获取本月的数量
            var currentSql = Sql.Builder.Append("SELECT COUNT(1) FROM ATD_LEAVE WHERE");
            currentSql.Append("CORPCODE=@0 AND ", SysContext.CorpCode);

            var beginDate = times[0];

            var endDate = times[1];
            currentSql.Append("BeginDate >=@0 And BeginDate<=@1", beginDate, endDate);
            result.CurrentMonthAmount = ATD_Leave.repo.ExecuteScalar<int>(currentSql);

            //获取上月的数量
            var lastSql = Sql.Builder.Append("SELECT COUNT(1) FROM ATD_LEAVE WHERE");

            beginDate = times[2];

            endDate = times[3];

            lastSql.Append("BeginDate >=@0 And BeginDate<=@1", beginDate, endDate);

            result.LastMonthAmount = ATD_Leave.repo.ExecuteScalar<int>(lastSql);

            return result;
        }
    }
}
