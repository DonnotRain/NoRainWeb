using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultConnection
{
    public partial class ATD_Attendence
    {
        public List<FileItem> Files { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        public string StoreId { get; set; }

        public string Name { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
