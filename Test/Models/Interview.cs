using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Interview
    {
        [Key]
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Survey Survey { get; set; }
        public List<Result> Results { get; set; }
    }
}
