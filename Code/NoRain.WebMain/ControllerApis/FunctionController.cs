using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

using MainWeb.Filters;
using NoRain.Business.Models;
using DefaultConnection;
using AttributeRouting.Web.Http;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class FunctionController : ApiController
    {

        private IFunctionBLL m_functionBll = DPResolver.Resolver<IFunctionBLL>();
        private IRoleBLL m_roleBll = DPResolver.Resolver<IRoleBLL>();

        public IEnumerable<Function> Get()
        {
            return m_functionBll.GetFunctions();
        }

        [GET("API/Function/GetByRole")]
        public IEnumerable<JsTreeNode> GetRoles(int id)
        {
            return m_roleBll.GetRoleFunctions(id);
        }

        [GET("API/Function/AlljsTreeData")]
        public IEnumerable<JsTreeNode> GetAllJsTreeData([FromUri]string someParam)
        {
            var result = m_functionBll.GetAllJsTreeData();

            return result;
        }

        // GET api/Function/5
        /// <summary>
        ///获取单个单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Function Get(int id)
        {
            return m_functionBll.Find<Function>("where id=@0", id);
        }

        // POST api/Function

        public void Post(Function sysFunction)
        {
            if (string.IsNullOrEmpty(sysFunction.Name))
            {
                throw new ApiException(ResponseCode.必须参数缺少, "缺少参数");
            }

            m_functionBll.InsertFunction(sysFunction);
        }

        // PUT api/Function/5
        public void Put(Function sysFunction)
        {
            m_functionBll.UpdateFunction(sysFunction);
        }

        // DELETE api/SysFunction/5
        public void Delete(int id)
        {
            m_functionBll.Delete(Get(id));
        }

        [GET("API/Function/DeleteSome/{ids}")]
        public void DeleteSome(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<Function>();
                itemsStr.ForEach(m => items.Add(Get(int.Parse(m))));
                m_functionBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
