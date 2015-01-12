using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.MobileAPI
{
    [MobileValidate()]
    [MobileServiceMark]
    public class MobileScoreController : ApiController
    {
        private IScoreBll m_ScoreBll;
        public MobileScoreController()
        {
            m_ScoreBll = DPResolver.Resolver<IScoreBll>();
        }

        /// <summary>
        /// 获取考试成绩
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public List<TRN_ExamScore> GetExamScore(int userId)
        //{
        //    List<TRN_ExamScore> trn_ExamScoreList = m_ScoreBll.GetExamScoreByUserId(userId);

        //    return trn_ExamScoreList;
        //}

        /// <summary>
        /// 增加考试分数
        /// </summary>
        /// <param name="trn_ExamScore"></param>
        public void PostExamScore(TRN_ExamScore trn_ExamScore)
        {
            m_ScoreBll.AddExamScore(trn_ExamScore);
        }
    }
}