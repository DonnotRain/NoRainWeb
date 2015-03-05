using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NoRain.WebBase
{
    public class CookieTool
    {
        #region  删除cookies
        ///<summary>
        /// 删除cookies
        ///</summary>
        public static void DelCookeis()
        {
            foreach (string cookiename in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpCookie cookies = HttpContext.Current.Request.Cookies[cookiename];
                if (cookies != null)
                {
                    cookies.Expires = DateTime.Today.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookies);
                    HttpContext.Current.Request.Cookies.Remove(cookiename);
                }
            }
        }

        ///<summary>
        /// 删除cookies
        ///</summary>
        public static void DelCookie(string cookieName)
        {

            HttpCookie cookies = HttpContext.Current.Request.Cookies[cookieName];
            if (cookies != null)
            {
                cookies.Expires = DateTime.Today.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookies);
                HttpContext.Current.Request.Cookies.Remove(cookieName);
            }
        }
        #endregion
    }
}
