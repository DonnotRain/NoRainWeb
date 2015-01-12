using DefaultConnection;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.HOP.Model.Request;
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

namespace WWQTWeb.Controllers.MobileAPI
{
    [MobileValidate()]
    [MobileServiceMark]
    public class MobileBoardController : ApiController
    {
        private IBoardBll m_BoardBll;
        public MobileBoardController()
        {
            m_BoardBll = DPResolver.Resolver<IBoardBll>();
        }

        /// <summary>
        /// 查询前10条公告板数据
        /// </summary>
        /// <returns>前10条公告板数据</returns>
        public List<BoardInfo> GetBoardData(string title,string corpCode,string userName)
        {
            List<BoardInfo> result = m_BoardBll.GetBoardData(10,title,corpCode,userName);

            return result;
        }

        /// <summary>
        /// 获取当前公告板详细信息
        /// </summary>
        /// <param name="id">公告板ID</param>
        /// <returns>当前公告板详细信息</returns>
        public BoardInfo GetBoardDetail(int id)
        {
            BoardInfo result = m_BoardBll.GetBoardDetail(id);

            return result;
        }

        /// <summary>
        /// 添加已读标记
        /// </summary>
        /// <param name="corpCode">公司名称</param>
        /// <param name="userName">用户名称</param>
        /// <param name="Ids">id集合</param>
        public void PostBoardMessage(string corpCode, string userName, string Ids)
        {
            m_BoardBll.PostBoardMessage(corpCode, userName, Ids);
        }
    }
}
