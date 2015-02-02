using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Antlr.Runtime;
using Security_ConnectionString;
using iBangTeam.Business.IBll;
using WebBase.Models;
using WebGrease.Css.Extensions;
using DefaultConnection;
using WebBase;

namespace BusinessWeb.Controllers.API
{
    public class CategoryItemController : ApiController
    {
        private readonly ICategoryBLL m_Bll = DPResolver.Resolver<ICategoryBLL>();

        // GET api/CategoryItem
        public IEnumerable<CategoryItem> Get()
        {
            return m_Bll.FindAll<CategoryItem>(null);
        }


        // GET api/CategoryItem
        public IEnumerable<EasyuiTreeNode> GetByCategory(Guid? categoryId)
        {
            return m_Bll.GetCategoryItems(categoryId);
        }

        // GET api/CategoryItem/5
        /// <summary>
        ///获取单个分类类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryItem Get(Guid id)
        {
            return m_Bll.Find<CategoryItem>("id=@0",id);
        }


        //[Route("API/CategoryItem/GetPager")]
        //public object GetCategoryItems(int rows, int page, string name, Guid? categoryId, Guid? parentId)
        //{
        //    if (name == null) name = "";

        //    var pager = m_Bll.FindAllByPage<CategoryItem, int>(
        //            m => (string.IsNullOrEmpty(name) || m.Content.Contains(name) || m.Code.Contains(name))
        //                && (!categoryId.HasValue || (m.CategoryTypeId == categoryId.Value))
        //            && ((!parentId.HasValue && (m.ParentId == null || m.ParentId.ToString() == new Guid().ToString())) || m.ParentId == parentId),
        //            m => m.Sort, rows, page);
        //    pager.ForEach(m =>
        //    {
        //        m.state = m_Bll.Find<CategoryItem>("ParentId=@0", m.Id) != null ? "closed" : "open";
        //    }
        //);

        //    return new { rows = pager.ToList(), total = pager.TotalItemCount };
        //}

        // POST api/CategoryItem
        public void Post(CategoryItem categoryItem)
        {
            if (string.IsNullOrEmpty(categoryItem.Content))
            {
                throw new ApiException(ResponseCode.SYSTEM_PARAMETER_REQUIRED, "缺少参数");
            }

            categoryItem.Id = Guid.NewGuid();

            m_Bll.Insert(categoryItem);
        }

        // PUT api/CategoryItem/5
        public void Put(CategoryItem categoryItem)
        {

            m_Bll.Update(categoryItem);
        }

        // DELETE api/CategoryItem/5
        public void Delete(Guid id)
        {
            m_Bll.Delete(Get(id));
        }

        [Route("API/CategoryItem/DeleteSome/{ids}")]
        public void DeleteSome(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<CategoryItem>();
                itemsStr.ForEach(m => items.Add(m_Bll.Find<CategoryItem>("id=@0", new Guid(m))));
                m_Bll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
