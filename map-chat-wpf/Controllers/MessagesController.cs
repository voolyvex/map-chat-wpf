using Microsoft.AspNetCore.Mvc;


namespace map_chat_wpf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
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
            string? message = userMessage.message;

            // Pass the message to your natural language processing service and get a response
            string? response = naturalLanguageService.ProcessMessage(message);

            // Return the response as a JSON object
            return new JsonResult(new { message = response });
        }

        private readonly NaturalLanguageService naturalLanguageService;

        public MessagesController(NaturalLanguageService naturalLanguageService)
        {
            this.naturalLanguageService = naturalLanguageService;
        }

    }
}