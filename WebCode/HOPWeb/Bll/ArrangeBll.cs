using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuaweiSoftware.WQT.IBll;
using DefaultConnection;
using HuaweiSoftware.WQT.WebBase;

namespace HuaweiSoftware.WQT.Bll
{
    public class ArrangeBll : CommonBLL, IArrangeBll
    {
        public PetaPoco.Page<TRN_StudyExamArrange> GetPager(string title, int? type, int pageSize, int pageIndex)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM TRN_StudyExamArrange").Append("WHERE CorpCode=@0",SysContext.CorpCode);

            if (!string.IsNullOrEmpty(title))
            {
                title = "%" + title + "%";
                sql.Append("AND Title like @0", title);
            }
            if (type.HasValue && type.Value != 0)
            {
                sql.Append("AND Type=@0", type.Value);
            }

            var pages = FindAllByPage<TRN_StudyExamArrange>(sql.SQL, pageSize, pageIndex, sql.Arguments);
            pages.Items.ForEach(m =>
            {
                var fileItem = Find<FileItem>("WHERE Id=@0", m.FileID);
                m.FileName = fileItem.FileName;
            });

            return pages;
        }

        /// <summary>
        /// 增加学习
        /// </summary>
        /// <param name="trn_StudyExamArrange">学习实体</param>
        /// <returns></returns>
        public TRN_StudyExamArrange Add(TRN_StudyExamArrange trn_StudyExamArrange)
        {
            trn_StudyExamArrange.CorpCode = SysContext.CorpCode;
            trn_StudyExamArrange.EntityID = string.Join(",", trn_StudyExamArrange.EntityIds);
            trn_StudyExamArrange.Insert();

            return trn_StudyExamArrange;
        }

        /// <summary>
        /// 修改学习
        /// </summary>
        /// <param name="trn_StudyExamArrange">学习实体</param>
        /// <returns></returns>
        public TRN_StudyExamArrange Edit(TRN_StudyExamArrange trn_StudyExamArrange)
        {
            trn_StudyExamArrange.EntityID = string.Join(",", trn_StudyExamArrange.EntityIds);
            trn_StudyExamArrange.Update();

            return trn_StudyExamArrange;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string ids)
        {
            string[] idList = ids.Split(',');
            foreach (string id in idList)
            {
                var trn_StudyExamArrange = Find<TRN_StudyExamArrange>("WHERE ID=@0", Convert.ToInt32(id));
                trn_StudyExamArrange.Delete();
            }
        }

        /// <summary>
        /// 获取考试安排
        /// </summary>
        /// <param name="corpCode"></param>
        /// <returns></returns>
        public List<TRN_StudyExamArrange> GetExamArrangeByCorpCode(string corpCode,string userName)
        {
            SEC_User sec_User = Find<SEC_User>("WHERE UserName=@0 AND CorpCode=@1", userName, corpCode);
            if (sec_User == null)
            {
                return null;
            }
            List<TRN_StudyExamArrange> trn_StudyExamArrangeList = FindAll<TRN_StudyExamArrange>("WHERE CorpCode=@0 And Type=@1 AND ID NOT IN(SELECT StudyExamArrangeID FROM TRN_ExamScore WHERE UserID=@2) ORDER BY ExamTime DESC", corpCode, (int)ArrangeType.Exam, sec_User.ID).ToList();

            trn_StudyExamArrangeList.ForEach((item) =>
            {
                item.EntityNames = new List<string>();
                string[] entityId = item.EntityID.Split(',');
                foreach (string Id in entityId)
                {
                    TRN_Exam trn_Exam = Find<TRN_Exam>("WHERE ID=@0", Id);
                    item.EntityNames.Add(trn_Exam.Title);
                }
                item.TypeName = "考试";
            });

            return trn_StudyExamArrangeList;
        }

        /// <summary>
        /// 获取单个考试安排详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TRN_StudyExamArrange GetArrangeDetail(int ID)
        {
            TRN_StudyExamArrange trn_StudyExamArrange = Find<TRN_StudyExamArrange>("WHERE ID=@0", ID);
            trn_StudyExamArrange.EntityIds = trn_StudyExamArrange.EntityID.Split(',');
            trn_StudyExamArrange.EntityNames = new List<string>();
            foreach (string id in trn_StudyExamArrange.EntityIds)
            {
                trn_StudyExamArrange.EntityNames.Add(Find<TRN_Exam>("WHERE ID=@0", id).Title);
            }

            return trn_StudyExamArrange;
        }

        private enum ArrangeType
        {
            Study = 1,
            Exam
        }
    }
}
