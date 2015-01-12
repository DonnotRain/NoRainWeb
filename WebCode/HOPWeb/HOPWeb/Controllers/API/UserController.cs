using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class UserController : ApiController
    {
        private IUserBll m_UserBll;
        public UserController()
        {
            m_UserBll = DPResolver.Resolver<IUserBll>();
        }

        public object GetPager(int page, int rows, int? begin, int? end, string name, string code, string storeName)
        {

            var pageResult = m_UserBll.GetUserPager(page, rows, begin, end, name, code, storeName);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        public object Post(SEC_User entity)
        {
            entity = m_UserBll.Add(entity);
            return entity;
        }


        public object Put(SEC_User entity)
        {
            entity = m_UserBll.Edit(entity);
            return entity;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                m_UserBll.LogicDelete(ids);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }

        [Route("api/user/GetAll")]
        public List<SEC_User> GetAll()
        {
            return m_UserBll.GetAll();
        }
    }
}
