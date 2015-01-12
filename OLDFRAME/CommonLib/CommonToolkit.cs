using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPAPI.CommonLib
{
    public class CommonToolkit
    {
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
    }
}
