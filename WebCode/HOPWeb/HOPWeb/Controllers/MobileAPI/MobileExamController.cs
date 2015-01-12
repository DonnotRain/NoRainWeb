using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WQTWeb.Filters;
using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;

namespace WQTWeb.Controllers.MobileAPI
{
    [MobileValidate()]
    [MobileServiceMark]
    public class MobileExamController : ApiController
    {
        private IExamBll m_ExamBll;
        public MobileExamController()
        {
            m_ExamBll = DPResolver.Resolver<IExamBll>();
        }
        // GET api/<controller>
        //public List<TRN_Exam> GetExam(string corpCode)
        //{
        //    return m_ExamBll.GetExamByCorp(corpCode);
        //}

        

        [Route("api/MobileExam/GetExamJudge")]
        public TRN_Judge GetExamJudge(int examID, int seq)
        {
            return m_ExamBll.GetExamJudge(examID, seq);
        }

        [Route("api/MobileExam/GetExamChoice")]
        public TRN_Choice GetExamChoice(int examID, int seq)
        {
            return m_ExamBll.GetExamChoice(examID, seq);
        }
    }
}