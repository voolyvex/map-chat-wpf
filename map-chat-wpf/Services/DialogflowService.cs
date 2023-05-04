using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System;

namespace map_chat_wpf
{
    public class DialogflowService
    {
        private readonly SessionsClient _sessionsClient;
        private readonly string _projectId;
        private readonly string _sessionId;

        public DialogflowService(string projectId, string sessionId)
        {
            _projectId = projectId;
            _sessionId = sessionId;
            _sessionsClient = SessionsClient.Create();
        }

        public string GetResponse(string message)
        {
            var response = _sessionsClient.DetectIntent(
                session: new SessionName(_projectId, _sessionId),
                queryInput: new QueryInput
                {
                    Text = new TextInput
                    {
                        Text = message,
                        LanguageCode = "en-US"
                    }
                }
            );

            return response.QueryResult.FulfillmentText;
        }
    }
}
