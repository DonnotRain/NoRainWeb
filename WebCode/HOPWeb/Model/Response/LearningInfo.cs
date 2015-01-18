using System;

namespace NoRain.Business.Model
{
    public class LearningInfo
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public string Content { get; set; }

        public int Type { get; set; }

        public DateTime CreateTime { get; set; }

        public string Creator { get; set; }
    }
}
