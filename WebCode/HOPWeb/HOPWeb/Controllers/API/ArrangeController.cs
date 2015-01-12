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

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class ArrangeController : ApiController
    {
        private IArrangeBll m_Bll;
        public ArrangeController()
        {
            m_Bll = DPResolver.Resolver<IArrangeBll>();
        }


        public object GetPager(string title,int? type,int page,int rows)
        {
            var pageResult = m_Bll.GetPager(title, type, rows, page);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        /// <summary>
        /// 增加学习资料
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        public object PostStudy(TRN_StudyExamArrange trn_Study)
        {
            var entity = m_Bll.Add(trn_Study);

            return entity;
        }

        public object PutStudy(TRN_StudyExamArrange trn_Study)
        {
            var entity = m_Bll.Edit(trn_Study);

            return entity;
        }

        public void DeleteStudy([FromBody] string ids)
        {
            m_Bll.Delete(ids);
        }
    }
}