using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPAPI.WebBase
{
    public class AjaxResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public string ResponseCode { get; set; }

        public string Message { get; set; }

        public bool Successed { get; set; }

        public Object Data { get; set; }

        public AjaxResult()
        {
            Successed = true;
        }
        public AjaxResult(bool isSuccess, string message)
        {
            Successed = isSuccess;
            Message = message;
        }
        public AjaxResult(bool isSuccess, string message, string responseCode)
        {
            Successed = isSuccess;
            Message = message;
            ResponseCode = responseCode;
        }
    }
}
