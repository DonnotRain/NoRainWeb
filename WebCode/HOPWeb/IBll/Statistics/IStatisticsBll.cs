using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DefaultConnection;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IStatisticsBll
    {
        /// <summary>
        /// 获取退货信息
        /// </summary>
        /// <param name="number"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        HOP.Model.StatisticsReturn GetReturnInfo(int? number, string dateType);

        //获取销售信息
        HOP.Model.StatisticsSale GetSaleInfo(string dateType);

        /// <summary>
        /// 获取培训信息
        /// </summary>
        /// <param name="number"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        HOP.Model.StatisticsTraining GetTrainingInfo(int? number, string dateType);

        /// <summary>
        /// 获取最新公告
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        List<BRD_Board> GetBoardInfo(int? number);

        /// <summary>
        /// 获取竞争信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        List<MKT_Infomation> GetInformation(int? number);

        /// <summary>
        /// 获取需求信息
        /// </summary>
        /// <param name="number"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        HOP.Model.StatisticsRequest GetRequestInfo(int? number, string dateType);

        /// <summary>
        /// 获取商品分类销售比例
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        Dictionary<string, double> GetTypeSaleRate(string dateType);

        /// <summary>
        /// 获取商品实时销售变化情况
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        List<HOP.Model.StatisticsChange> GetSaleChanges(string dateType);

        HOP.Model.StatisticsLeave GetLeaveInfo(string dateType);
    }
}
