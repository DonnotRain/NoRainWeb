using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IRequestBll : ICommonBLL
    {
        Page<MKT_Request> GetRequestPager(int page, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName);


        MKT_Request AddRequest(HOP.Model.Request.RequestContract model);

        List<MKT_Request> GetRequestByName(string p, string title);
    }
}
