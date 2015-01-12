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

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class StudyController : ApiController
    {
        private IStudyBll m_Bll;
        public StudyController()
        {
            m_Bll = DPResolver.Resolver<IStudyBll>();
        }

        public object GetPager(string title,int? type,DateTime? beginTime,DateTime? endTime,int page,int rows)
        {
            var pageResult = m_Bll.GetPager(title, type, beginTime,endTime, rows, page);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        /// <summary>
        /// 增加学习资料
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        public object PostStudy(TRN_Study trn_Study)
        {
            var entity = m_Bll.Add(trn_Study);

            return entity;
        }

        public object PutStudy(TRN_Study trn_Study)
        {
            var entity = m_Bll.Edit(trn_Study);

            return entity;
        }

        public void DeleteStudy([FromBody] string ids)
        {
            m_Bll.Delete(ids);
        }

        [Route("api/study/GetAll")]
        public object GetAllStudy()
        {
            var pages = m_Bll.GetAll();

            return pages;
        }

        private  enum StudyType
        {
            Principle = 1,
            Product,
            Market
        }
    }

    
}