using Esri.ArcGISRuntime.Geometry;
using System.Collections.Generic;

namespace map_chat_wpf.Services
{
    public class MapPointsBuilder
    {
        public List<Point> BuildMapPoints(string userInput)
        {
            List<Point> points = new List<Point>();

            // Parse the user input to extract point coordinates and create Point objects
            // You can add your own implementation here to parse the user input in the way you need
            // Here's an example of how you might parse a comma-separated string of coordinates
            string[] coordinates = userInput.Split(',');
            foreach (string coordinate in coordinates)
            {
                string[] values = coordinate.Trim().Split(' ');
                double x, y;
                if (values.Length == 2 && double.TryParse(values[0], out x) && double.TryParse(values[1], out y))
                {
                    points.Add(new Point(x, y, SpatialReferences.Wgs84));
                }
            }

            return points;
        }
    }
}