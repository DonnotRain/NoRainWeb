
using System;
using System.Net;

namespace WebBase
{
    public enum ResponseCode
    {

        #region 参数
        [ResponseMessage("必须参数[{0}]缺少", HttpStatusCode.NotFound)]
        SYSTEM_PARAMETER_REQUIRED = 10001001,

        [ResponseMessage("必须参数[{0}]缺少", HttpStatusCode.NotFound)]
        必须参数缺少 = 10001001,

        [ResponseMessage("参数[{0}]值错误", HttpStatusCode.NotAcceptable)]
        参数值错误 = 10001002,

        [ResponseMessage("参数[{0}]值格式错误", HttpStatusCode.NotAcceptable)]
        参数值格式错误 = 10001003,

        [ResponseMessage("验证失败,请安装最新版本", HttpStatusCode.NotAcceptable)]
        SYSTEM_VERIFICATION_ERROR = 10001004,

        [ResponseMessage("方法错误")]
        SYSTEM_PARAMETER_METHOD_ERROR = 10001005,

        [ResponseMessage("账号已在其他设备上登陆，请重新登陆")]
        SYSTEM_TOKEN_ERROR = 10001006,

        [ResponseMessage("您的操作过于频繁，请稍后再试")]
        SYSTEM_Request_Interval = 10001007,

        [ResponseMessage("基础数据缺少[{0}]")]
        SYSTEM_Static_Data_Null = 10001008,

        [ResponseMessage("当前无法操作，请重新登陆")]
        SYSTEM_Request_Timeout = 10001009,

        #endregion

        #region 服务器
        //系统错误
        [ResponseMessage("服务器错误$[{0}]")]
        SYSTEM_SERVER_ERROR = 10002001,
        [ResponseMessage("服务器关闭中，暂时不能使用")]
        SYSTEM_SERVER_CLOSED = 10002004,
        [ResponseMessage("登录失败，请重试")]
        SYSTEM_SERVER_LOGIN_ERROR = 10002005,
        #endregion
    }

    public class ResponseMessageAttribute : Attribute
    {
        private readonly string message;
        public readonly HttpStatusCode StatusCode;
        public ResponseMessageAttribute(string message)
        {
            this.message = message;
            this.StatusCode = HttpStatusCode.InternalServerError; ;
        }
        public ResponseMessageAttribute(string message, HttpStatusCode statusCode)
        {
            this.message = message;
            this.StatusCode = statusCode;
        }
        public override string ToString()
        {
            return message;
        }
    }
}
