using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DefaultConnection;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IExamBll
    {
        PetaPoco.Page<TRN_Exam> GetPager(string title, int? type, int page, int rows);
        TRN_Exam Add(TRN_Exam trn_Exam);
        TRN_Exam Edit(TRN_Exam trn_Exam);
        void Delete(string ids);
        object GetAll();
        List<TRN_Exam> GetExamByCorp(string corpCode);
        TRN_Judge GetExamJudge(int ExamID, int seq);
        TRN_Choice GetExamChoice(int ExamID, int seq);
    }
}
