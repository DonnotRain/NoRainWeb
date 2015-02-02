using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.WebBase.Models
{
    public class AjaxResult
    {
        public bool IsSuccess { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public AjaxResult()
        {
            IsSuccess = true;
        }
        public AjaxResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public void OnError(string errorMessage = "")
        {
            this.IsSuccess = false;
            this.Message = errorMessage;
        }
    }
}
