using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultConnection
{
    public partial class TRN_Exam
    {
        public string FileName { get; set; }

        public List<int> TRN_StudyID { get; set; }

        public List<TRN_Judge> Judge { get; set; }

        public List<TRN_Choice> Choice { get; set; }
    }
}
