using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuaweiSoftware.WQT.IBll;
using DefaultConnection;
using HuaweiSoftware.WQT.WebBase;

namespace HuaweiSoftware.WQT.Bll
{
    public class ScoreBll : CommonBLL, IScoreBll
    {
        /// <summary>
        /// 获取考试成绩
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountTime"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public PetaPoco.Page<TRN_ExamScore> GetPager(int? userId, DateTime? accountTime, int page, int rows)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM TRN_ExamScore").Append("WHERE CorpCode = @0", SysContext.CorpCode);

            if (userId.HasValue)
            {
                sql.Append("AND UserID=@0", userId.Value);
            }
            if (accountTime.HasValue)
            {
                sql.Append("AND DateDiff(day,CreateTime,@0)=0 AND DateDiff(year,CreateTime,@0)=0", accountTime.Value);
            }
            var trn_ExamScoreList = FindAllByPage<TRN_ExamScore>(sql.SQL, rows, page, sql.Arguments);
            trn_ExamScoreList.Items.ForEach((item) =>
            {
                var user = Find<SEC_User>("WHERE ID = @0", item.UserID);
                item.UserName = user.UserName;
                item.Code = user.Code;
            });

            trn_ExamScoreList.Items.ForEach((item) =>
            {
                var exam = Find<TRN_StudyExamArrange>("WHERE ID = @0", item.StudyExamArrangeID);
                item.Title = exam.Title;
            });

            return trn_ExamScoreList;
        }

        /// <summary>
        /// 通过用户ID获取考试成绩
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<TRN_ExamScore> GetExamScoreByUserId(int userId)
        {
            List<TRN_ExamScore> trn_ExamScoreList = FindAll<TRN_ExamScore>("WHERE UserID=@0", userId).ToList();
            trn_ExamScoreList.ForEach((item) =>
            {
                var user = Find<SEC_User>("WHERE ID = @0", item.UserID);
                item.UserName = user.UserName;
                item.Code = user.Code;
            });

            trn_ExamScoreList.ForEach((item) =>
            {
                var exam = Find<TRN_StudyExamArrange>("WHERE ID = @0", item.StudyExamArrangeID);
                item.Title = exam.Title;
            });


            return trn_ExamScoreList;
        }

        public void AddExamScore(TRN_ExamScore trn_ExamScore)
        {
            var sec_User = Find<SEC_User>("WHERE UserName = @0 AND CorpCode = @1", trn_ExamScore.UserName, trn_ExamScore.CorpCode);

            trn_ExamScore.UserID = sec_User.ID;
            trn_ExamScore.CreateTime = DateTime.Now;

            trn_ExamScore.Insert();
        }
    }
}
