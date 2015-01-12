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
    public class InfomationBll : CommonBLL, IInfomationBll
    {
        private IInfomationDal m_infomationDal;
        public InfomationBll(IInfomationDal dal)
        {
            m_infomationDal = dal;
        }
        public PetaPoco.Page<DefaultConnection.MKT_Infomation> GetInfomationPager(int pageIndex, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName)
        {

            List<object> paramaters = new List<object>();

            var sql = PetaPoco.Sql.Builder.Append("SELECT s.*,ss.StoreName,u.UserName FROM MKT_Infomation s")
   .LeftJoin("SEC_User u").On("s.UserId=u.Id").LeftJoin("SEC_Store ss")
    .On("ss.ID=u.StoreId").Append("Where s.CorpCode=@0 ", SysContext.CorpCode);

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

            var page = FindAllByPage<MKT_Infomation>(sql.SQL, pageSize, pageIndex, sql.Arguments);
            m_infomationDal.GetOtherInfo(page.Items);
            return page;
        }


        public List<MKT_Infomation> GetInformationByName(string p, string title)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM MKT_Infomation").Append("WHERE UserId=@0", user.ID);
            if (title != null)
            {
                title = "%" + title + "%";
                sql.Append("AND (Title LIKE @0 OR Detail LIKE @0)", title);
            }
            sql.Append("ORDER BY CreateTime DESC");
            var items = FindAll<MKT_Infomation>(sql.SQL, sql.Arguments);
            return items.ToList();
        }

        public MKT_Infomation AddInformation(HOP.Model.Request.InformationContract model)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);

            MKT_Infomation infomationInfo = new MKT_Infomation();
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
