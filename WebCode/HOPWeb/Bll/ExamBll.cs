using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuaweiSoftware.WQT.IBll;
using DefaultConnection;
using HuaweiSoftware.WQT.WebBase;

namespace HuaweiSoftware.WQT.Bll
{
    public class ExamBll : CommonBLL, IExamBll
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public PetaPoco.Page<TRN_Exam> GetPager(string title, int? type, int page, int rows)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM TRN_Exam").Append("WHERE CorpCode=@0", SysContext.CorpCode);
            if (!string.IsNullOrEmpty(title))
            {
                title = "%" + title + "%";
                sql.Append("AND Title like @0", title);
            }
            if (type.HasValue && type.Value != 0)
            {
                sql.Append("AND Type=@0", type.Value);
            }

            var pages = FindAllByPage<TRN_Exam>(sql.SQL, rows, page, sql.Arguments);
            pages.Items.ForEach((m) =>
            {
                m.FileName = Find<FileItem>("WHERE ID=@0", m.FileID).FileName;
                m.Judge = FindAll<TRN_Judge>("WHERE ExamID=@0", m.ID)!=null?FindAll<TRN_Judge>("WHERE ExamID=@0", m.ID).ToList():null;
                m.Choice = FindAll<TRN_Choice>("WHERE ExamID=@0", m.ID)!=null?FindAll<TRN_Choice>("WHERE ExamID=@0", m.ID).ToList():null;
                if (FindAll<TRN_StudyExam>("WHERE ExamID=@0", m.ID) != null && FindAll<TRN_StudyExam>("WHERE ExamID=@0", m.ID).ToList().Count > 0)
                {
                    List<int> studyID = new List<int>();
                    List<TRN_StudyExam> trn_StudyExams = FindAll<TRN_StudyExam>("WHERE ExamID=@0", m.ID).ToList();
                    foreach (TRN_StudyExam item in trn_StudyExams)
                    {
                        studyID.Add(Convert.ToInt32(item.StudyID));
                    }
                    m.TRN_StudyID = studyID;
                }
            });

            return pages;
        }

        /// <summary>
        /// 增加考试
        /// </summary>
        /// <param name="trn_Exam">考试实体</param>
        /// <returns></returns>
        public TRN_Exam Add(TRN_Exam trn_Exam)
        {
            using (PetaPoco.Transaction transaction = new PetaPoco.Transaction(new DefaultConnectionDB()))
            {
                trn_Exam.CorpCode = SysContext.CorpCode;
                int examID = Convert.ToInt32(trn_Exam.Insert());
                if (trn_Exam.Judge != null)
                {
                    foreach (TRN_Judge item in trn_Exam.Judge)
                    {
                        item.CorpCode = SysContext.CorpCode;
                        item.ExamID = examID;
                        item.Insert();
                    }
                }
                if (trn_Exam.Choice != null)
                {
                    foreach (TRN_Choice item in trn_Exam.Choice)
                    {
                        item.CorpCode = SysContext.CorpCode;
                        item.ExamID = examID;
                        item.Insert();
                    }
                }
                if (trn_Exam.TRN_StudyID.HasItems())
                {
                    foreach (int? item in trn_Exam.TRN_StudyID.ToList())
                    {
                        TRN_StudyExam trn_StudyExam = new TRN_StudyExam();
                        trn_StudyExam.ExamID = examID;
                        trn_StudyExam.StudyID = item.Value;
                        trn_StudyExam.CorpCode = SysContext.CorpCode;

                        trn_StudyExam.Insert();
                    }
                }
                transaction.Complete();
            }

            return trn_Exam;
        }

        /// <summary>
        /// 修改考试
        /// </summary>
        /// <param name="trn_Exam">考试实体</param>
        /// <returns></returns>
        public TRN_Exam Edit(TRN_Exam trn_Exam)
        {
            using (PetaPoco.Transaction transaction = new PetaPoco.Transaction(new DefaultConnectionDB()))
            {
                trn_Exam.CorpCode = SysContext.CorpCode;
                int examID = Convert.ToInt32(trn_Exam.ID);
                trn_Exam.Update();

                // 删除原有的数据
                List<TRN_Judge> judge = FindAll<TRN_Judge>("WHERE ExamID=@0", examID)!=null?FindAll<TRN_Judge>("WHERE ExamID=@0", examID).ToList():null;
                List<TRN_Choice> choice = FindAll<TRN_Choice>("WHERE ExamID=@0", examID)!=null?FindAll<TRN_Choice>("WHERE ExamID=@0", examID).ToList():null;
                List<TRN_StudyExam> studyExams = FindAll<TRN_StudyExam>("WHERE ExamID=@0", examID)!=null?FindAll<TRN_StudyExam>("WHERE ExamID=@0", examID).ToList():null;
                foreach (TRN_Judge item in judge)
                {
                    item.Delete();
                }
                foreach (TRN_Choice item in choice)
                {
                    item.Delete();
                }
                foreach (TRN_StudyExam item in studyExams)
                {
                    item.Delete();
                }

                if (trn_Exam.Judge != null)
                {
                    foreach (TRN_Judge item in trn_Exam.Judge)
                    {
                        item.CorpCode = SysContext.CorpCode;
                        item.ExamID = examID;
                        item.Insert();
                    }
                }
                if (trn_Exam.Choice != null)
                {
                    foreach (TRN_Choice item in trn_Exam.Choice)
                    {
                        item.CorpCode = SysContext.CorpCode;
                        item.ExamID = examID;
                        item.Insert();
                    }
                }
                if (trn_Exam.TRN_StudyID.HasItems())
                {
                    foreach (int? item in trn_Exam.TRN_StudyID.ToList())
                    {
                        TRN_StudyExam trn_StudyExam = new TRN_StudyExam();
                        trn_StudyExam.ExamID = examID;
                        trn_StudyExam.StudyID = item.Value;
                        trn_StudyExam.CorpCode = SysContext.CorpCode;

                        trn_StudyExam.Insert();
                    }
                }
                transaction.Complete();
            }

            return trn_Exam;
        }

        /// <summary>
        /// 删除考试
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string ids)
        {
            string[] idList = ids.Split(',');
            foreach (string id in idList)
            {
                using (PetaPoco.Transaction transaction = new PetaPoco.Transaction(new DefaultConnectionDB()))
                {
                    int examID = Convert.ToInt32(id);
                    var trn_Exam = Find<TRN_Exam>("WHERE Id=@0", examID);
                    // 删除原有的数据
                    List<TRN_Judge> judge = FindAll<TRN_Judge>("WHERE ExamID=@0", examID) != null ? FindAll<TRN_Judge>("WHERE ExamID=@0", examID).ToList() : null;
                    List<TRN_Choice> choice = FindAll<TRN_Choice>("WHERE ExamID=@0", examID) != null ? FindAll<TRN_Choice>("WHERE ExamID=@0", examID).ToList() : null;
                    List<TRN_StudyExam> studyExams = FindAll<TRN_StudyExam>("WHERE ExamID=@0", examID) != null ? FindAll<TRN_StudyExam>("WHERE ExamID=@0", examID).ToList() : null;
                    foreach (TRN_Judge item in judge)
                    {
                        item.Delete();
                    }
                    foreach (TRN_Choice item in choice)
                    {
                        item.Delete();
                    }
                    foreach (TRN_StudyExam item in studyExams)
                    {
                        item.Delete();
                    }

                    trn_Exam.Delete();

                    transaction.Complete();
                }
            }
        }

        public object GetAll()
        {
            DateTime nowTime = DateTime.Now;
            var pages = FindAll<TRN_Exam>("WHERE CorpCode=@0 AND ((BeginTime <=@1 AND ExpiredTime >= @1) OR HasValidateTime='false')", SysContext.CorpCode, nowTime);

            return pages;
        }

        /// <summary>
        /// 获取考试
        /// </summary>
        /// <returns></returns>
        public List<TRN_Exam> GetExamByCorp(string corpCode)
        {
            var pages = FindAll<TRN_Exam>("WHERE CorpCode=@0", corpCode).ToList();
            pages.ForEach((item) =>
            {
                item.Judge = FindAll<TRN_Judge>("WHERE ExamID=@0", item.ID).ToList();
                item.Choice = FindAll<TRN_Choice>("WHERE ExamID=@0", item.ID).ToList();
            });


            return pages.ToList();
        }

        /// <summary>
        /// 获取判断题
        /// </summary>
        /// <param name="ExamID"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public TRN_Judge GetExamJudge(int ExamID, int seq)
        {
            List<TRN_Judge> trn_JudgeList = FindAll<TRN_Judge>("WHERE ExamID=@0 ORDER BY Seq",ExamID).ToList();
            List<TRN_Choice> trn_ChoiceList = FindAll<TRN_Choice>("WHERE ExamID=@0 ORDER BY Seq", ExamID).ToList();
            if (seq <= trn_JudgeList.Count)
            {
                TRN_Judge trn_Judge = trn_JudgeList.Skip(seq - 1).FirstOrDefault();
                trn_Judge.Count = trn_JudgeList.Count;
                trn_Judge.TotalCount = trn_JudgeList.Count + trn_ChoiceList.Count;

                return trn_Judge;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取选择题
        /// </summary>
        /// <param name="ExamID"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public TRN_Choice GetExamChoice(int ExamID, int seq)
        {
            List<TRN_Judge> trn_JudgeList = FindAll<TRN_Judge>("WHERE ExamID=@0 ORDER BY Seq", ExamID).ToList();
            List<TRN_Choice> trn_ChoiceList = FindAll<TRN_Choice>("WHERE ExamID=@0 ORDER BY Seq", ExamID).ToList();
            if (seq <= trn_ChoiceList.Count)
            {
                TRN_Choice trn_Choice = trn_ChoiceList.Skip(seq - 1).FirstOrDefault();
                trn_Choice.Count = trn_ChoiceList.Count;
                trn_Choice.TotalCount = trn_ChoiceList.Count + trn_JudgeList.Count;

                return trn_Choice;
            }
            else
            {
                return null;
            }
        }
    }
}
