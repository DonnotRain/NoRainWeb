using HuaweiSoftware.WQT.IDal;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Dal
{
    public class StoreDal : CommonDAL, IStoreDal
    {
        public int GetUserCount(long storeId)
        {
            var sql = Sql.Builder.Append("select count(1) UserCount from SEC_User Where StoreID=@0", storeId);
            return this.DB.ExecuteScalar<int>(sql);
        }
    }
}
