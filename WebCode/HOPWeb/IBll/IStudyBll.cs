using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DefaultConnection;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IStudyBll
    {
        /// <summary>
        /// 获取学习资料列表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        PetaPoco.Page<TRN_Study> GetPager(string title, int? type, DateTime? beginTime,DateTime? endTime, int pageSize, int pageIndex);

        /// <summary>
        /// 增加学习
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        TRN_Study Add(TRN_Study trn_Study);

        /// <summary>
        /// 修改学习资料
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        TRN_Study Edit(TRN_Study trn_Study);

        /// <summary>
        /// 删除学习资料
        /// </summary>
        /// <param name="ids"></param>
        void Delete(string ids);

        /// <summary>
        /// 获取所有学习资料
        /// </summary>
        /// <returns></returns>
        object GetAll();

        /// <summary>
        /// 获取学习资料
        /// </summary>
        /// <returns></returns>
        List<TRN_Study> GetStudyByCorp(string corpCode,string title);

        /// <summary>
        /// 获取学习资料详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        TRN_Study GetStudyDetail(int ID);
    }
}
