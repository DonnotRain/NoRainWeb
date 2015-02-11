using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DefaultConnection;
using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using MainWeb.Filters;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class CategoryController : ApiController
    {
        private ICategoryBLL _mBaseBll;


        public CategoryController()
        {
            _mBaseBll = DPResolver.Resolver<ICategoryBLL>();
        }

        // GET api/CategoryType
        public IEnumerable<CategoryType> Get()
        {
            return _mBaseBll.FindAll<CategoryType>("");
        }

        // GET api/CategoryType
        public object GetPager(int rows, int page, string name)
        {
            var pager = _mBaseBll.GetCategoryPager(rows, page, name);
            return new { total = pager.TotalItems, rows = pager.Items };
        }
        // GET api/CategoryType/5
        /// <summary>
        ///获取单个分类类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryType Get(Guid id)
        {
            return _mBaseBll.Find<CategoryType>("Where ID=@0", id);
        }

        // POST api/CategoryType
        public void Post(CategoryType categoryType)
        {
            if (string.IsNullOrEmpty(categoryType.Name))
            {
                throw new ApiException(ResponseCode.必须参数缺少, "缺少参数");
            }

            categoryType.Id = Guid.NewGuid();
            _mBaseBll.Insert(categoryType);
        }

        // PUT api/CategoryType/5
        public void Put(CategoryType categoryType)
        {
            var oldCategoryType = _mBaseBll.Find<CategoryType>("Where ID=@0", categoryType.Id);
         
            _mBaseBll.Update(categoryType);
        }

        // DELETE api/CategoryType/5
        public void Delete(Guid id)
        {
            _mBaseBll.Delete(Get(id));
        }

        [Route("API/CategoryType/DeleteSome/{ids}")]
        public void DeleteSome(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<CategoryType>();
                itemsStr.ForEach(m => items.Add(_mBaseBll.Find<CategoryType>("Where id=@0", new Guid(m))));
                _mBaseBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
