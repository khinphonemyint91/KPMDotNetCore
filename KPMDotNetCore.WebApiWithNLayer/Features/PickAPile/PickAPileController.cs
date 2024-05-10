using KPMDotNetCore.WebApiWithNLayer.Features.LatHtaukBayDin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KPMDotNetCore.WebApiWithNLayer.Features.PickAPile
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickAPileController : ControllerBase
    {
        private async Task<PickAPile> GetPickAPileDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("PickAPile.json");
            var model = JsonConvert.DeserializeObject<PickAPile>(jsonStr);
            return model;

        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var model = await GetPickAPileDataAsync();
            return Ok(model.Questions);
        }
        
        [HttpGet("{questionId}")]
        public async Task<IActionResult> QuestionById(int questionId)
        {
            var model = await GetPickAPileDataAsync();
            var answers = model.Answers.Where(x => x.QuestionId == questionId).ToList();
            return Ok(answers);

        }

        [HttpGet("{questionId}/{answerId}")]
        public async Task<IActionResult> AnswerByQuestionId(int questionId, int answerId)
        {
            var model = await GetPickAPileDataAsync();
            return Ok(model.Answers.FirstOrDefault(x => x.QuestionId == questionId && x.AnswerId == answerId));
        }

    }


    public class PickAPile
    {
        public Question[] Questions { get; set; }
        public Answer[] Answers { get; set; }
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionDesp { get; set; }
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerImageUrl { get; set; }
        public string AnswerName { get; set; }
        public string AnswerDesp { get; set; }
        public int QuestionId { get; set; }
    }

}
