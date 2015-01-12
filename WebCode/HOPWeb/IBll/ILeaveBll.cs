using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface ILeaveBll : ICommonBLL
    {
         Page<ATD_Leave> GetLeavePager(int page, int pageSize, DateTime? beginDate, DateTime? endDate, string state);


         List<HOP.Model.LeaveInfo> GetLeaveData(string userName);

         bool AddLeave(HOP.Model.Request.LeaveContract leave);

         void PutLeave(bool isPass, int ID);
    }
}
