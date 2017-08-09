using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System.Windows;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;


namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    internal class PatrolDetailsViewModel
    {
        public double EventLatitude { get; set; }
        public double EventLongitude { get; set; }

        public SOPSources SOPSource { get; set; }

        public PatrolLastLocationDTO Patrol { get; set; }

        public long NotificationId { get; set; }

        public Dictionary<string, ObservableCollection<Graphic>> LayersGraphicsDictionary { get; set; }

        public PatrolDetailsViewModel()
        {
            Patrol = new PatrolLastLocationDTO();

            LayersGraphicsDictionary = new Dictionary<string, ObservableCollection<Graphic>>();
            GetLayerObservable();
        }

        public ObservableCollection<Graphic> GetLayerObservable()
        {
            ObservableCollection<Graphic> graphicCol = new ObservableCollection<Graphic>();
            if (LayersGraphicsDictionary.ContainsKey("DispatchingPatrol"))
                graphicCol = LayersGraphicsDictionary["DispatchingPatrol"];
            else {
                LayersGraphicsDictionary.Add("DispatchingPatrol", graphicCol);
            }
            return graphicCol;
        }

        public void AddLayerContent(string Type, double Latitude, double Longitude)
        {
            if (LayersGraphicsDictionary.ContainsKey("DispatchingPatrol")) {
                var graphic = CreateGraphic(Latitude, Longitude, GetMarkerImageUrl(Type));
                LayersGraphicsDictionary["DispatchingPatrol"].Add(graphic);
            }
        }

        private Graphic CreateGraphic(double Latitude, double Longitude, string IconPath)
        {
            CompositeSymbol compositeSymbol = new CompositeSymbol();
            PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();

            pictureMarkerSymbol.SetSourceAsync(new Uri(IconPath));

            compositeSymbol.Symbols.Add(pictureMarkerSymbol);

            var graphics = new Graphic(new MapPoint(Longitude, Latitude, new SpatialReference(4326)), compositeSymbol);
            return graphics;
        }

        private string GetMarkerImageUrl(string Type)
        {
            string strPath = @"pack://application:,,,/STC.Projects.WPFControlLibrary.SOPBox;component/" + @"images/marker/" + Type + "_marker.png";

            try
            {//Workaround to check if the image exists
                var tempImg = new System.Windows.Media.Imaging.BitmapImage(new Uri(strPath));

            }
            catch (Exception ex)
            {

                strPath = @"pack://application:,,,/STC.Projects.WPFControlLibrary.SOPBox;component/" + @"images/marker/event_marker.png";
            }
            return strPath;
        }
    }
}
