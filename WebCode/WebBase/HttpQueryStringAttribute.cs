using System;

namespace WebBase
{

    /// <summary>
    /// Specifies a field for a query string. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    sealed class HttpQueryStringAttribute : Attribute
    {
        private readonly string _name;
        private readonly object _defaultValue;
        private readonly bool _isRequired;

        /// <summary>
        /// Constructor. The query string must be provided.
        /// </summary>
        /// <param name="name">Name of the query string</param>
        public HttpQueryStringAttribute(string name)
        {
            _name = name;
            _defaultValue = null;
            _isRequired = true;
        }

        /// <summary>
        /// Constructor. If the query string is not be provided, using the default value.
        /// </summary>
        /// <param name="name">Name of the query string</param>
        /// <param name="defaultValue">Default value of the query string which is not provided</param>
        public HttpQueryStringAttribute(string name, object defaultValue)
        {
            _name = name;
            _defaultValue = defaultValue;
            _isRequired = false;
        }

        /// <summary>
        /// Name of the query string.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Default value of the query string which is not provided.
        /// </summary>
        public object DefaultValue
        {
            get { return _defaultValue; }
        }

        /// <summary>
        /// Indicates if the query string must be provided.
        /// </summary>
        public bool IsRequired
        {
            get { return _isRequired; }
        }
    }
}