using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using WQTWeb.Filters;
using DefaultConnection;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class ExamController : ApiController
    {
        private IExamBll m_Bll;
        public ExamController()
        {
            m_Bll = DPResolver.Resolver<IExamBll>();
        }

        /// <summary>
        /// 分页考试
        /// </summary>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public object GetPager(string title, int? type, int page, int rows)
        {
            var pageResult = m_Bll.GetPager(title, type, page, rows);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        /// <summary>
        /// 增加考试
        /// </summary>
        /// <param name="Exam"></param>
        /// <returns></returns>
        public object PostExam(TRN_Exam exam)
        {
            var item = m_Bll.Add(exam);

            return item;
        }

        public object PutExam(TRN_Exam exam)
        {
            var item = m_Bll.Edit(exam);

            return item;
        }

        public void DeleteExam([FromBody]string ids)
        {
            m_Bll.Delete(ids);
        }

        [Route("api/Exam/GetAll")]
        public object GetAllExam()
        {
            var pages = m_Bll.GetAll();

            return pages;
        }
    }
}