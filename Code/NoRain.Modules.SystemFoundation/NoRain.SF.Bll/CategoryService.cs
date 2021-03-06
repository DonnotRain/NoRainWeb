﻿using DefaultConnection;
using NoRain.Business.IService;
using NoRain.Business.Models;
using NoRain.Business.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoRain.Business.Service
{
    public class CategoryService : CommonService, ICategoryService
    {
        public CategoryService()
        {

        }

        public IEnumerable<EasyuiTreeNode> GetCategoryItems(Guid? categoryId)
        {
            var allCategoryItems = FindAll<CategoryItem>("Where CategoryTypeId=@0 or ''=@0", categoryId);
            return ConvertCategoryItemsToTree(allCategoryItems, null);
        }


        private IEnumerable<EasyuiTreeNode> ConvertCategoryItemsToTree(IEnumerable<CategoryItem> allCategoryItems, Guid? pId)
        {
            return allCategoryItems.Where(m => m.ParentId == pId).Select(m => new EasyuiTreeNode()
            {
                id = m.Id.ToString(),
                text = m.ItemContent,
                state = "open",
                @checked = false,
                children = ConvertCategoryItemsToTree(allCategoryItems, m.Id)
            });
        }


        public PetaPoco.Page<CategoryItem> GetItemsPager(int rows, int page, string name, Guid? categoryId, Guid? parentId)
        {
            var sql = Sql.Builder.Append("Where 1=1 ");

            //开始时间条件
            if (parentId.HasValue)
            {
                sql.Append(" And  ParentId =@0", parentId.Value);
            }
            else
            {
                sql.Append(" And  ParentId IS null");
            }
            //开始时间条件
            if (categoryId.HasValue)
            {
                sql.Append(" And CategoryTypeId =@0", categoryId.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And (Code like @0 OR Content like @0)", name);
            }
            sql.Append(" ORDER BY SORT ");
            var pager = FindAllByPage<CategoryItem>(sql.SQL, rows, page, sql.Arguments);

            pager.Items.ForEach(m =>
            {
                m.state = Find<CategoryItem>("WHERE ParentId=@0", m.Id) != null ? "closed" : "open";
                m.CategoryType = Find<CategoryType>("WHERE ID=@0", m.CategoryTypeId);
            });
            return pager;
        }


        public Page<CategoryType> GetCategoryPager(int rows, int page, string name)
        {
            var sql = Sql.Builder.Append("Where 1=1 ");

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And (Code like @0 OR Name like @0)", name);
            }

            sql.Append(" ORDER BY SORT ");
            var pager = FindAllByPage<CategoryType>(sql.SQL, rows, page, sql.Arguments);

            return pager;
        }

        public List<CategoryItem> GetItems(string categoryCode, Guid? parentId)
        {
            var catogory = Find<CategoryType>("Where Code=@0", categoryCode);

            var sql = Sql.Builder.Append(" Where 1=1 ");

            //开始时间条件
            if (parentId.HasValue)
            {
                sql.Append(" And  ParentId =@0", parentId.Value);
            }
            else
            {
                sql.Append(" And  ParentId IS null");
            }

            sql.Append(" And CategoryTypeId =@0", catogory.Id);

            sql.Append(" ORDER BY SORT ");
            var items = FindAll<CategoryItem>(sql.SQL, sql.Arguments).ToList();

            items.ForEach(m =>
            {
                m.state = Find<CategoryItem>("WHERE ParentId=@0", m.Id) != null ? "closed" : "open";
                m.CategoryType = Find<CategoryType>("WHERE ID=@0", m.CategoryTypeId);
            });
            return items;
        }

        public void AddCategory(CategoryType category)
        {
            //检查必要参数
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new ApiException(ResponseCode.必须参数缺少, "缺少参数", "名称（Name）");
            }

            if (string.IsNullOrEmpty(category.Code))
            {
                throw new ApiException(ResponseCode.必须参数缺少, "缺少参数", "编号（Code）");
            }
            //生成Id
            category.Id = Guid.NewGuid();

            //判断名称和编号是否有重复
            var items = FindAll<CategoryType>("WHERE Name=@0 or Code=@1", category.Name, category.Code);

            if (items.Count() != 0)
            {
                throw new ApiException(ResponseCode.参数值重复, "请检查编号和名称", "编号（Code）名称（Name）");
            }
            Insert(category);
        }
    }
}
