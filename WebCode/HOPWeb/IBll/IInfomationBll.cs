using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IInfomationBll : ICommonBLL
    {
        Page<MKT_Infomation> GetInfomationPager(int page, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName);


        List<MKT_Infomation> GetInformationByName(string p,string title);

        MKT_Infomation AddInformation(HOP.Model.Request.InformationContract model);
    }
}
