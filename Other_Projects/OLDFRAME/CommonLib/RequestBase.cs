using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
namespace MPAPI.CommonLib
{
    public class RequestBase
    {
        [HttpQueryString("timestamp")]
        public long Timestamp;

        [HttpQueryString("sig")]
        public string Sig;

        [HttpQueryString("token")]
        public string Token;

        private readonly NameValueCollection QueryString;
        public RequestBase(NameValueCollection queryString)
        {
            QueryString = queryString;
            Type type = GetType();
            ParameterInitialize(type.GetFields());
        }

        /// <summary>
        /// Initialize parameters according to query strings.
        /// </summary>
        protected void ParameterInitialize(FieldInfo[] fields)
        {
            foreach (FieldInfo field in fields)
            {
                var attribute =
                    (HttpQueryStringAttribute)Attribute.GetCustomAttribute(field, typeof(HttpQueryStringAttribute));

                // If has HttpQueryStringAttribute, this field is for a query string.
                if (attribute != null)
                {
                    SetField(field, attribute);
                }
            }
        }

        /// <summary>
        /// Set field according to the HttpQueryStringAttribute.
        /// </summary>
        /// <param name="field">The field will be set</param>
        /// <param name="attribute">The attribute of current field</param>
        private void SetField(FieldInfo field, HttpQueryStringAttribute attribute)
        {
            // The query string must be provided.
            if (attribute.IsRequired)
            {
                if (QueryString[attribute.Name] != null)
                {
                    SetFieldValue(field, this, attribute.Name, field.FieldType);
                }
                else
                {
                    throw new ApiException(ResponseCode.SYSTEM_PARAMETER_REQUIRED, attribute.Name);
                }
            }
            // If the query string is not be provided, using the default value.
            else
            {
                if (attribute.DefaultValue == null || field.FieldType == attribute.DefaultValue.GetType())
                {
                    if (QueryString[attribute.Name] == null || QueryString[attribute.Name] == string.Empty)
                    {
                        field.SetValue(this, attribute.DefaultValue);
                    }
                    else
                    {
                        SetFieldValue(field, this, attribute.Name, field.FieldType);
                    }
                }
                else
                {
                    throw new ApiException(ResponseCode.SYSTEM_PARAMETER_VALUE_INVALID, attribute.Name);
                }
            }
        }

        /// <summary>
        /// Set the value of current field according to the query string.
        /// </summary>
        /// <param name="field">The field will be set</param>
        /// <param name="obj">The object whose field value will be set</param>
        /// <param name="name">The name of query string</param>
        /// <param name="conversionType">The type to be converted</param>
        private void SetFieldValue(FieldInfo field, object obj, string name, Type conversionType)
        {
            try
            {
                // Set field value.
                field.SetValue(obj, Convert.ChangeType(HttpUtility.UrlDecode(QueryString[name]), conversionType));
            }
            catch (Exception ex)
            {
                throw new ApiException(ResponseCode.SYSTEM_PARAMETER_VALUE_CAN_NOT_CONVERT, name);
            }
        }
    }
}
