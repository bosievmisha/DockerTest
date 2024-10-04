using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    public class SurveyController : ControllerBase
    {
        private readonly SurveyContext _context; 

        public SurveyController(SurveyContext context)
        {
            _context = context;
        }

        [HttpGet("surveys/{surveyId}/questions/{questionId}")]
        public async Task<IActionResult> GetQuestion(int surveyId, int questionId)
        {
            var question = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.SurveyId == surveyId && q.Id == questionId);

            if (question == null)
            {
                return NotFound();
            }

            var response = new
            {
                Text = question.Text,
                Answers = question.Answers.Select(a => new { a.Id, a.Text })
            };

            return Ok(response);
        }

        [HttpPost("interviews/{interviewId}/results")]
        public async Task<IActionResult> SaveResult(int interviewId, int questionId, int answerId)
        {
            // Валидация данных (можно добавить дополнительные проверки)
            if (questionId <= 0 || answerId <= 0)
            {
                return BadRequest("Неверные значения questionId или answerId.");
            }

            var result = new Result
            {
                InterviewId = interviewId,
                QuestionId = questionId,
                AnswerId = answerId
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            var nextQuestion = await _context.Questions
                .Where(q => q.SurveyId == _context.Interviews.FirstOrDefault(i => i.Id == interviewId).SurveyId)
                .OrderBy(q => q.Order)
                .FirstOrDefaultAsync(q => q.Order > _context.Questions.FirstOrDefault(qu => qu.Id == questionId).Order);

            return Ok(new { nextQuestionId = nextQuestion?.Id });
        }
    }
}
