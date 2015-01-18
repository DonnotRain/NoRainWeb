using System;

namespace NoRain.Business.Model
{
    public class LeaveInfo
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Duration { get; set; }

        public string Reason { get; set; }

        public short State { get; set; }
    }
}
