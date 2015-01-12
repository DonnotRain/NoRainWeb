using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WQTRights;

namespace HuaweiSoftware.WQT.Bll
{
    public class BRD_BoardBll : CommonBLL, IBoardBll
    {
        ICommonSecurityBLL m_securityBll;
        public BRD_BoardBll(ICommonSecurityBLL securityBll)
        {
            m_securityBll = securityBll;
        }

        public PetaPoco.Page<DefaultConnection.BRD_Board> GetBoardPager(int pageIndex, int pageSize, DateTime? beginDate, DateTime? endDate, string title)
        {

            var sql = Sql.Builder.Append("Where CorpCode=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And  ValidDate >=@0", beginDate.Value);
            }
            //开始时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And ValidDate <=@0", endDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(title))
            {
                title = "%" + title + "%";
                sql.Append(" And Title like @0", title);
            }

            var page = FindAllByPage<BRD_Board>(sql.SQL, pageSize, pageIndex, sql.Arguments);

            page.Items.ForEach(m =>
            {

                if (m.FileID > 0)
                {
                    if (m.Files == null) m.Files = new List<FileItem>();
                    var file = Find<FileItem>("Where ID=@0", m.FileID);
                    m.Files.Add(file);
                }
            });

            return page;
        }


        public BRD_Board Add(BRD_Board border)
        {
            var user = m_securityBll.Find<User>("WHERE ID=@0", SysContext.UserId);
            border.CreateTime = DateTime.Now;

            border.Creator = user.Name;
            border.CorpCode = SysContext.CorpCode;
            border.Save();
            return border;
        }


        public BRD_Board Edit(BRD_Board border)
        {
            var oldBoard = Find<BRD_Board>("WHERE ID=@0", border.ID);
            oldBoard.Title = border.Title;
            oldBoard.TargetType = border.TargetType;
            oldBoard.TargetZone = border.TargetZone;
            oldBoard.ValidDate = border.ValidDate;
            oldBoard.FileID = border.FileID;
            Update(oldBoard);
            return oldBoard;
        }


        public List<HOP.Model.BoardInfo> GetBoardData(int top,string title,string corpCode,string userName)
        {
            var sql = Sql.Builder.Append("Where CorpCode=@0", corpCode);
            SEC_User user = Find<SEC_User>("WHERE UserName=@0 AND CorpCode=@1", userName, corpCode);

            if (title != null)
            {
                title = "%" + title + "%";
                sql.Append("AND (Title LIKE @0 OR Detail LIKE @0)",title);
            }

            sql.Append("ORDER BY CreateTime DESC");

            var page = FindAllByPage<BRD_Board>(sql.SQL, top, 1, sql.Arguments);

            page.Items.ForEach(m =>
            {
                if (m.FileID > 0)
                {
                    if (m.Files == null) m.Files = new List<FileItem>();
                    var file = Find<FileItem>("Where ID=@0", m.FileID);
                    m.Files.Add(file);
                }
            });

            return page.Items.Select(m => new HOP.Model.BoardInfo()
            {
                CreateTime = m.CreateTime,
                Creator = m.Creator,
                Detail = m.Detail,
                ID = m.ID,
                TargetType = m.TargetType,
                TargetZone = m.TargetZone,
                Title = m.Title,
                File = m.FileID > 0 ? m.Files[0] : null,
                IsRead = FindAll<BRD_Record>("WHERE UserId=@0 AND BoardId=@1",user.ID,m.ID).ToList().Count > 0

            }).ToList();
        }

        public HOP.Model.BoardInfo GetBoardDetail(int id)
        {
            var board = Find<BRD_Board>("WHERE ID=@0", id);
            return new HOP.Model.BoardInfo()
            {
                CreateTime = board.CreateTime,
                Creator = board.Creator,
                Detail = board.Detail,
                ID = board.ID,
                TargetType = board.TargetType,
                TargetZone = board.TargetZone,
                Title = board.Title
            };
        }

        public void PostBoardMessage(string corpCode, string userName, string Ids)
        {
            SEC_User user = Find<SEC_User>("WHERE UserName=@0 AND CorpCode=@1", userName, corpCode);
            string[] IdList = Ids.Split(',');
            foreach (string Id in IdList)
            {
                if (FindAll<BRD_Record>("WHERE BoardId=@0 AND UserId=@1", Id, user.ID).ToList().Count == 0)
                {
                    BRD_Record brd_Record = new BRD_Record();
                    brd_Record.BoardId = Convert.ToInt32(Id);
                    brd_Record.UserId = user.ID;

                    brd_Record.Insert();
                }
            }
        }
    }
}
