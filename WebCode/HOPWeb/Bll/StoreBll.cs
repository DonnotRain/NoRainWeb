using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.IDal;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class StoreBll : CommonBLL, IStoreBll
    {
        IStoreDal m_dal;
        public StoreBll(IStoreDal dal)
        {
            m_dal = dal;
        }
        public PetaPoco.Page<DefaultConnection.SEC_Store> GetStorePager(int pageIndex, int pageSize, int? begin, int? end, string name, string address, string belongTo)
        {

            var sql = PetaPoco.Sql.Builder.Append(" select * from (SELECT s.*,(select Count(1)  From SEC_User U Where U.StoreID=s.ID) as UserCount FROM SEC_Store s")
                .Append("Where s.CorpCode=@0 ", SysContext.CorpCode);


            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And s.StoreName like @0", name);
            }

            if (!string.IsNullOrEmpty(address))
            {
                address = "%" + address + "%";
                sql.Append(" And s.Address like @0", address);
            }

            if (!string.IsNullOrEmpty(belongTo))
            {
                belongTo = "%" + belongTo + "%";
                sql.Append(" And s.BelongTo like @0", belongTo);
            }
            sql.Append(") A Where 1=1");

            //开始时间条件
            if (begin.HasValue)
            {
                sql.Append(" AND   UserCount >=@0", begin.Value);

            }
            //开始时间条件
            if (end.HasValue)
            {
                sql.Append(" And UserCount <=@0", end.Value);
            }

            var page = FindAllByPage<SEC_Store>(sql.SQL, pageSize, pageIndex, sql.Arguments);

            page.Items.ForEach(m =>
            {
                m.UserCount = m_dal.GetUserCount(m.Id);
            });
            return page;
        }


        public SEC_Store Add(SEC_Store entity)
        {
            entity.CorpCode = SysContext.CorpCode;
            entity.Insert();
            return entity;
        }

        public SEC_Store Edit(SEC_Store entity)
        {
             var oldEntity = SEC_Store.First("Where ID=@0", entity.Id);
             entity.CorpCode = oldEntity.CorpCode;
        
             entity.Update();
            return entity;
        }
    }
}
