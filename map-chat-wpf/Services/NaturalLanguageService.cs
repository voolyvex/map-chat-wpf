using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using map_chat_wpf.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class NaturalLanguageService
{
    private const string LUIS_APP_ID = "<your LUIS app ID>";
    private const string LUIS_SUBSCRIPTION_KEY = "<your LUIS subscription key>";
    private const string LUIS_ENDPOINT = "https://<your LUIS endpoint>.api.cognitive.microsoft.com/";

    private readonly Map _map;

    public NaturalLanguageService(Map map)
    {
        _map = map;
    }

    public async Task<string> ProcessMessageAsync(string message)
    {
        // Send a request to LUIS to get the predicted intent and entities from the user's message
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", LUIS_SUBSCRIPTION_KEY);
            string uri = $"{LUIS_ENDPOINT}luis/prediction/v3.0/apps/{LUIS_APP_ID}/slots/production/predict?query={message}";
            HttpResponseMessage response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return "Error: Failed to get response from LUIS";
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            LUISResponse luisResponse = JsonSerializer.Deserialize<LUISResponse>(responseContent);

            // Get the top-scoring intent and extract any relevant entities
            LUISIntent topIntent = luisResponse.Prediction.TopIntent;
            string intent = topIntent.Intent;
            switch (intent)
            {
                case "ShowMapPoints":
                    List<string> locations = new List<string>();
                    foreach (var entity in luisResponse.Prediction.Entities)
                    {
                        if (entity.Type == "Location")
                        {
                            locations.Add(entity.Text);
                        }
                    }
                    if (locations.Count == 0)
                    {
                        return "Please specify a location.";
                    }
                    else
                    {
                        string userInput = string.Join(",", locations);
                        MapPointsBuilder builder = new MapPointsBuilder();
                        List<Point> points = builder.BuildMapPoints(userInput);
                        if (points.Count == 0)
                        {
                            return "No points found for the specified location.";
                        }
                        else
                        {
                            UpdateMapDisplay(points);
                            return "Showing points for the specified location.";
                        }
                    }
                // Add more cases for other intents here
                default:
                    return "I'm sorry, I don't understand.";
            }
        }
    }

    private void UpdateMapDisplay(List<Point> points)
    {
        // Create a graphics overlay to hold the graphics
        GraphicsOverlay overlay = new GraphicsOverlay();

        // Add the graphics to the overlay
        foreach (Point point in points)
        {
            SimpleMarkerSymbol symbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 10);
            Graphic graphic = new Graphic(point, symbol);
            overlay.Graphics.Add(graphic);
        }

        // Add the overlay to the map
        _map.OperationalLayers.Add(overlay);
    }
}

public class LUISResponse
{
    public LUISPrediction Prediction { get; set; }
}

public class LUISPrediction
{
    public LUISIntent TopIntent { get; set; }
    public List<LUISEntity> Entities { get; set; }
}

public class LUISIntent
{
    public string Intent { get; set; }
}

public class LUISEntity
{
    public string Type { get; set; }
    public string Text { get; set; }
}


public class NaturalLanguageService
{
    private const string LUIS_APP_ID = "<your LUIS app ID>";
    private const string LUIS_SUBSCRIPTION_KEY = "<your LUIS subscription key>";
    private const string LUIS_ENDPOINT = "https://<your LUIS endpoint>.api.cognitive.microsoft.com/";
}