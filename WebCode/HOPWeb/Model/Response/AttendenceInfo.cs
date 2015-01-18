using DefaultConnection;
using System;

namespace NoRain.Business.Model
{
    public class AttendenceInfo
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string Position { get; set; }

        public bool Type { get; set; }

        public FileItem File { get; set; }
    }
}
