using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IReturnBll : ICommonBLL
    {
        Page<MKT_Return> GetReturnPager(int page, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName);


        List<MKT_Return> GetReturnByName(string p,string name);

        MKT_Return AddReturn(HOP.Model.Request.ReturnContract model);
    }
}
