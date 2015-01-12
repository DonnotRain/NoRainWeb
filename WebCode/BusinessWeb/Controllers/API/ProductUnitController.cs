using System.Web.Mvc;
using  Security_ConnectionString;
using iBangTeam.Business.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using PagedList;
using DefaultConnection;
using WebBase;
namespace BusinessWeb.Controllers.API
{
    public class ProductUnitController : ApiController
    {
        private ICommonBLL _mBaseBll = DPResolver.Resolver<ICommonBLL>();

        // GET api/productUnit
        public IEnumerable<ProductUnit> Get()
        {
            return _mBaseBll.FindAll<ProductUnit>(null);
        }

        // GET api/productUnit/5
        /// <summary>
        ///获取单个单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductUnit Get(Guid id)
        {
            return _mBaseBll.Find<ProductUnit>("id=@0", id);
        }

        [System.Web.Http.Route("API/ProductUnitPager")]
        public object GetPager(int page, int rows)
        {
            var pageList = _mBaseBll.FindAllByPage<ProductUnit>(null,  rows, page);
            return new { rows = pageList.Items, total = pageList.TotalItems };
        }

        // POST api/productUnit
        public void Post(ProductUnit productUnit)
        {
            if (string.IsNullOrEmpty(productUnit.Name))
            {
                throw new ApiException(ResponseCode.SYSTEM_PARAMETER_REQUIRED, "缺少参数");
            }

            productUnit.Id = Guid.NewGuid();

            _mBaseBll.Insert(productUnit);
        }

        // PUT api/productUnit/5
        public void Put(ProductUnit productUnit)
        {
            //    var entity = Get(productUnit.Id);

            _mBaseBll.Update(productUnit);
        }

        // DELETE api/productUnit/5
        public void Delete(Guid id)
        {
            _mBaseBll.Delete(Get(id));
        }

        [System.Web.Http.Route("API/ProductUnit/DeleteSome/{ids}")]
        public void Delete(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<ProductUnit>();
                itemsStr.ForEach(m =>
                {
                    items.Add(Get(new Guid(m)));
                });
                _mBaseBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
