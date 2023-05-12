using Google.Cloud.Dialogflow.V2;

namespace map_chat_wpf
{
    public class NaturalLanguageService
    {
        private readonly SessionsClient _sessionsClient;
        private readonly string _projectId;
        private readonly string _sessionId;

        public NaturalLanguageService(string projectId, string sessionId)
        {
            _projectId = projectId;
            _sessionId = sessionId;

            // Create a new sessions client using Google Cloud credentials.
            _sessionsClient = SessionsClient.Create();
        }

        public string ProcessMessage(string message)
        {
            // Set the session ID and language code.
            var session = new SessionName(_projectId, _sessionId);
            var queryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = message,
                    LanguageCode = "en-US"
                }
            };

            // Call the Dialogflow API to detect the intent.
            var response = _sessionsClient.DetectIntent(session, queryInput);

            // Get the response from the Dialogflow API.
            var queryResult = response.QueryResult;

            // Return the fulfillment text.
            return queryResult.FulfillmentText;
        }
    }
}
