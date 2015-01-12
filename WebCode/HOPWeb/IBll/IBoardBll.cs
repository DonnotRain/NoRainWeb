using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IBoardBll : ICommonBLL
    {
        Page<BRD_Board> GetBoardPager(int page, int pageSize, DateTime? beginDate, DateTime? endDate, string title);
        BRD_Board Add(BRD_Board border);
        BRD_Board Edit(BRD_Board border);

        List<HOP.Model.BoardInfo> GetBoardData(int top, string title, string corpCode, string userName);

        HOP.Model.BoardInfo GetBoardDetail(int id);

        void PostBoardMessage(string corpCode, string userName, string Ids);
    }
}
