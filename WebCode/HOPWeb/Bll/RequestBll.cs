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
    public class RequestBll : CommonBLL, IRequestBll
    {
        private IRequestDal m_requestDal;
        public RequestBll(IRequestDal dal)
        {
            m_requestDal = dal;
        }

        public PetaPoco.Page<DefaultConnection.MKT_Request> GetRequestPager(int pageIndex, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName)
        {

            List<object> paramaters = new List<object>();

            var sql = PetaPoco.Sql.Builder.Append("SELECT s.*,ss.StoreName,u.UserName FROM MKT_Request s")
   .LeftJoin("SEC_User u").On("s.UserId=u.Id").LeftJoin("SEC_Store ss")
    .On("ss.ID=u.StoreId").Append("Where 1=1 ");

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And  s.CreateTime >=@0", beginDate.Value);

            }
            //开始时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And s.CreateTime <=@0", endDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And s.Title like @0", name);
            }


            if (!string.IsNullOrEmpty(storeName))
            {
                storeName = "%" + storeName + "%";
                sql.Append(" And ss.StoreName like @0", storeName);
            }

            sql.OrderBy(" s.CreateTime Desc");

            var page = FindAllByPage<MKT_Request>(sql.SQL, pageSize, pageIndex, sql.Arguments);

            m_requestDal.GetOtherInfo(page.Items);

            return page;
        }


        public List<MKT_Request> GetRequestByName(string p, string title)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM MKT_Request").Append("WHERE UserId=@0", user.ID);
            if (title != null)
            {
                title = "%" + title + "%";
                sql.Append("AND (Title LIKE @0 OR Detail LIKE @0)", title);
            }
            sql.Append("ORDER BY CreateTime DESC");
            var items = FindAll<MKT_Request>(sql.SQL, sql.Arguments);
            return items.ToList();
        }

        public MKT_Request AddRequest(HOP.Model.Request.RequestContract model)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);

            MKT_Request infomationInfo = new MKT_Request();
            infomationInfo.Title = model.title;
            infomationInfo.Detail = model.detail;
            infomationInfo.Creator = model.creator;
            infomationInfo.CreateTime = DateTime.Now;
            infomationInfo.FileID = model.fileID;
            infomationInfo.CorpCode = SysContext.CorpCode;
            infomationInfo.UserId = (int)user.ID;

            Insert(infomationInfo);

            return infomationInfo;
        }
    }
}
