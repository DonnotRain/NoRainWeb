using DefaultConnection;
using HuaweiSoftware.HOP.Model.Request;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.IDal;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class ReturnBll : CommonBLL, IReturnBll
    {
        private IReturnDal m_returnDal;
        public ReturnBll(IReturnDal dal)
        {
            m_returnDal = dal;
        }
        public PetaPoco.Page<DefaultConnection.MKT_Return> GetReturnPager(int pageIndex, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName)
        {

            List<object> paramaters = new List<object>();

            var sql = PetaPoco.Sql.Builder.Append("SELECT s.*,ss.StoreName,u.UserName FROM MKT_Return s")
   .LeftJoin("SEC_User u").On("s.UserId=u.Id").LeftJoin("SEC_Store ss")
    .On("ss.ID=u.StoreId").LeftJoin("MKT_Items IT").On("S.CODE=IT.Code").Append("Where 1=1 ");

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And  s.Time >=@0", beginDate.Value);

            }
            //开始时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And s.Time <=@0", endDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And IT.Name like @0", name);
            }

            if (!string.IsNullOrEmpty(code))
            {
                code = "%" + code + "%";
                sql.Append(" And s.Code like @0", code);
            }

            if (!string.IsNullOrEmpty(storeName))
            {
                storeName = "%" + storeName + "%";
                sql.Append(" And ss.StoreName like @0", storeName);
            }

            sql.OrderBy("Time Desc");

            var page = FindAllByPage<MKT_Return>(sql.SQL, pageSize, pageIndex, sql.Arguments);

            //获取额外的信息
            m_returnDal.GetOtherInfo(page.Items);

            return page;
        }



        public List<MKT_Return> GetReturnByName(string p, string name)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM MKT_Return").Append("WHERE UserId=@0", user.ID);

            sql.Append("ORDER BY Time DESC");
            var items = FindAll<MKT_Return>(sql.SQL, sql.Arguments);

            items = m_returnDal.GetOtherInfo(items.ToList());
            if (name != null)
            {
                items = items.Where(m => m.Name.Contains(name));
            }

            return items.ToList();
        }

        public MKT_Return AddReturn(ReturnContract model)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);

            MKT_Return infomationInfo = new MKT_Return();
            infomationInfo.Code = model.code;
            infomationInfo.Amount = Convert.ToInt32(model.amount);
            infomationInfo.Reason = model.reason;

            infomationInfo.Time = DateTime.Now;

            infomationInfo.CorpCode = SysContext.CorpCode;
            infomationInfo.UserId = (int)user.ID;

            Insert(infomationInfo);

            return infomationInfo;
        }
    }
}
