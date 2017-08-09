using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IdentifyModule;
using Infrastructure.Entities;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Layers;
using Infrastructure.CommonInterfaces;
using Infrastructure.Enums;
using Esri.ArcGISRuntime.Geometry;
using MappingModule;
using System.Xml.Linq;
using STC.Projects.ClassLibrary.DAL;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GisService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GisService.svc or GisService.svc.cs at the Solution Explorer and start debugging.
    public class GisService : IGisService
    {
        public List<AssetsDetailsViewDTO> GetAssets(List<MapPointDTO> mapPoints)
        {
            try
            {
                var res = new List<AssetsDetailsViewDTO>();
                var points = new List<Esri.ArcGISRuntime.Geometry.MapPoint>();
                foreach (var item in mapPoints)
                {
                    points.Add(new Esri.ArcGISRuntime.Geometry.MapPoint(item.Longitude, item.Latitude, new Esri.ArcGISRuntime.Geometry.SpatialReference(4326)));
                }
                var config = new ConfigurationModule.ConfigurationModule();
                config.Initialize();

                //config.IdentifyConfig
                var x = new IdentifyModule.IdentifyModule(config, null);
                var options = new Infrastructure.CommonEvents.IdentifyServiceParameters();
                options.MapSpatialReference = new Esri.ArcGISRuntime.Geometry.SpatialReference(4326);
                options.IdentifyGeometry = new Esri.ArcGISRuntime.Geometry.Polygon(points, options.MapSpatialReference);
                var list = x.Identify(options).Result;

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        var asset = new AssetsDetailsViewDTO();
                        asset.SerialNo = item.ReturnFeature != null && item.ReturnFeature.Attributes != null && item.ReturnFeature.Attributes.ContainsKey("CH_No") && item.ReturnFeature.Attributes["CH_No"] != null ? item.ReturnFeature.Attributes["CH_No"].ToString() : item.DisplayFieldValue;
                        asset.Name = item.ReturnFeature != null && item.ReturnFeature.Attributes != null && item.ReturnFeature.Attributes.ContainsKey("CH_No") && item.ReturnFeature.Attributes["CH_No"] != null ? item.ReturnFeature.Attributes["CH_No"].ToString() : item.DisplayFieldValue;

                        res.Add(asset);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public List<GraphicDTO> GetDriveTimePolygons(MapPointDTO point)
        {
            try
            {
                var res = new List<GraphicDTO>();
                var config = new ConfigurationModule.ConfigurationModule();
                config.Initialize();
                var identify = new IdentifyModule.IdentifyModule(config, null);
                var spcRef = new SpatialReference(4326);
                SymbolCreator _symbolCreator = new SymbolCreator(config);
                var networkAnalysisModule = new NetworkAnalysisModule.NetworkAnalysisModule(identify, config);
                // point.Extent.Extent
                var mobileAssetIdentifyLayerConfig = config.IdentifyConfig.MapServices.SelectMany(x => x.Layers).FirstOrDefault(x => x.ID == 1);
                var actionConfig = mobileAssetIdentifyLayerConfig.Actions.FirstOrDefault(x => x.Type == ActionType.ServiceArea.ToString());
                var timeRanges = actionConfig.TimeRanges.Select(x => Convert.ToDouble(x.Time)).ToArray();
                Graphic g = new Graphic();
                g.Geometry = new MapPoint(point.Longitude, point.Latitude, spcRef);
                List<Feature> serviceAreaPolygons = networkAnalysisModule.GetServiceArea(spcRef, g, timeRanges).Result;
                var polygonHighlightConfig = config.ZoomAndHighlightConfig.Highlight.Symbols.Polygon;
                if (serviceAreaPolygons != null && serviceAreaPolygons.Any())
                {
                    for (int i = 0; i < serviceAreaPolygons.Count; i++)
                    {
                        var polygonColor = actionConfig.TimeRanges[i].Color;
                        var symbol = _symbolCreator.CreatePolygonSymbol(polygonColor, polygonHighlightConfig.OutlineColor, polygonHighlightConfig.Thickness);

                        //var graphic = new Graphic()
                        //{
                        //    Symbol = symbol,
                        //    Geometry = serviceAreaPolygons[i].Geometry
                        //};
                        var grpDto = new GraphicDTO();
                        grpDto.polygonObject = serviceAreaPolygons[i].Geometry.ToJson();
                        grpDto.symbolObject = symbol.ToJson();
                        res.Add(grpDto);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public List<PolygonTimeColorOutputDTO> GetPolygonTimeAndColor()
        {
            List<PolygonTimeColorOutputDTO> timeList = new List<PolygonTimeColorOutputDTO>();



            var doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + @"\bin\XMLConfigurations\Identify.xml");
            var address = doc.Root.Element("MapServices").Element("MapService").Element("Layers").Elements("Layer").ToList();
            if (address != null)
            {
                var layer = address.ElementAt(1);

                var actions = layer.Element("Actions").Elements("Action").ToList();

                if (actions != null)
                {
                    var Times = actions.ElementAt(3);

                    var timesList = Times.Element("TimeRanges").Elements("Range").ToList();

                    if (timesList != null && timesList.Count > 0)
                    {
                        for (int i = 0; i < timesList.Count; i++)
                        {
                            var output = new PolygonTimeColorOutputDTO();



                            output.RGB = timesList[i].Attribute("Color").Value;

                            int time = 0;

                            int.TryParse(timesList[i].Attribute("Time").Value, out time);

                            output.Time = time;

                            timeList.Add(output);
                        }
                    }
                }
            }

            return timeList;
        }

        public void UpdatePolygonTimeAndColor(List<PolygonTimeColorDTO> timeList)
        {
            if (timeList == null || timeList.Count < 3)
                return;

            var doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + @"\bin\XMLConfigurations\Identify.xml");
            var address = doc.Root.Element("MapServices").Element("MapService").Element("Layers").Elements("Layer").ToList();
            if (address != null)
            {
                var layer = address.ElementAt(1);

                var actions = layer.Element("Actions").Elements("Action").ToList();

                if (actions != null)
                {
                    var Times = actions.ElementAt(3);

                    var timesList = Times.Element("TimeRanges").Elements("Range").ToList();

                    if (timesList != null && timesList.Count > 0)
                    {
                        for (int i = 0; i < timesList.Count; i++)
                        {
                            string color = timeList[i].Opacity + "," + timeList[i].Red + "," + timeList[i].Green + "," + timeList[i].Blue;
                            timesList[i].Attribute("Time").Value = timeList[i].Time.ToString();
                            timesList[i].Attribute("Color").Value = color;
                        }
                    }
                }
            }
            doc.Save(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + @"\bin\XMLConfigurations\Identify.xml");
        }
    }
}
