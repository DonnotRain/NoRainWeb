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
    public class MobileStudyController : ApiController
    {
        public IStudyBll m_StudyBll;
        public MobileStudyController()
        {
            m_StudyBll = DPResolver.Resolver<IStudyBll>();
        }
        // GET api/<controller>
        public List<TRN_Study> GetStudy(string corpCode,string title)
        {
            return m_StudyBll.GetStudyByCorp(corpCode,title);
        }

        [Route("api/MobileStudy/GetStudyDetail")]
        public TRN_Study GetStudyDetail(int ID)
        {
            return m_StudyBll.GetStudyDetail(ID);
        }
    }
}