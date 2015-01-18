namespace NoRain.Business.Model
{
    public class ChoiceInfo
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public int Seq { get; set; }

        public long ExamID { get; set; }

        public bool Answer { get; set; }

        public string ChoiceA { get; set; }

        public string ChoiceB { get; set; }

        public string ChoiceC { get; set; }

        public int AnswerTime { get; set; }
    }
}
