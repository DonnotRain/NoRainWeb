using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using iBangTeam.Business.IBll;
using DefaultConnection;
using WebBase;
using PetaPoco;

namespace BusinessWeb.Controllers.API
{
    public class CategoryController : ApiController
    {
        private ICommonBLL _mBaseBll = DPResolver.Resolver<ICommonBLL>();

        // GET api/CategoryType
        public IEnumerable<CategoryType> Get()
        {
            return _mBaseBll.FindAll<CategoryType>();
        }

        // GET api/CategoryType
        public object GetPager(int rows, int page, string name)
        {
            var pager = _mBaseBll.FindAllByPage<CategoryType>("Name LIKE @0 OR Code LIKE @0 ORDER BY Sort", page, rows, name);
            return new { total = pager, rows = pager.Items };
        }
        // GET api/CategoryType/5
        /// <summary>
        ///获取单个分类类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryType Get(Guid id)
        {
            return _mBaseBll.Find<CategoryType>("id=@0", id);
        }

        // POST api/CategoryType
        public void Post(CategoryType categoryType)
        {
            if (string.IsNullOrEmpty(categoryType.Name))
            {
                throw new ApiException(ResponseCode.SYSTEM_PARAMETER_REQUIRED, "缺少参数");
            }

            categoryType.Id = Guid.NewGuid();

            _mBaseBll.Insert(categoryType);
        }

        // PUT api/CategoryType/5
        public void Put(CategoryType categoryType)
        {

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
                itemsStr.ForEach(m => items.Add(_mBaseBll.Find<CategoryType>("id=@0", new Guid(m))));
                _mBaseBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
