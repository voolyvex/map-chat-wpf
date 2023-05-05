## Getting Started

### Prerequisites

* [.NET Framework](https://dotnet.microsoft.com/download) version 4.7.2 or later
* [Visual Studio](https://visualstudio.microsoft.com/) version 2017 or later

### Installation

1. Clone the repository or download the ZIP file.
2. Open the `MapChat.sln` solution file in Visual Studio.
3. In the `App.xaml.cs` file, replace `ArcGISAPIKEY` with your own ArcGIS API key. **Note:** It is not best practice to store API keys in source code.
4. Build and run the application.

### Usage

1. Enter a message in the chat box at the bottom of the screen and press enter.
2. The chat bot will process the message using natural language processing and respond with an appropriate message.
3. If the chat bot recognizes a command to "show points", it will update the map display to show relevant points.

### Dialogflow Integration

This application integrates with Dialogflow, a natural language understanding platform, to process user messages. To enable Dialogflow integration, you will need to create a Dialogflow agent and obtain a Google Cloud Platform API key. For more information, see the [Dialogflow documentation](https://cloud.google.com/dialogflow/docs). Once you have an API key, create a new file in the `MapChat` project called `appsettings.json`. In this file, add the following configuration:

```json
{
  "Dialogflow": {
    "ProjectId": "<your project id>",
    "SessionId": "<your session id>",
    "ApiKey": "<your api key>"
  }
}

