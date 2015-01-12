using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IItemBll : ICommonBLL
    {
        Page<MKT_Item> GetItemPager(int page, int pageSize, int? begin, int? end, string name, string code, string type);
        MKT_Item Add(MKT_Item entity);
        MKT_Item Edit(MKT_Item entity);

        void LogicDelete(string ids);
    }
}
