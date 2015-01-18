using NoRain.Business.CommonToolkit;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WQTWeb.Filters
{
    /// <summary>
    /// 用于移动端的接口标记,被标记的接口显示在/Help页面中
    /// </summary>
    public class MobileServiceMarkAttribute : Attribute
    {
    }
}