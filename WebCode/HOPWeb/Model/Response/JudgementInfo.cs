namespace NoRain.Business.Model
{
    public class JudgementInfo
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public int Seq { get; set; }

        public long ExamID { get; set; }

        public bool Answer { get; set; }

        public int AnswerTime { get; set; }
    }
}
