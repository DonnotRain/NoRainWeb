using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonToolkit
{
    public class CommonToolkit
    {
        public static string GetContentType(string extension)
        {
            extension = extension.ToUpper();
            string contentType = string.Empty;

            if (extension == "323")
                contentType = "text/h323";
            else if (extension == "ACX")
                contentType = "application/internet-property-stream";
            else if (extension == "AI")
                contentType = "application/postscript";
            else if (extension == "AIF")
                contentType = "audio/x-aiff";
            else if (extension == "AIFC")
                contentType = "audio/x-aiff";
            else if (extension == "AIFF")
                contentType = "audio/x-aiff";
            else if (extension == "ASF")
                contentType = "video/x-ms-asf";
            else if (extension == "SR")
                contentType = "video/x-ms-asf";
            else if (extension == "SX")
                contentType = "video/x-ms-asf";
            else if (extension == "AU")
                contentType = "audio/basic";
            else if (extension == "AVI")
                contentType = "video/x-msvideo";
            else if (extension == "AXS")
                contentType = "application/olescript";
            else if (extension == "BAS")
                contentType = "text/plain";
            else if (extension == "BCPIO")
                contentType = "application/x-bcpio";
            else if (extension == "BIN")
                contentType = "application/octet-stream";
            else if (extension == "BMP")
                contentType = "image/bmp";
            else if (extension == "C")
                contentType = "text/plain";
            else if (extension == "CAT")
                contentType = "application/vnd.ms-pkiseccat";
            else if (extension == "CDF")
                contentType = "application/x-cdf";
            else if (extension == "CER")
                contentType = "application/x-x509-ca-cert";
            else if (extension == "CLASS")
                contentType = "application/octet-stream";
            else if (extension == "CLP")
                contentType = "application/x-msclip";
            else if (extension == "CMX")
                contentType = "image/x-cmx";
            else if (extension == "COD")
                contentType = "image/cis-cod";
            else if (extension == "CPIO")
                contentType = "application/x-cpio";
            else if (extension == "CRD")
                contentType = "application/x-mscardfile";
            else if (extension == "CRL")
                contentType = "application/pkix-crl";
            else if (extension == "CRT")
                contentType = "application/x-x509-ca-cert";
            else if (extension == "CSH")
                contentType = "application/x-csh";
            else if (extension == "CSS")
                contentType = "text/css";
            else if (extension == "DCR")
                contentType = "application/x-director";
            else if (extension == "DER")
                contentType = "application/x-x509-ca-cert";
            else if (extension == "DIR")
                contentType = "application/x-director";
            else if (extension == "DLL")
                contentType = "application/x-msdownload";
            else if (extension == "DMS")
                contentType = "application/octet-stream";
            else if (extension == "DOC")
                contentType = "application/msword";
            else if (extension == "DOT")
                contentType = "application/msword";
            else if (extension == "DVI")
                contentType = "application/x-dvi";
            else if (extension == "DXR")
                contentType = "application/x-director";
            else if (extension == "EPS")
                contentType = "application/postscript";
            else if (extension == "ETX")
                contentType = "text/x-setext";
            else if (extension == "EVY")
                contentType = "application/envoy";
            else if (extension == "EXE")
                contentType = "application/octet-stream";
            else if (extension == "FIF")
                contentType = "application/fractals";
            else if (extension == "FLR")
                contentType = "x-world/x-vrml";
            else if (extension == "GIF")
                contentType = "image/gif";
            else if (extension == "GTAR")
                contentType = "application/x-gtar";
            else if (extension == "GZ")
                contentType = "application/x-gzip";
            else if (extension == "H")
                contentType = "text/plain";
            else if (extension == "HDF")
                contentType = "application/x-hdf";
            else if (extension == "HLP")
                contentType = "application/winhlp";
            else if (extension == "HQX")
                contentType = "application/mac-binhex40";
            else if (extension == "HTA")
                contentType = "application/hta";
            else if (extension == "HTC")
                contentType = "text/x-component";
            else if (extension == "HTM")
                contentType = "text/html";
            else if (extension == "HTML")
                contentType = "text/html";
            else if (extension == "HTT")
                contentType = "text/webviewhtml";
            else if (extension == "ICO")
                contentType = "image/x-icon";
            else if (extension == "IEF")
                contentType = "image/ief";
            else if (extension == "III")
                contentType = "application/x-iphone";
            else if (extension == "INS")
                contentType = "application/x-internet-signup";
            else if (extension == "ISP")
                contentType = "application/x-internet-signup";
            else if (extension == "JFIF")
                contentType = "image/pipeg";
            else if (extension == "JPE")
                contentType = "image/jpeg";
            else if (extension == "JPEG" || extension == "JPG")
                contentType = "image/jpeg";
            else if (extension == "PNG")
                contentType = "image/x-png";
            //如果以上的都不匹配
            else contentType = "application/octet-stream";

            return contentType;
        }
        /// <summary>
        /// 将字符串进行MD5加密,使用普通方式
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            //获取加密服务  
            System.Security.Cryptography.MD5CryptoServiceProvider md5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //获取要加密的字段，并转化为Byte[]数组  
            byte[] testEncrypt = Encoding.UTF8.GetBytes(str);

            //加密Byte[]数组  
            byte[] resultEncrypt = md5CSP.ComputeHash(testEncrypt);

            //将加密后的数组转化为字段(普通加密)  
            string testResult = BitConverter.ToString(resultEncrypt).Replace("-", "").ToLower();

            return testResult;
        }

        /// <summary>
        /// 将字符串进行MD5加密,使用密码加密方式
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetMD5Password(string str)
        {
            string PWD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str.ToLower(), "MD5").ToLower();
            return PWD;
        }
        public static string GetBase64(string str)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 根据传入的日期阶段类型，生成查询条件的两个日期
        /// </summary>
        /// <param name="type">day 天，week 周，month月，year年,lastday昨天，lastweek上周,lastyear去年</param>
        /// <returns></returns>
        public static DateTime[] GetTypeTime(string type)
        {
            //当前开始时间
            DateTime beginDate = DateTime.Now;
            //当前结束时间
            DateTime endDate = DateTime.Now;

            //上日/周/月/年开始时间
            DateTime lastBeginTime = DateTime.Now;
            //上日/周/月/年结束时间
            DateTime lastendTime = DateTime.Now;

            //开始时间精确到天
            beginDate = beginDate.AddHours(-beginDate.Hour);
            beginDate = beginDate.AddMinutes(-beginDate.Minute);
            beginDate = beginDate.AddSeconds(-beginDate.Second);


            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException("type");

            switch (type.ToUpper())
            {
                case "DAY":
                    lastBeginTime = beginDate.AddDays(-1);
                    lastendTime = beginDate;
                    break;
                case "LASTDAY":
                    beginDate = beginDate.AddDays(-1);
                    //结束时间截至到今天凌晨
                    RemoveHour(endDate);

                    lastBeginTime = beginDate.AddDays(-1);
                    lastendTime = beginDate;
                    break;
                case "WEEK":
                    beginDate = beginDate.AddDays(-(int)DateTime.Now.DayOfWeek);

                    lastBeginTime = beginDate.AddDays(-7);
                    lastendTime = beginDate;
                    break;
                case "LASTWEEK":    //上周与上上周
                     beginDate = beginDate.AddDays(-(int)DateTime.Now.DayOfWeek - 7);
                    RemoveHour(endDate);
                    endDate = endDate.AddDays(-(int)DateTime.Now.DayOfWeek);

                    lastBeginTime = beginDate.AddDays(-7);
                    lastendTime = beginDate;
                    break;
                case "MONTH":
                    beginDate = beginDate.AddDays(1 - DateTime.Now.Day);  //把日期中天数设为1 

                    lastBeginTime = beginDate.AddMonths(-1);    //再往前推一个月
                    lastendTime = beginDate;
                    break;
                case "LASTMONTH":
                    beginDate = beginDate.AddMonths(-1).AddDays(1 - DateTime.Now.Day);
                    RemoveHour(endDate);
                    endDate = endDate.AddDays(1 - DateTime.Now.Day);  //把结束日期中天数设为1 

                    lastBeginTime = beginDate.AddMonths(-1);    //再往前推一个月
                    lastendTime = beginDate;
                    break;

                case "YEAR":
                    beginDate = beginDate.AddMonths(1 - DateTime.Now.Month).AddDays(1 - DateTime.Now.Day);  //把月份和天数设为1

                    lastBeginTime = beginDate.AddYears(-1);    //再往前推一年
                    lastendTime = beginDate;
                    break;
                case "LASTYEAR":
                    beginDate = beginDate.AddYears(-1).AddMonths(1 - DateTime.Now.Month).AddDays(1 - DateTime.Now.Day);  //把月份和天数设为1且年份设为去年
                    RemoveHour(endDate);
                    endDate = endDate.AddMonths(1 - DateTime.Now.Month).AddDays(1 - DateTime.Now.Day);  //把月份和天数设为1            

                    lastBeginTime = beginDate.AddYears(-1);    //再往前推一年
                    lastendTime = beginDate;
                    break;
            }

            return new DateTime[4] { beginDate, endDate, lastBeginTime, lastendTime };
        }

        /// <summary>
        /// 把小时分钟和秒设为0
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static DateTime RemoveHour(DateTime time)
        {
            time = time.AddHours(-time.Hour);
            time = time.AddMinutes(-time.Minute);
            time = time.AddSeconds(-time.Second);
            return time;
        }
    }
}
