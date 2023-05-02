using Microsoft.AspNetCore.Mvc;

namespace map_gpt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello, world!";
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserMessage userMessage)
        {
            // Extract the user message from the request body
            string message = userMessage.message;

            // Pass the message to your natural language processing service and get a response
            string response = naturalLanguageService.ProcessMessage(message);

            // Return the response as a JSON object
            return new JsonResult(new { message = response });
        }

        private readonly NaturalLanguageService naturalLanguageService;

        public ChatController(NaturalLanguageService naturalLanguageService)
        {
            this.naturalLanguageService = naturalLanguageService;
        }

    }
}