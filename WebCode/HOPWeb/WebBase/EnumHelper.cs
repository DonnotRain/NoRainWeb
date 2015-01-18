using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

namespace NoRain.Business.WebBase
{
    public class EnumHelper
    {
        private EnumHelper()
        {
        }

        public static string GetEnumDescription<T>(T enumeratedType)
        {
            var description = enumeratedType.ToString();

            var fieldInfo = enumeratedType.GetType().GetField(enumeratedType.ToString());

            if (fieldInfo != null)
            {
                var attributes = fieldInfo.GetCustomAttributes(false);

                if (attributes.Length > 0)
                {
                    description = attributes[0].ToString();
                }
            }

            return description;
        }

        public static string GetEnumCollectionDescription<T>(Collection<T> enums)
        {
            var sb = new StringBuilder();

            var enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            foreach (var enumeratedType in enums)
            {
                sb.AppendLine(GetEnumDescription(enumeratedType));
            }

            return sb.ToString();
        }
        public static HttpStatusCode GetResponseCode(ResponseCode responseCode)
        {
           var fieldInfo=   responseCode.GetType().GetField(responseCode.ToString());
        var attributes=   fieldInfo.GetCustomAttributes(typeof(ResponseMessageAttribute), false);
        var responseMessage = (ResponseMessageAttribute)attributes[0];
        return responseMessage.StatusCode;
        }
    }
}
