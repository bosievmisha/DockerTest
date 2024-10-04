using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public Survey Survey { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Result> Results { get; set; }
    }
}
