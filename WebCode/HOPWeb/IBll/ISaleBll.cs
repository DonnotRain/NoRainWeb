using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface ISaleBll : ICommonBLL
    {
        Page<MKT_Sell> GetSalePager(int page, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName);


        List<MKT_Sell> GetSellByName(string p,string name);

        MKT_Sell AddSell(HOP.Model.Request.SellContract model);

        List<CategoryItem> GetCategoryItems(string corpCode);

        object GetItems(string corpCode, string type, string barCode);
    }
}
