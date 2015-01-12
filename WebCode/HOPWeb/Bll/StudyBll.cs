using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuaweiSoftware.WQT.IBll;
using DefaultConnection;
using HuaweiSoftware.WQT.WebBase;

namespace HuaweiSoftware.WQT.Bll
{
    public class StudyBll : CommonBLL,IStudyBll
    {
        /// <summary>
        /// 获取学习资料
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="type">类型</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="pageSize">页面序号</param>
        /// <param name="pageIndex">页面大小</param>
        /// <returns>返回值</returns>
        public PetaPoco.Page<TRN_Study> GetPager(string title, int? type, DateTime? beginTime,DateTime? endTime,int pageSize,int pageIndex)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM TRN_Study").Append("WHERE CorpCode=@0", SysContext.CorpCode);
            if (!string.IsNullOrEmpty(title))
            {
                title = "%" + title + "%";
                sql.Append("AND Title like @0", title);
            }
            if (type.HasValue && type.Value != 0)
            {
                sql.Append("AND Type=@0", type.Value);
            }
            if (beginTime.HasValue && endTime.HasValue)
            {
                sql.Append("AND CreateTime>=@0 AND CreateTime<=@1", beginTime.Value,endTime.Value);
            }

            var page = FindAllByPage<TRN_Study>(sql.SQL, pageSize, pageIndex,sql.Arguments);
            //page.Items.ForEach(m =>
            //{
            //   var fileItem = Find<FileItem>("WHERE Id=@0", m.FileId);
            //   m.FileName = fileItem.FileName;
            //});

            return page;
        }

        /// <summary>
        /// 增加学习
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        public TRN_Study Add(TRN_Study trn_Study)
        {
            trn_Study.CorpCode = SysContext.CorpCode;
            trn_Study.CreateTime = DateTime.Now;
            trn_Study.Creator = SysContext.UserId.ToString();
            trn_Study.Insert();

            return trn_Study;
        }

        /// <summary>
        /// 修改学习
        /// </summary>
        /// <param name="trn_Study">学习实体</param>
        /// <returns></returns>
        public TRN_Study Edit(TRN_Study trn_Study)
        {
            trn_Study.Update();

            return trn_Study;
        }

        public void Delete(string ids)
        {
            string[] idList = ids.Split(',');
            foreach (string id in idList)
            {
                var trn_Study = Find<TRN_Study>("WHERE Id=@0", Convert.ToInt32(id));
                trn_Study.Delete();
            }
        }

        public object GetAll()
        {
            var pages = FindAll<TRN_Study>("WHERE CorpCode=@0", SysContext.CorpCode);

            return pages;
        }

        /// <summary>
        /// 获取学习资料
        /// </summary>
        /// <returns></returns>
        public List<TRN_Study> GetStudyByCorp(string corpCode,string title)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM TRN_Study").Append("WHERE CorpCode=@0",corpCode);
            if (title != null)
            {       
                title = "%" + title + "%";
                sql.Append("AND (Title LIKE @0 OR Detail LIKE @0)",title);
            }
            sql.Append("ORDER BY CreateTime DESC");

            return FindAll<TRN_Study>(sql.SQL, sql.Arguments).ToList();
        }

        /// <summary>
        /// 获取学习资料详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TRN_Study GetStudyDetail(int ID)
        {
            TRN_Study trn_Study =  Find<TRN_Study>("WHERE ID=@0", ID);
            if (trn_Study.FileId != 0)
            {
                trn_Study.FileName = Find<FileItem>("WHERE ID=@0", trn_Study.FileId).FileName;
            }

            return trn_Study;
        }
    }
}
