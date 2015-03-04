using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DefaultConnection;
using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using MainWeb.Filters;
using NoRain.Business.Model.Request;
using NoRain.Business.Model.Response;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class CategoryController : ApiController
    {
        private ICategoryService _categoryService;


        public CategoryController()
        {
            _categoryService = DPResolver.Resolver<ICategoryService>();
        }

        // GET api/CategoryType
        public IEnumerable<CategoryType> Get()
        {
            return _categoryService.FindAll<CategoryType>("");
        }

        // GET api/CategoryType
        public object GetPager(int rows, int page, string name)
        {
            var pager = _categoryService.GetCategoryPager(rows, page, name);
            return new { total = pager.TotalItems, rows = pager.Items };
        }
        [Route("API/Category/DataTablePager")]
        public object GetCategoryItems([FromUri]DataTablesRequest reqestParams, [FromUri]CategoryItemPagerCondition condition)
        {
            var pageResult = _categoryService.GetCategoryPager(reqestParams.length, reqestParams.start / reqestParams.length + 1, condition.Name);

            return new DataTablePager<CategoryType>()
            {
                draw = reqestParams.draw,
                recordsTotal = pageResult.TotalItems,
                recordsFiltered = pageResult.TotalItems,
                data = pageResult.Items
            };
        }

        public CategoryType Get(Guid id)
        {
            return _categoryService.Find<CategoryType>("Where ID=@0", id);
        }

      
        public void Post(CategoryType categoryType)
        {
            _categoryService.AddCategory(categoryType);
        }

        public void Put(CategoryType categoryType)
        {
            var oldCategoryType = _categoryService.Find<CategoryType>("Where ID=@0", categoryType.Id);

            _categoryService.Update(categoryType);
        }

        public void Delete(Guid id)
        {
            _categoryService.Delete(Get(id));
        }

        public void DeleteSome([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<CategoryType>();
                itemsStr.ForEach(m => items.Add(_categoryService.Find<CategoryType>("Where id=@0", new Guid(m))));
                _categoryService.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
