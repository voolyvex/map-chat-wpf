using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace map_chat_wpf.Services
{
    internal class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            SetupMap();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Map? _map;
        public Map? Map
        {
            get { return _map; }
            set
            {
                _map = value;
                OnPropertyChanged();
            }
        }

        private void SetupMap()
        {

            // Create a new map with a 'topographic vector' basemap.
            Map = new Map(BasemapStyle.ArcGISTopographic);

        }

        public void UpdateMapDisplay(string userInput)
        {
            // Create a new graphics overlay to display the points.
            GraphicsOverlay overlay = new GraphicsOverlay();

            // Build the list of points from the user input using the MapPointsBuilder class.
            MapPointsBuilder builder = new MapPointsBuilder();
            List<MapPoint> points = builder.BuildMapPoints(userInput);

            // Add a graphic for each point.
            foreach (MapPoint point in points)
            {
                // Create a symbol for the point.
                SimpleMarkerSymbol symbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 10);

                // Create a graphic for the point with the symbol and geometry.
                Graphic graphic = new Graphic(point, symbol);

                // Add the graphic to the overlay.
                overlay.Graphics.Add(graphic);
            }

            // Wrap the graphics overlay in a layer and add it to the map's operational layers.
            Map?.OperationalLayers.Add(new GraphicsOverlayLayer(overlay));
        }

    }
}
