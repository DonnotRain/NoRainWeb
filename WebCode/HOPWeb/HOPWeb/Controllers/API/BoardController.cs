using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class BoardController : ApiController
    {
        private IBoardBll m_BoardBll;
        public BoardController()
        {
            m_BoardBll = DPResolver.Resolver<IBoardBll>();
        }

        public object GetPager(int page, int rows, string beginDate, string endDate, string userName)
        {
            var pageResult = m_BoardBll.GetBoardPager(page, rows, string.IsNullOrEmpty(beginDate) ? null : (DateTime?)DateTime.Parse(beginDate),
                string.IsNullOrEmpty(endDate) ? null : (DateTime?)DateTime.Parse(endDate), userName);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        public object PostBoard(BRD_Board board)
        {
            board = m_BoardBll.Add(board);
            return board;
        }


        public object Put(BRD_Board board)
        {
            board = m_BoardBll.Edit(board);
            return board;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<BRD_Board>();
                itemsStr.ForEach(m =>
                {
                    var item = m_BoardBll.Find<BRD_Board>("WHERE ID=@0",int.Parse(m));
                    if (item != null) items.Add(item);
                });
                m_BoardBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
