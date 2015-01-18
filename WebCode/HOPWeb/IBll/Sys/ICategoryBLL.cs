﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoRain.Business.WebBase;
using PetaPoco;
using DefaultConnection;

namespace NoRain.Business.IBll
{
    public interface ICategoryBLL : ICommonBLL
    {
        IEnumerable<EasyuiTreeNode> GetCategoryItems(Guid? categoryId);

        Page<CategoryItem> GetItemsPager(int rows, int page, string name, Guid? categoryId, Guid? parentId);

        List<CategoryItem> GetItems(string categoryCode, Guid? parentId);

        Page<CategoryType> GetCategoryPager(int rows, int page, string name);
    }
}
