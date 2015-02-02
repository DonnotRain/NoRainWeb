using System;
namespace NoRain.Business.WebBase
{
    public class ApiException : Exception
    {
        private readonly ResponseCode code;
        private readonly string addition;
        private readonly string[] parameters;
        public ApiException(ResponseCode code, params string[] parameters)
        {
            this.code = code;
            this.parameters = parameters;
        }

        public ApiException(ResponseCode code, string addition, params string[] parameters)
        {
            this.code = code;
            this.addition = addition;
            this.parameters = parameters;
        }

        public string ToDescription()
        {
            return EnumHelper.GetEnumDescription(code);
        }

        public ResponseCode ResponseCode { get { return code; } }

        public string[] Parameters { get { return parameters; } }
        public string Addition { get { return addition; } }
    }
}
