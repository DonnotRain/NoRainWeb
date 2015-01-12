using System;

namespace HuaweiSoftware.HOP.Model
{
    public class ExamInfo
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public long FileID { get; set; }

        public int Type { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime ExpiredTime { get; set; }
    }
}
