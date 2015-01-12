using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using DefaultConnection;
using WQTWeb.Filters;
using HuaweiSoftware.HOP.Model;

namespace WQTWeb.Controllers.API
{
    /// <summary>
    /// 首页的一些统计数据从这里获取
    /// </summary>
    [ServiceValidate()]
    public class StatisticsController : ApiController
    {
        private IStatisticsBll m_Bll;
        public StatisticsController()
        {
            m_Bll = DPResolver.Resolver<IStatisticsBll>();
        }

        /// <summary>
        /// 店面需求信息
        /// </summary>
        /// <returns></returns>
        [Route("API/Statistics/Request")]
        public StatisticsRequest GetRequestInfo(int? number, string dateType)
        {
            return m_Bll.GetRequestInfo(number, dateType);
        }

        /// <summary>
        /// 退货信息
        /// </summary>
        /// <returns></returns>
        [Route("API/Statistics/Return")]
        public StatisticsReturn GetReturnInfo(int? number, string dateType)
        {
            return m_Bll.GetReturnInfo(number, dateType);
        }

        /// <summary>
        /// 销售情况
        /// </summary>
        /// <returns></returns>
        [Route("API/Statistics/Sale")]
        public StatisticsSale GetSaleInfo(string dateType)
        {
            return m_Bll.GetSaleInfo(dateType);
        }

        /// <summary>
        /// 培训情况
        /// </summary>
        /// <returns></returns>
        [Route("API/Statistics/Training")]
        public StatisticsTraining GetTrainingInfo(int? number, string dateType)
        {
            return m_Bll.GetTrainingInfo(number, dateType);
        }

        /// <summary>
        /// 前X条最新公告
        /// </summary>
        /// <returns></returns>
        [Route("API/Statistics/Board")]
        public List<BRD_Board> GetBoardInfo(int? number)
        {
            return m_Bll.GetBoardInfo(number);
        }

        /// <summary>
        /// 前X条最新竞争信息
        /// </summary>
        /// <returns></returns>
        [Route("API/Statistics/Information")]
        public List<MKT_Infomation> GetInformation(int? number)
        {
            return m_Bll.GetInformation(number); ;
        }


        /// <summary>
        /// 获取天/周/月/年的商品销售比例数据
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        [Route("API/Statistics/SaleRate")]
        public Dictionary<string, double> GetSaleRate(string dateType)
        {
            return m_Bll.GetTypeSaleRate(dateType);
        }
        /// <summary>
        /// 获取天/周/月/年的商品销售额变化情况
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        [Route("API/Statistics/SaleChange")]
        public List<StatisticsChange> GetSaleChanges(string dateType)
        {
            return m_Bll.GetSaleChanges(dateType); ;
        }

        /// <summary>
        /// 请假信息
        /// </summary>
        /// <returns></returns>
        [Route("API/Statistics/Leave")]
        public StatisticsLeave GetLeaveInfo(string dateType)
        {
            return m_Bll.GetLeaveInfo(dateType);
        }
    }
}