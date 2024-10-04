using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public Question Question { get; set; }
        public List<Result> Results { get; set; }
    }
}
