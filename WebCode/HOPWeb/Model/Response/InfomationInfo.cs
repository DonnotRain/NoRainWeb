using System;

namespace NoRain.Business.Model
{
    public class InfomationInfo
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public long FileID { get; set; }

        public DateTime CreateTime { get; set; }

        public string Creator { get; set; }
    }
}
