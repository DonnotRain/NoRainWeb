using DefaultConnection;
using System;

namespace HuaweiSoftware.HOP.Model
{
    public class BoardInfo
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public DateTime CreateTime { get; set; }

        public string Creator { get; set; }

        public int TargetType { get; set; }

        public string TargetZone { get; set; }

        public FileItem File { get; set; }

        public bool IsRead { get; set; }
    }
}
