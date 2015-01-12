using DefaultConnection;
using HuaweiSoftware.WQT.IDal;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Dal
{
    public class ReturnDal : CommonDAL, IReturnDal
    {
        public List<DefaultConnection.MKT_Return> GetOtherInfo(List<DefaultConnection.MKT_Return> srcItems)
        {
            srcItems.ForEach(m =>
            {
                var user = Find<SEC_User>("Where Id=@0", m.UserId);
                if (user == null)
                {
                    m.Creator = "无相关用户";
                    m.StoreName = "无相关用户";
                }
                else
                {
                    var store = Find<SEC_Store>("Where ID=@0", user.StoreId);
                    m.StoreName = store != null ? store.StoreName : "无相关门店";
                    m.Creator = user.UserName;
                }
                var item = Find<MKT_Item>("Where Code=@0", m.Code);

                m.Name = item.Name;
                m.Type = item.Type;
                m.Barcode = item.Barcode;

                var itemType = CategoryItem.FirstOrDefault("WHERE CODE=@0", item.Type);
                m.TypeName = itemType != null ? itemType.Content : "(未找到相关类型)";
            });

            return srcItems;
        }
    }
}
