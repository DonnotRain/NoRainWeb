using DefaultConnection;
using HuaweiSoftware.WQT.IDal;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Dal
{
    public class RequestDal : CommonDAL, IRequestDal
    {
        public List<DefaultConnection.MKT_Request> GetOtherInfo(List<DefaultConnection.MKT_Request> srcItems)
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

                if (m.FileID > 0)
                {
                    var file = Find<FileItem>("Where ID=@0", m.FileID);
                    m.Files = new List<FileItem>();
                    m.Files.Add(file);
                }
            });

            return srcItems;
        }
    }
}
