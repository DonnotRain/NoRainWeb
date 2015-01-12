using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultConnection
{
    public partial class MKT_Request
    {
        public List<FileItem> Files { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        public string StoreId { get; set; }

        public string Creator { get; set; }

    }
}
