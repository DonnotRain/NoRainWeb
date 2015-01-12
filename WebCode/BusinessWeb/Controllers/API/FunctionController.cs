using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using  Security_ConnectionString;
using iBangTeam.Business.IBll;
using WebBase;

namespace BusinessWeb.Controllers.API
{
    public class FunctionController : ApiController
    {
        private IFunctionBLL m_functionBll = DPResolver.Resolver<IFunctionBLL>();
        private ICommonBLL m_commonBll = DPResolver.Resolver<ICommonBLL>();

        public IEnumerable<Function> Get()
        {
            return m_functionBll.GetFunctions();
        }

        // GET api/Function/5
        /// <summary>
        ///获取单个单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Function Get(int id)
        {
            return m_commonBll.Find<Function>("id=@0",id);
        }

        // POST api/Function

        public void Post(Function sysFunction)
        {
            if (string.IsNullOrEmpty(sysFunction.Name))
            {
                throw new ApiException(ResponseCode.SYSTEM_PARAMETER_REQUIRED, "缺少参数");
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
            m_commonBll.Delete(Get(id));
        }

        [Route("API/Function/DeleteSome/{ids}")]
        public void DeleteSome(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<Function>();
                itemsStr.ForEach(m => items.Add(Get(int.Parse(m))));
                m_commonBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
