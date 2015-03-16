﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Antlr.Runtime;
using DefaultConnection;
using NoRain.Business.IService;
using NoRain.Business.WebBase;
using NoRain.Business.Service;
using MainWeb.Filters;
using NoRain.Business.Models;
using NoRain.Business.Model.Request;
using NoRain.Business.Model.Response;
using AttributeRouting.Web.Http;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class CategoryItemController : ApiController
    {
        private readonly ICategoryService m_Service;


        public CategoryItemController()
        {
            m_Service = new CategoryService(); //DPResolver.Resolver<ICategoryService>();
        }

        // GET api/CategoryItem
        public IEnumerable<CategoryItem> Get()
        {
            return m_Service.FindAll<CategoryItem>(null);
        }


        // GET api/CategoryItem
        public IEnumerable<EasyuiTreeNode> GetByCategory(Guid? categoryId)
        {
            return m_Service.GetCategoryItems(categoryId);
        }

        // GET api/CategoryItem
        public IEnumerable<CategoryItem> GetByCategory(string categoryCode, Guid? parentId)
        {
            return m_Service.GetItems(categoryCode, parentId);
        }
        // GET api/CategoryItem/5
        /// <summary>
        ///获取单个分类类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryItem Get(Guid id)
        {
            return m_Service.Find<CategoryItem>("Where ID=@0", id);
        }
        [GET("API/CategoryItem/GetPager")]
        public object GetCategoryItems(int rows, int page, string name, Guid? categoryId, Guid? parentId)
        {
            if (name == null) name = "";

            var pager = m_Service.GetItemsPager(rows, page, name, categoryId, parentId);

            return new { rows = pager.Items, total = pager.TotalItems };
        }
        [GET("API/CategoryItem/DataTablePager")]
        public object GetCategoryItems([FromUri]DataTablesRequest reqestParams, [FromUri]CategoryItemPagerCondition condition)
        {
            var pageResult = m_Service.GetItemsPager(reqestParams.length, reqestParams.start / reqestParams.length + 1, condition.Name, condition.CategoryId, condition.ParentId);

            return new DataTablePager<CategoryItem>()
            {
                draw = reqestParams.draw,
                recordsTotal = pageResult.TotalItems,
                recordsFiltered = pageResult.TotalItems,
                data = pageResult.Items
            };
        }
        // POST api/CategoryItem
        public void Post(CategoryItem categoryItem)
        {
            if (string.IsNullOrEmpty(categoryItem.ItemContent))
            {
                throw new ApiException(ResponseCode.必须参数缺少, "缺少参数", "参数值(ItemContent)");
            }

            categoryItem.Id = Guid.NewGuid();
            m_Service.Insert(categoryItem);
        }

        // PUT api/CategoryItem/5
        public void Put(CategoryItem categoryItem)
        {
            var oldCategoryItem = m_Service.Find<CategoryItem>("Where ID=@0", categoryItem.Id);
       
            m_Service.Update(categoryItem);
        }

        // DELETE api/CategoryItem/5
        public void Delete(Guid id)
        {
            m_Service.Delete(Get(id));
        }

        public void DeleteSome([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<CategoryItem>();
                itemsStr.ForEach(m => items.Add(m_Service.Find<CategoryItem>("Where ID=@0", new Guid(m))));
                m_Service.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}