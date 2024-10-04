using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Question> Questions { get; set; }
        public List<Interview> Interviews { get; set; }
    }
}
