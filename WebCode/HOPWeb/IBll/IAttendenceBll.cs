using DefaultConnection;
using HuaweiSoftware.HOP.Model.Request;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IAttendenceBll : ICommonBLL
    {
        Page<ATD_Attendence> GetAttendencePager(int page, int pageSize, DateTime? beginDate, DateTime? endDate, string state);


        /// <summary>
        /// 移动端
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<HOP.Model.AttendenceInfo> GetAttendenceByName(string userName);
        
        /// <summary>
        /// 移动端
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        ATD_Attendence AddAttendence(AttendenceContract model);
    }
}
