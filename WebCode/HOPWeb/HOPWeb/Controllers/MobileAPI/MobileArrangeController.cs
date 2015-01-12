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
    public class MobileArrangeController : ApiController
    {
        private IArrangeBll m_ArrangeBll;
        public MobileArrangeController()
        {
            m_ArrangeBll = DPResolver.Resolver<IArrangeBll>();
        }

        public List<TRN_StudyExamArrange> GetExamArrange(string corpCode,string userName)
        {
            List<TRN_StudyExamArrange> trn_StudyExamArrangeList = m_ArrangeBll.GetExamArrangeByCorpCode(corpCode,userName);

            return trn_StudyExamArrangeList;
        }

        [Route("api/MobileArrange/GetArrangeDetail")]
        public TRN_StudyExamArrange GetArrangeDetail(int ID)
        {
            return m_ArrangeBll.GetArrangeDetail(ID);
        }
    }
}