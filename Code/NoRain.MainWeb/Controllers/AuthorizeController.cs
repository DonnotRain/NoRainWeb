using BusinessWeb.Filters;
using DefaultConnection;
using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoRainRights;
using NoRain.Business.Model.Request;
using NoRain.Business.Models;

namespace MainWeb.Controllers
{
    public class AuthorizeController : Controller
    {
        ICommonSecurityBLL m_bll;
        public AuthorizeController()
        {
            m_bll = DPResolver.Resolver<ICommonSecurityBLL>();
        }

        public ActionResult Login(bool? logout, string url)
        {
            ViewBag.Title = "登录";
            if (logout.HasValue && logout.Value)
            {
                Response.Cookies.Clear();
            }
            ViewBag.Url = url;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            AjaxResult result = new AjaxResult();
            result.Data = login;
            //先判断有没有相关用户名

            var isNameExist = m_bll.Find<SysUser>("WHERE Name=@0 ", login.username) != null;
            if (!isNameExist)
            {
                result.OnError("用户名不存在!");
            }

            login.password = NoRain.Business.CommonToolkit.CommonToolkit.GetMD5Password(login.password);

            var user = m_bll.Find<SysUser>("WHERE Name=@0 AND Password=@1", login.username, login.password);

            if (user == null)
            {
                result.OnError("用户名或密码错误，重新输入");
            }
            else
            {
                Response.AppendCookie(new HttpCookie("UserName", user.FullName.ToString()));
                Response.AppendCookie(new HttpCookie("UserId", user.ID.ToString()));
                if (string.IsNullOrEmpty(login.url)) return RedirectToAction("Index", "Home");
                else return Redirect(login.url);
            }

            return View(result);
        }


        [AdminAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [AdminAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(string password, string newPassword, string confirmPassword, string validateCode)
        {
            var bll = DPResolver.Resolver<ISysUserBll>();

            ViewBag.Success = false;
            if (validateCode != Session["__VCode"].ToString())
            {
                ModelState.AddModelError("", "验证码输入错误");
                return View();
            }
            try
            {
                bll.ChangePwd(password, newPassword, confirmPassword);

                ViewBag.Success = true;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "修改密码出错。");
            }

            return View();
        }


        public ActionResult Logout()
        {
            return RedirectToAction("Index", new { logout = true });
        }


        public void ValidateCode()
        {
            // 在此处放置用户代码以初始化页面
            string vnum;
            vnum = GetByRndNum(4);
            Response.ClearContent(); //需要输出图象信息 要修改HTTP头 
            Response.ContentType = "image/jpeg";

            CreateValidateCode(vnum);

        }

        private void CreateValidateCode(string vnum)
        {
            Bitmap Img = null;
            Graphics g = null;
            Random random = new Random();
            int gheight = vnum.Length * 15;
            Img = new Bitmap(gheight, 26);
            g = Graphics.FromImage(Img);
            Font f = new Font("微软雅黑", 16, FontStyle.Bold);
            //Font f = new Font("宋体", 9, FontStyle.Bold);

            g.Clear(Color.White);//设定背景色
            Pen blackPen = new Pen(ColorTranslator.FromHtml("#e1e8f3"), 18);

            for (int i = 0; i < 128; i++)// 随机产生干扰线，使图象中的认证码不易被其它程序探测到
            {
                int x = random.Next(gheight);
                int y = random.Next(20);
                int xl = random.Next(6);
                int yl = random.Next(2);
                g.DrawLine(blackPen, x, y, x + xl, y + yl);
            }

            SolidBrush s = new SolidBrush(ColorTranslator.FromHtml("#411464"));
            g.DrawString(vnum, f, s, 1, 1);

            //画边框
            blackPen.Width = 1;
            g.DrawRectangle(blackPen, 0, 0, Img.Width - 1, Img.Height - 1);
            Img.Save(Response.OutputStream, ImageFormat.Jpeg);
            s.Dispose();
            f.Dispose();
            blackPen.Dispose();
            g.Dispose();
            Img.Dispose();

            //Response.End();
        }

        //-----------------给定范围获得随机颜色
        Color GetByRandColor(int fc, int bc)
        {
            Random random = new Random();

            if (fc > 255)
                fc = 255;
            if (bc > 255)
                bc = 255;
            int r = fc + random.Next(bc - fc);
            int g = fc + random.Next(bc - fc);
            int b = fc + random.Next(bc - bc);
            Color rs = Color.FromArgb(r, g, b);
            return rs;
        }

        //取随机产生的认证码(数字)
        public string GetByRndNum(int VcodeNum)
        {

            string VNum = "";

            Random rand = new Random();

            for (int i = 0; i < VcodeNum; i++)
            {
                VNum += VcArray[rand.Next(0, 9)];
            }
            Session["__VCode"] = VNum;
            return VNum;
        }

        private static readonly string[] VcArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    }
}
