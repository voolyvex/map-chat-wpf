using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace map_chat_wpf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly NaturalLanguageService naturalLanguageService;

        public MessagesController(NaturalLanguageService naturalLanguageService)
        {
            this.naturalLanguageService = naturalLanguageService;
        }

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
    }

    public class JsonResult : ActionResult
    {
        private readonly object value;

        public JsonResult(object value)
        {
            this.value = value;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            // Serialize the value to JSON
            string json = JsonConvert.SerializeObject(value);

            // Set the content type of the response
            context.HttpContext.Response.ContentType = "application/json";

            // Write the JSON to the response stream
            await context.HttpContext.Response.WriteAsync(json);
        }
    }
}
