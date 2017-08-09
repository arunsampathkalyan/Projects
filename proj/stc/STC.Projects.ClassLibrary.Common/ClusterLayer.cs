using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;



namespace STC.Projects.ClassLibrary.Common
{
    public class ClusterLayer : GraphicsLayer
    {
        #region Attributes

        double screenWidth;
        double ExtentWidth;
        Map MyMap;
        MapView MyMapView;
        double ClusterResolution;
        double ClusterTolerance = 50;
        GraphicCollection ClusterData = null;
        List<Cluster> Clusters;
        List<Graphic> Singles;
        Color ClusterLabelColor = Colors.Black;
        SimpleMarkerSymbol SingleSym = new SimpleMarkerSymbol();
        decimal MaxSingles = 1000;
        SpatialReference spatialReference = new SpatialReference(102100);
        double ClusterLabelOffset = 5;
        public bool ShowSingles = true;
        #endregion



        #region Properties
        #endregion
        public ClusterLayer(Options options)
        {
            this.ClusterTolerance = options.distance;
            if (options.data != null)
                this.ClusterData = options.data;
            this.Clusters = new List<Cluster>();
            if (options.labelColor != null)
                this.ClusterLabelColor = options.labelColor;
            // labelOffset can be zero so handle it differently
            this.ClusterLabelOffset = options.labelOffset;
            // graphics that represent a single point
            this.Singles = new List<Graphic>(); // populated when a graphic is clicked
            // symbol for single graphics
            if (options.singleSymbol != null)
                this.SingleSym = options.singleSymbol;
            else
            {
                this.SingleSym = new SimpleMarkerSymbol();
                this.SingleSym.Color = Colors.Brown;
                this.SingleSym.Size = 10;
                this.SingleSym.Style = SimpleMarkerStyle.Circle;

            }
            if (options.maxSingles != null)
                this.MaxSingles = options.maxSingles.Value;

            this.spatialReference = options.spatialReference;

        }


        public void SetMap(Map map, MapView mapView)
        {

            MyMap = map;
            MyMapView = mapView;
            // calculate and set the initial resolution
            this.ClusterResolution = CalculateResloution();
            this.ClusterGraphics();

            MyMapView.ExtentChanged += (object sender, EventArgs e) =>
            {
                this.ClusterResolution = CalculateResloution();
                this.Clear();
                this.ClusterGraphics();
            };
        }

        private double CalculateResloution()
        {
            screenWidth = Application.Current.MainWindow.ActualWidth;
            if (Application.Current.MainWindow.ActualWidth == 0)
            {
                screenWidth = Application.Current.MainWindow.Width;
            }
            if (MyMapView.Extent != null)
            {
                ExtentWidth = MyMapView.Extent.XMax - MyMapView.Extent.XMin;
                // update resolution
                return ExtentWidth / screenWidth;
            }
            else
                return 0;
        }



        void UnsetMap()
        {
            MyMapView.ExtentChanged += null;
        }
        void Clear()
        {
            // Summary:  Remove all clusters and data points.
            if (this.Clusters != null)
                this.Clusters.Clear();

            if (this.Graphics != null)
                this.Graphics.Clear();
        }
        internal void ClearSingles(List<Graphic> singles)
        {
            List<Graphic> s;
            if (singles == null)
            {
                s = singles;
            }
            else
            {
                s = this.Singles;
            }
            foreach (var g in s)
            {
                s.Remove(g);
            }
        }
        void ClusterGraphics()
        {
            // first time through, loop through the points
            for (int j = 0, jl = this.ClusterData.Count; j < jl; j++)
            {
                // see if the current feature should be added to a cluster
                var point = this.ClusterData[j];
                var clustered = false;
                var numClusters = this.Clusters.Count;
                for (var i = 0; i < this.Clusters.Count; i++)
                {
                    var c = this.Clusters[i];
                    if (this.ClusterTest(point.Geometry.Extent, c))
                    {
                        this.ClusterAddPoint(point, c);
                        clustered = true;
                        break;
                    }
                }

                if (!clustered)
                {
                    this.ClusterCreate(point.Geometry.Extent);
                }
            }
            this.ShowAllClusters();
        }

        bool ClusterTest(Envelope p, Cluster cluster)
        {
            var distance = (Math.Sqrt(Math.Pow((cluster.X - p.XMin), 2) + Math.Pow((cluster.Y - p.YMin), 2)) / this.ClusterResolution);
            return (distance <= this.ClusterTolerance);
        }

        void ClusterAddPoint(Graphic p, Cluster cluster)
        {
            // average in the new point to the cluster geometry
            cluster.clusterAttributes.extent = p.Geometry.Extent;
            // increment the count
            cluster.clusterAttributes.clusterCount++;
            p.Attributes["clusterId"] = cluster.clusterAttributes.clusterId;

        }
        void ClusterCreate(Envelope p)
        {
            var clusterId = this.Clusters.Count + 1;
            // create the cluster
            Cluster cluster = new Cluster();
            cluster.X = p.XMin;
            cluster.Y = p.YMin;
            cluster.clusterAttributes = new attributes();
            cluster.clusterAttributes.clusterId = clusterId;
            cluster.clusterAttributes.extent = p;
            this.Clusters.Add(cluster);
        }
        void ShowAllClusters()
        {
            for (int i = 0, il = this.Clusters.Count; i < il; i++)
            {
                var c = this.Clusters[i];
                this.ShowCluster(c);
            }
        }
        void ShowCluster(Cluster c)
        {

            //edited 20/12/2015
            Graphic g = new Graphic();
            if (c.clusterAttributes.clusterCount > 1)
            {
                MapPoint point = new MapPoint(c.X, c.Y, this.spatialReference);
                g.Geometry = point;
                g.Attributes.Add("clusterCount", c.clusterAttributes.clusterCount);
                g.Attributes.Add("clusterId", c.clusterAttributes.clusterId);
                this.Graphics.Add(g);

                // show number of points in the cluster
                TextSymbol label = new TextSymbol();
                label.Text = c.clusterAttributes.clusterCount.ToString();
                label.Color = ClusterLabelColor;
                label.XOffset = 0;
                label.YOffset = this.ClusterLabelOffset;
                Graphic g2 = new Graphic();
                g2.Geometry = point;
                g2.Attributes.Add("clusterCount", c.clusterAttributes.clusterCount);
                g2.Attributes.Add("clusterId", c.clusterAttributes.clusterId);
                g2.Symbol = label;
                this.Graphics.Add(g2);
            }

            //add your symbol ,check for attribute and the symbol
            // code below is used to not label clusters with a single point
            else
            {
                MapPoint point = new MapPoint(c.X, c.Y, this.spatialReference);
                g.Geometry = point;
                g.Symbol = SingleSym;
                g.Attributes.Add("clusterCount", c.clusterAttributes.clusterCount);
                g.Attributes.Add("clusterId", c.clusterAttributes.clusterId);
                this.Graphics.Add(g);
                return;
            }




        }
        void AddSingles(List<Envelope> singles)
        {
            foreach (var p in singles)
            {
                var g = new Graphic(p, this.SingleSym);
                g.Attributes.Add(new KeyValuePair<string, object>("single", "single"));
                this.Singles.Add(g);
                if (this.ShowSingles)
                {
                    this.Graphics.Add(g);
                }
            }
        }

        public void ClusterClick(int clusterid)
        {
            this.ClearSingles();

            // find single graphics that make up the cluster that was clicked
            // would be nice to use filter but performance tanks with large arrays in IE
            List<Envelope> singles = new List<Envelope>();
            for (int i = 0, il = this.ClusterData.Count; i < il; i++)
            {
                if (clusterid == int.Parse(ClusterData[i].Attributes["clusterId"].ToString()))
                {
                    singles.Add(ClusterData[i].Geometry as Envelope);
                }
            }
            this.AddSingles(singles);

        }

        private void ClearSingles()
        {
            foreach (var item in this.Graphics)
            {
                if (item.Attributes["single"] != null)
                    Graphics.Remove(item);


            }
            this.Singles.Clear();
        }


    }
    public class Options
    {

        //     Array of objects. Required. Object are required to have properties named x, y and attributes. The x and y coordinates have to be numbers that represent a points coordinates.
        public GraphicCollection data;
        //   distance:  Number?
        public double distance;
        //     Optional. The max number of pixels between points to group points in the same cluster. Default value is 50.
        //   labelColor:  String?
        public Color labelColor;
        //     Optional. Hex string or array of rgba values used as the color for cluster labels. Default value is #fff (white).
        //   labelOffset:  String?
        public double labelOffset;
        //     Optional. Number of pixels to shift a cluster label vertically. Defaults to -5 to align labels with circle symbols. Does not work in IE.
        //   resolution:  Number
        public double resolution;
        //     Required. Width of a pixel in map coordinates. Example of how to calculate: 
        //     map.extent.getWidth() / map.width
        //   showSingles:  Boolean?
        public Boolean? showSingles;
        //     Optional. Whether or graphics should be displayed when a cluster graphic is clicked. Default is true.
        //   singleSymbol:  MarkerSymbol?
        public SimpleMarkerSymbol singleSymbol;
        //     Marker Symbol (picture or simple). Optional. Symbol to use for graphics that represent single points. Default is a small gray SimpleMarkerSymbol.
        //   singleTemplate:  PopupTemplate?
        //     PopupTemplate</a>. Optional. Popup template used to format attributes for graphics that represent single points. Default shows all attributes as "attribute = value" (not recommended).
        //   maxSingles:  Number?
        //     Optional. Threshold for whether or not to show graphics for points in a cluster. Default is 1000.
        public long? maxSingles;

        //   webmap:  Boolean?
        //     Optional. Whether or not the map is from an ArcGIS.com webmap. Default is false.
        //   spatialReference:  SpatialReference?
        //     Optional. Spatial reference for all graphics in the layer. This has to match the spatial reference of the map. Default is 102100. Omit this if the map uses basemaps in web mercator.
        public SpatialReference spatialReference;
    }

    public class attributes
    {
        internal int clusterCount;
        internal int clusterId;
        internal Envelope extent;
    }
    public class Cluster
    {
        internal double X;
        internal double Y;
        internal attributes clusterAttributes;

    }
}
