using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatGPT.Net;

namespace map_chat_wpf
{
    public class ChatBot
    {
        private readonly ChatGPT _chatGPT;
        private string _message;
        public event EventHandler<MessageEventArgs> OnMessageReceived;

        public ChatBot()
        {
            // Create a new instance of the ChatGPT class.
            _chatGPT = new ChatGPT();

            // Initialize the Message property.
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
            _chatGPT.OnMessageReceived += OnMessageReceived;

            // Start listening for messages.
            _chatGPT.StartListening();
        }

        private void OnMessageReceived(object sender, MessageEventArgs eventArgs)
        {
            // Get the message from the user.
            string message = eventArgs.Message;

            // Respond to the message.
            _chatGPT.Respond(message);
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}