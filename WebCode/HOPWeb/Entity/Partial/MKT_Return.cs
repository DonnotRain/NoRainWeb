using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultConnection
{
    public partial class MKT_Return
    {
        public List<FileItem> Files { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        public string StoreId { get; set; }

        public string Creator { get; set; }

        public string TypeName { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Barcode { get; set; }

    }
}
