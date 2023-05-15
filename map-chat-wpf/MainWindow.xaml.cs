using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace map_chat_wpf
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static readonly DependencyProperty MainMapViewProperty =
            DependencyProperty.Register(nameof(MainMapView), typeof(MapView), typeof(MainWindow), new PropertyMetadata(null));

        public MapView MainMapView
        {
            get { return (MapView)GetValue(MainMapViewProperty); }
            set { SetValue(MainMapViewProperty, value); }
        }

        private readonly MapViewModel _viewModel;

        public MainWindow()
        {
            MapPointsBuilder builder = new MapPointsBuilder();
            List<MapPoint> points = builder.BuildMapPoints("user input goes here");

            _viewModel = new MapViewModel(points);
            DataContext = _viewModel;

            MapPoint mapCenterPoint = new MapPoint(-118.805, 34.027, SpatialReferences.Wgs84);
            MainMapView.SetViewpoint(new Viewpoint(mapCenterPoint, 100000));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _messageText;

        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                NotifyPropertyChanged(nameof(MessageText));
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string userMessage = MessageText;
                // Call your Dialogflow and webhook services with userMessage here
                MessageText = string.Empty;
            }
        }
    }
}
