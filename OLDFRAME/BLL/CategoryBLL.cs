using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using MPAPI.CommonLib;
using MPAPI.IBLL;
using MPAPI.IDAL;
using MPAPI.WebBase.Models;
using DefaultConnection;

namespace MPAPI.BLL
{
    public class CategoryBLL : CommonBLL, ICategoryBLL
    {
        public CategoryBLL()
        {

        }

        public IEnumerable<EasyuiTreeNode> GetCategoryItems(Guid? categoryId)
        {
            var allCategoryItems = FindAll<CategoryItem>(m => !categoryId.HasValue || m.CategoryTypeId == categoryId);
            return ConvertCategoryItemsToTree(allCategoryItems, null);
        }


        private IEnumerable<EasyuiTreeNode> ConvertCategoryItemsToTree(IEnumerable<CategoryItem> allCategoryItems, Guid? pId)
        {
            return allCategoryItems.Where(m => m.ParentId == pId).Select(m => new EasyuiTreeNode()
            {
                id = m.Id.ToString(),
                text = m.Content,
                state = "open",
                @checked = false,
                children = ConvertCategoryItemsToTree(allCategoryItems, m.Id)
            });
        }
    }
}
