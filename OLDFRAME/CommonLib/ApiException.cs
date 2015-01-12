using System;
using System.Net.Http;
using System.Web.Http;

namespace MPAPI.CommonLib
{
    public class ApiException :Exception
    {
        private readonly ResponseCode code;
        private readonly string addition;
        public ApiException(ResponseCode code)
        {
            this.code = code;
        }

        public ApiException(ResponseCode code, string addition)
        {
            this.code = code;
            this.addition = addition;
        }

        public string ToDescription()
        {
            return EnumHelper.GetEnumDescription(code);
        }

        public ResponseCode ResponseCode { get { return code; } }

        public string Addition { get { return addition; } }
    }
}
