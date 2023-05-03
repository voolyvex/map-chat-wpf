using System;

namespace map_chat_wpf
{
    public class ChatBot
    {
        private readonly NaturalLanguageService _naturalLanguageService;
        private string _message;

        public event EventHandler<MessageEventArgs> OnMessageReceived;
        public event EventHandler<MessageEventArgs> OnResponseReceived;

        public ChatBot(NaturalLanguageService naturalLanguageService)
        {
            _naturalLanguageService = naturalLanguageService;
            _message = "";
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public void Start()
        {
            // Listen for messages from the user.
            OnMessageReceived += HandleMessageReceived;
        }

        public void SendMessage(string message)
        {
            // Process the message with the natural language service.
            string response = _naturalLanguageService.ProcessMessage(message);

            // Take any necessary actions based on the response.
            if (response.Contains("show points"))
            {
                // Update the map display to show relevant points.
                UpdateMapDisplay();
            }

            // Raise an event to notify any subscribers of the response.
            OnResponseReceived(this, new MessageEventArgs(response));
        }

        private void HandleMessageReceived(object sender, MessageEventArgs eventArgs)
        {
            // Get the message from the user.
            string message = eventArgs.Message;

            // Send the message to the natural language service.
            SendMessage(message);
        }

        private void UpdateMapDisplay()
        {
            // Code to update the map display goes here.
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
