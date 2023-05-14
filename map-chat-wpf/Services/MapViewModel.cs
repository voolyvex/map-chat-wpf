using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace map_chat_wpf.Services
{
    internal class MapViewModel : INotifyPropertyChanged
    {
        private readonly Map _map;
        private readonly GraphicsOverlayCollection _graphicsOverlays;

        public MapViewModel(List<MapPoint> points)
        {
            _map = new Map(BasemapStyle.ArcGISStreets);
            var mapView = new MapView { Map = _map };

            // Use the first point in the list as the initial location.
            var initialLocation = points.FirstOrDefault() ?? new MapPoint(-107.8801, 37.2753, SpatialReferences.Wgs84);
            var initialViewpoint = new Viewpoint(initialLocation, scale: 50000);
            mapView.SetViewpointAsync(initialViewpoint);

            _graphicsOverlays = new GraphicsOverlayCollection();
            var graphicsOverlay = new GraphicsOverlay();
            _graphicsOverlays.Add(graphicsOverlay);

            GraphicsOverlays.Add(graphicsOverlay);

            // Raise the PropertyChanged event for the GraphicsOverlays property
            OnPropertyChanged(nameof(GraphicsOverlays));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Map Map => _map;

        public GraphicsOverlayCollection GraphicsOverlays
        {
            get { return _graphicsOverlays; }
            set
            {
                if (value?.FirstOrDefault() != null)
                {
                    _graphicsOverlays.Clear();
                    _graphicsOverlays.Add(value.FirstOrDefault());
                }

                // Raise the PropertyChanged event for the GraphicsOverlays property
                OnPropertyChanged(nameof(GraphicsOverlays));
            }
        }

        public void UpdateMapDisplay(string userInput)
        {
            if (Map != null)
            {
                // Build the list of points from the user input using the MapPointsBuilder class.
                MapPointsBuilder builder = new MapPointsBuilder();
                List<MapPoint> points = builder.BuildMapPoints(userInput);

                // Add a graphic for each point.
                foreach (MapPoint point in points)
                {
                    // Create a symbol for the point.
                    var symbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 10);

                    // Add an outline to the symbol.
                    symbol.Outline = new SimpleLineSymbol
                    {
                        Style = SimpleLineSymbolStyle.Solid,
                        Color = System.Drawing.Color.Blue,
                        Width = 2.0
                    };

                    // Create a graphic for the point with the symbol and geometry.
                    var graphic = new Graphic(point, symbol);

                    // Add the graphic to the overlay.
                    _graphicsOverlays.FirstOrDefault()?.Graphics.Add(graphic);
                }
            }
        }
    }
}