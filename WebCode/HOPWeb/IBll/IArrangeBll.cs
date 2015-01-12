using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DefaultConnection;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IArrangeBll
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
        PetaPoco.Page<TRN_StudyExamArrange> GetPager(string title, int? type, int pageSize, int pageIndex);

        /// <summary>
        /// 增加学习
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        TRN_StudyExamArrange Add(TRN_StudyExamArrange trn_Study);

        /// <summary>
        /// 修改学习资料
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        TRN_StudyExamArrange Edit(TRN_StudyExamArrange trn_Study);

        /// <summary>
        /// 删除学习资料
        /// </summary>
        /// <param name="ids"></param>
        void Delete(string ids);

        /// <summary>
        /// 通过企业代号获取考试安排
        /// </summary>
        /// <param name="corpCode"></param>
        /// <returns></returns>
        List<TRN_StudyExamArrange> GetExamArrangeByCorpCode(string corpCode,string userName);

        /// <summary>
        /// 获取单个考试安排
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        TRN_StudyExamArrange GetArrangeDetail(int ID);
    }
}
