using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using iBangTeam.Business.IBll;
using Security_ConnectionString;
using WebBase;

namespace BusinessWeb.Controllers.API
{
    public class OperatorController : ApiController
    {
        private ICommonBLL _mBaseBll = DPResolver.Resolver<ICommonBLL>();

        // GET api/Operator
        public IEnumerable<Operator> Get()
        {
            return _mBaseBll.FindAll<Operator>(null);
        }

        // GET api/Operator/5
        /// <summary>
        ///获取单个操作员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Operator Get(Guid id)
        {
            return _mBaseBll.Find<Operator>("id=@0", id);
        }

        [System.Web.Http.Route("API/OperatorPager")]
        public object GetPager(int page, int rows)
        {
            var pageList = _mBaseBll.FindAllByPage<Operator>("", rows, page);
            return new { rows = pageList.Items, total = pageList.TotalItems };
        }

        // POST api/Operator
        public void Post(Operator Operator)
        {
            //if (string.IsNullOrEmpty(Operator.Name))
            //{
            //    throw new ApiException(ResponseCode.SYSTEM_PARAMETER_REQUIRED, "缺少参数");
            //}

            //Operator.ID = Guid.NewGuid();

            //_mBaseBll.Insert(Operator);
        }

        // PUT api/Operator/5
        public void Put(Operator Operator)
        {

            _mBaseBll.Update(Operator);
        }



        // DELETE api/Operator/5
        public void Delete(Guid id)
        {
            _mBaseBll.Delete(Get(id));
        }

        [System.Web.Http.Route("API/Operator/DeleteSome/{ids}")]
        public void DeleteSome(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<Operator>();
                itemsStr.ForEach(m => items.Add(Get(new Guid(m))));
                _mBaseBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
