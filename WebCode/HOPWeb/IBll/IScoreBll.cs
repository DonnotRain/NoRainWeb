using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DefaultConnection;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IScoreBll
    {
        PetaPoco.Page<TRN_ExamScore> GetPager(int? userId,  DateTime? accountTime, int page, int rows);

        List<TRN_ExamScore> GetExamScoreByUserId(int userId);

        void AddExamScore(TRN_ExamScore trn_ExamScore);
    }
}
