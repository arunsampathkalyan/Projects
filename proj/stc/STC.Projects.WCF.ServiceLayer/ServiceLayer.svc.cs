using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DAL;
using System.Linq;
using STC.Projects.ClassLibrary.Main;
using System.Configuration;
using System.Xml.Serialization;
using System.Globalization;
using System.ServiceModel.Web;
using System.Messaging;
using STC.Projects.ClassLibrary.TFMIntegration;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using STC.Projects.WCF.ServiceLayer.Request;
using System.Web;
using System.Drawing;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: In  to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceLayer : IServiceLayer
    {
        private string _analysisViolationConnectionString = ConfigurationManager.AppSettings["AnalysisViolationConnectionString"];
        private string _analysisTruckViolationConnectionString = ConfigurationManager.AppSettings["AnalysisTruckViolationConnectionString"];
        private string _analysisIncidentConnectionString = ConfigurationManager.AppSettings["AnalysisIncidentConnectionString"];
        private string _analysisDangerousViolationConnectionString = ConfigurationManager.AppSettings["AnalysisDangerousViolationConnectionString"];
        private int _oldPeriod = Utility.GetNotificationPendingPeriod();
        [AspNetCacheProfile("CacheForAssets")]
        [WebGet()]
        public List<AssetsViewDTO> GetAllAssets()
        {
            var res = new List<AssetsViewDTO>();
            res = new AssetsDAL().GetAssetsList(null, null);
            return res;
        }

        public AssetViolationDetailsDTO GetAssetViolations(string originalIdent, string lang)
        {
            AssetViolationDetailsDTO asset = new AssetViolationDetailsDTO();

            AssetsViewDTO assetData = new AssetsDAL().GetAssetDataById(int.Parse(originalIdent));

            if (assetData != null)
            {
                asset.AssetCode = assetData.SerialNo;
                asset.AssetVendor = assetData.ItemCategoryName;
                asset.AssetLocation = assetData.AreaName;

                List<ViolationNotificationDTO> AssetViolations = new ViolationNotificationDAL().GetViolationsHistoryListByAsset(assetData.SerialNo, new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0));

                if (AssetViolations != null && AssetViolations.Count > 0)
                {
                    asset.AssetViolationCountYearly = AssetViolations.Count;
                    asset.AssetViolationCount = AssetViolations.Where(x => x.DateTaken >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)).Count();
                    asset.AssetViolationCountMonth = AssetViolations.Where(x => x.DateTaken >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0)).Count();
                    asset.AssetPieKPIs = new CubeDTO();
                    asset.AssetPieKPIs.LegendName = lang == "ar" ? "رسم توضيحي لعدد المخالفات من حيث النوع في أخر 12 شهر" : "Violations count per type in the last 12 month";
                    asset.AssetPieKPIs.Details = new System.Collections.ObjectModel.ObservableCollection<CubeDetailsDTO>();

                    asset.AssetLinerKPIs = new List<CubeDTO>();
                    CubeDTO LineKPI = new CubeDTO();
                    LineKPI.LegendName = lang == "ar" ? "عدد المخالفات على مدار العام الأخير" : "Violations count for the last 12 months";
                    LineKPI.Details = new System.Collections.ObjectModel.ObservableCollection<CubeDetailsDTO>();

                    List<string> ViolationTypes = AssetViolations.Select(x => x.ViolationTypeName).Distinct().ToList();
                    List<int> ViolationMonths = AssetViolations.Select(x => x.DateTaken.Month).Distinct().ToList();

                    foreach (string type in ViolationTypes)
                    {
                        CubeDetailsDTO detail = new CubeDetailsDTO();
                        detail.Key = type == null ? "N/A" : type;
                        detail.Value = AssetViolations.Where(x => x.ViolationTypeName == type).ToList().Count;

                        asset.AssetPieKPIs.Details.Add(detail);
                    }

                    foreach (int month in ViolationMonths)
                    {
                        CubeDetailsDTO detail = new CubeDetailsDTO();
                        detail.Key = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                        detail.Value = AssetViolations.Where(x => x.DateTaken.Month == month).ToList().Count;

                        LineKPI.Details.Add(detail);
                    }

                    asset.AssetLinerKPIs.Add(LineKPI);
                }
            }

            return asset;
        }

        public void AddToViolationQueue(string ViolationTypeName, int ViolationTypeId, string PlateNumber, string PlateKind, string PlateColor, string PlateSource, double Latitude, double Longitude, int VehicleType, bool IsInsideCity, int SpeedLimit, int SpeedTolerance, int CurrentSpeed, int TrafficCrossElapsedTimeSecs, string AssetSerialNumber)
        {
            MessageQueue msgQ = new MessageQueue(ConfigurationSettings.AppSettings["MessageQueuePath"]);
            var item = new XYPayload();
            item.EventTime = DateTime.UtcNow;
            item.Message = ViolationTypeName;
            item.TransporterPlateNumber = string.Format("{0},{1},{2},{3},{4},{5}", PlateNumber, PlateKind, PlateSource, PlateColor, VehicleType, AssetSerialNumber);
            item.ViolationTypeId = ViolationTypeId;
            item.Lat = Latitude;
            item.Lon = Longitude;
            item.IsInsideCity = IsInsideCity;
            int OverSpeedByKM = CurrentSpeed - SpeedTolerance;
            item.OverSpeedKM = OverSpeedByKM;
            item.TrafficLightSecs = TrafficCrossElapsedTimeSecs;
            System.Messaging.Message msg = new System.Messaging.Message
            {
                Label = "Add Violation",
                Body = item,
                UseDeadLetterQueue = true
            };

            msgQ.Send(msg);
        }

        public void AddTruckViolationEvent(string plateNumber, DateTime violationDate, double lat, double lon, string lang, string assetSerial)
        {
            string assetId = "";

            var asset = GetAllAssets().FirstOrDefault(x => x.SerialNo.ToLower().Trim() == assetSerial.ToLower().Trim());
            if (asset == null)
                return;

            assetId = asset.ItemId.ToString();

            string[] plateNumberArr = plateNumber.Split(',');
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            string pNumber = plateNumber;
            if (plateNumberArr.Length == 4)
            {
                pNumber = plateNumberArr[0];
                string pCategory = plateNumberArr[1];
                string pAuthority = plateNumberArr[2];
                string pColor = plateNumberArr[3];

                violationNotificationDAL.AddWantedVehicle(pNumber, pCategory, pAuthority, pColor, violationDate);
            }

            var notificationBox = new MapNotificationPopUpDTO
            {
                Lat = lat,
                Lon = lon,
                TypeName = "Truck Violation",
                GeneralType = 13,
                Discription = "Truck permission violation"
            };

            var notification = lang == "ar" ? "مخالفة شاحنة - " : "Truck Violation - ";

            notificationBox.Discription = notification + pNumber;

            var tab = new TabDTO
            {
                TabName = "Event Details"
            };

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Event Type",
                ValueName = "Truck Violation"
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Tower Id",
                ValueName = assetId.ToString()
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Event Id",
                ValueName = ((int)SOPSources.TruckViolation).ToString()
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Truck Number",
                ValueName = pNumber
            });

            notificationBox.Tabs.Add(tab);

            XmlSerializer serializer = new XmlSerializer(typeof(MapNotificationPopUpDTO));
            var stringwriter = new System.IO.StringWriter();
            serializer.Serialize(stringwriter, notificationBox);
            long messageId = 0;
            var isEventSavedAsync = AddNewEvent(stringwriter.ToString(), null, out messageId);
        }

        public void AddWantedCarEventManualy(string plateNumber, string plateCategory, string plateSource, string plateColor, DateTime violationDate, string lang, string ruleName, string ruleId, double lat, double lon)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            violationNotificationDAL.AddWantedVehicle(plateNumber, plateCategory, plateSource, plateColor, violationDate, reason: ruleName);
            AddWantedNotification(lat, lon, lang, plateNumber, plateCategory, plateSource, plateColor, "0", ruleId);
        }

        public void AddWantedCarEvent(string plateNumber, DateTime violationDate, string lang, string ruleName, string ruleId)
        {
            string[] plateNumberArr = plateNumber.Split(',');
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            string pNumber = plateNumber;
            string pCategory = "Undefined";
            string pAuthority = "Undefined";
            string pColor = "Undefined";
            string assetId = string.Empty;
            double lat = 0;
            double lon = 0;
            string serialNumber = "";



            if (plateNumberArr.Length >= 4)
            {
                pNumber = plateNumberArr[0];
                pCategory = plateNumberArr[1];
                pAuthority = plateNumberArr[2];
                pColor = plateNumberArr[3];

                violationNotificationDAL.AddWantedVehicle(pNumber, pCategory, pAuthority, pColor, violationDate, reason: ruleName);
                bool getAsset = false;
                if (plateNumberArr.Length >= 6)
                {
                    serialNumber = plateNumberArr[5];
                    var assets = GetAllAssets();
                    if (assets == null)
                        return;
                    var asset = assets.Where(x => x.SerialNo == serialNumber).FirstOrDefault();
                    if (asset != null && asset.Longitude.HasValue && asset.Latitude.HasValue)
                    {
                        lat = asset.Latitude.Value;
                        lon = asset.Longitude.Value;
                        assetId = asset.ItemId.ToString();
                        getAsset = true;
                    }
                }
                if (!getAsset)
                {
                    ViolationNotificationDTO vehicle = violationNotificationDAL.GetVehicleLastViolation(pNumber, pCategory != "" ? pCategory : "0", pAuthority != "" ? pAuthority : "0", pColor != "" ? pColor : "0");
                    if (vehicle != null && vehicle.Latitude.HasValue && vehicle.Longitude.HasValue)
                    {
                        lat = vehicle.Latitude.Value;
                        lon = vehicle.Longitude.Value;
                        assetId = vehicle.AssetId;
                    }
                    else
                    {
                        // just workaround as plate color not saved successful on OP DB
                        vehicle = violationNotificationDAL.GetVehicleLastViolation(pNumber, "0", "0", "0");
                        if (vehicle != null && vehicle.Latitude.HasValue && vehicle.Longitude.HasValue)
                        {
                            lat = vehicle.Latitude.Value;
                            lon = vehicle.Longitude.Value;
                            assetId = vehicle.AssetId;
                        }
                    }

                }
            }
            else
            {
                VehicleLiveTrackingDTO vehicle = new VehicleLiveTrackingDAL().GetVehicleDetailsByPlateNumber(plateNumberArr[0]);
                if (vehicle == null)
                    return;
                pNumber = plateNumberArr[0];
                lat = vehicle.Latitude.Value;
                lon = vehicle.Longitude.Value;
                assetId = vehicle.TowerId.ToString();
            }

            if (lat == 0 || lon == 0 || assetId == "")
                return;
            AddWantedNotification(lat, lon, lang, pNumber, pCategory, pAuthority, pColor, assetId.ToString(), ruleId, serialNumber);

        }

        private bool ValidateNewRules(string serialNumber, int ruleId, string plateNumber, string plateSource, string plateKind, string plateColor, out bool isNewRule)
        {
            isNewRule = true;
            if (System.Configuration.ConfigurationSettings.AppSettings["IsNewDynamicRules"] != null)
                bool.TryParse(System.Configuration.ConfigurationSettings.AppSettings["IsNewDynamicRules"].ToString(), out isNewRule);
            if (!isNewRule)
                return true;
            try
            {
                var res = true;

                var rule = new DynamicRulesDAL().GetRuleById(ruleId);

                if (rule != null)
                {
                    if (rule.LocationsDetails != null && serialNumber != "")
                    {
                        var allAssets = new GisService().GetAssets(rule.LocationsDetails.RegionPoints);
                        if (allAssets == null || !allAssets.Any(x => x.SerialNo == serialNumber))
                        {
                            return false;
                        }
                    }
                    if (rule.TimeDetails != null)
                    {
                        if (rule.TimeDetails.TimeType == (int)ScheduleType.SPECIFIC && rule.TimeDetails.FromDate.HasValue && rule.TimeDetails.ToDate.HasValue)
                        {
                            if (DateTime.Now.Date < rule.TimeDetails.FromDate.Value || DateTime.Now.Date > rule.TimeDetails.ToDate.Value)
                                return false;
                        }
                        if (rule.TimeDetails.TimeType == (int)ScheduleType.WEEKDAYS && rule.TimeDetails.WeekDays.Any())
                        {
                            if (!rule.TimeDetails.WeekDays.Any(x => x == (int)DateTime.Now.DayOfWeek))
                                return false;
                        }
                        if (rule.TimeDetails.FromTime.HasValue)
                        {
                            if (DateTime.Now.TimeOfDay < rule.TimeDetails.FromTime.Value)
                                return false;
                        }
                        if (rule.TimeDetails.ToTime.HasValue)
                        {
                            if (DateTime.Now.TimeOfDay > rule.TimeDetails.ToTime.Value)
                                return false;
                        }
                    }
                    if (rule.DriverDetails != null || rule.VehicleDetails != null)
                    {
                        int plateColorCode = -1;
                        int plateKindCode = -1;
                        int plateSourceCode = -1;

                        int.TryParse(plateColor, out plateColorCode);
                        int.TryParse(plateKind, out plateKindCode);
                        int.TryParse(plateSource, out plateSourceCode);

                        var allColor = new ADPUTSService().GetVehiclePlateColor();
                        var allKind = new ADPUTSService().GetVehiclePlateKind();
                        var allSources = new ADPUTSService().GetVehiclePlateSource();
                        if (!allColor.Any(x => x.Name.ToLower().Trim() == plateColor.ToLower().Trim()) || !allKind.Any(x => x.Name.ToLower().Trim() == plateKind.ToLower().Trim()) || !allSources.Any(x => x.Name.ToLower().Trim() == plateSource.ToLower().Trim()) || !allColor.Any(x => x.Code == plateColorCode) || !allKind.Any(x => x.Code == plateKindCode) || !allSources.Any(x => x.Code == plateSourceCode))
                            return false;

                        var utsReq = new VehicleDetailsRequest()
                        {
                            ChassisNoExist = true,
                            PlateColorCode = plateColorCode != -1 ? plateColorCode : allColor.FirstOrDefault(x => x.Name.ToLower().Trim() == plateColor.ToLower().Trim()).Code,
                            PlateKindCode = plateKindCode != -1 ? plateKindCode : allKind.FirstOrDefault(x => x.Name.ToLower().Trim() == plateKind.ToLower().Trim()).Code,
                            PlateNo = plateNumber,
                            PlateSourceCode = plateSourceCode != -1 ? plateSourceCode : allSources.FirstOrDefault(x => x.Name.ToLower().Trim() == plateSource.ToLower().Trim()).Code,
                        };

                        var utsRes = new ADPUTSService().GetVehicleDetails(utsReq);
                        if (utsRes == null)
                            return false;

                        if (rule.DriverDetails != null)
                        {
                            var profileRq = new TrafficProfileRequest
                            {
                                TcfNo = long.Parse(utsRes.OwnerTcfNo)
                            };

                            var driverRes = new ADPUTSService().GetTrafficProfile(profileRq);
                            if (driverRes == null)
                                return false;

                            var driverAge = DateTime.Now.Date.Year - driverRes.BirthDate.Year;
                            if (rule.DriverDetails.MinAge > driverAge)
                                return false;
                            if (rule.DriverDetails.MaxAge < driverAge)
                                return false;
                        }

                        if (rule.VehicleDetails != null)
                        {
                            if (rule.VehicleDetails.VehicleBrand != null)
                            {
                                if (rule.VehicleDetails.VehicleBrand.VehicleBrandId.ToString() != utsRes.MakeCode)
                                    return false;
                            }
                            if (rule.VehicleDetails.VehicleType != null)
                            {
                                if (rule.VehicleDetails.VehicleType.VehicleTypeID.ToString() != utsRes.TypeCode)
                                    return false;
                            }
                            if (rule.VehicleDetails.VehicleYear.HasValue)
                            {
                                if (rule.VehicleDetails.VehicleYear.Value.ToString() != utsRes.Year.ToString())
                                    return false;
                            }
                        }
                        // Need UTS
                    }

                }
                return res;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        private void AddWantedNotification(double lat, double lon, string lang, string pNumber, string pCategory, string pAuthority, string pColor, string assetId, string ruleId, string assetSerialNumber = "")
        {

            var notificationBox = new MapNotificationPopUpDTO
            {
                Lat = lat,
                Lon = lon,
                TypeName = "Wanted Car",
                GeneralType = 8,
                Discription = "New dangerous vehicle Event"
            };

            var notification = lang == "ar" ? " مركبة خطرة - " : "Dangerous Vehicle - ";

            notificationBox.Discription = notification + pNumber;

            var tab = new TabDTO
            {
                TabName = "Event Details"
            };

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Event Type",
                ValueName = "Wanted Car"
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Tower Id",
                ValueName = assetId
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Event Id",
                ValueName = ((int)SOPSources.WantedCar).ToString()
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Vehicle Number",
                ValueName = pNumber
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Plate Kind",
                ValueName = pCategory
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Plate Type",
                ValueName = pCategory
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Plate Authority",
                ValueName = pAuthority
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "Plate Color",
                ValueName = pColor
            });

            var businessRuleId = 0;
            bool isNewRules = false;
            int? priorityId = null;
            string ruleName = "";
            if (int.TryParse(ruleId, out businessRuleId))
            {
                var isValid = ValidateNewRules(assetSerialNumber, businessRuleId, pNumber, pAuthority, pCategory, pColor, out isNewRules);

                if (!isValid)
                    return;
                if (isNewRules)
                {
                    var rule = new DynamicRulesDAL().GetRuleById(businessRuleId);
                    if (rule != null && rule.Priority != null)
                    {
                        priorityId = rule.Priority.PriorityID;
                        ruleName = rule.RuleName;
                    }

                }
                else
                {
                    var role = new BusinessRuleDAL().GetBusinessRuleByID(businessRuleId);
                    if (role != null)
                    {
                        priorityId = role.PriorityId;
                    }
                }
            }

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "PriorityId",
                ValueName = priorityId.HasValue ? priorityId.Value.ToString() : ""
            });

            tab.Attributes.Add(new TabItemDTO
            {
                KeyName = "RuleName",
                ValueName = ruleName
            });


            notificationBox.Tabs.Add(tab);

            XmlSerializer serializer = new XmlSerializer(typeof(MapNotificationPopUpDTO));
            var stringwriter = new System.IO.StringWriter();
            serializer.Serialize(stringwriter, notificationBox);


            long messageId = 0;
            var isEventSavedAsync = AddNewEvent(stringwriter.ToString(), priorityId, out messageId);
            if (businessRuleId > 0)
            {
                if (isNewRules)
                {
                    new BusinessRuleDAL().UpdateOccurNumber(businessRuleId, messageId, pNumber, pCategory, pAuthority, pColor);
                }
            }
        }
        public BusinessRulesDTO GetMessageBusinessRule(long messageId)
        {
            return new BusinessRuleDAL().GetBusinessRuleByMessageId(messageId);
        }

        public DangerousVehicleDetailsDTO GetDangerousVehicleDetailsByPlateNumber(string plateNumber, string lang)
        {
            DangerousVehicleDetailsDTO details = new DangerousVehicleDetailsDTO();

            details.VehicleViolations = new ViolationNotificationDAL().GetViolationsHistoryListByDate(DateTime.Now.AddYears(-1), DateTime.Now, plateNumber);
            if (details.VehicleViolations != null && details.VehicleViolations.Count > 0)
            {
                details.VehiclePieKPIs = new CubeDTO();

                details.VehiclePieKPIs.LegendName = lang == "ar" ? "رسم توضيحي لعدد المخالفات من حيث النوع في أخر 12 شهر" : "Violations count per type in the last 12 month";
                details.VehiclePieKPIs.Details = new System.Collections.ObjectModel.ObservableCollection<CubeDetailsDTO>();

                details.VehicleLinerKPIs = new List<CubeDTO>();
                CubeDTO LineKPI = new CubeDTO();
                LineKPI.LegendName = lang == "ar" ? "عدد المخالفات على مدار العام الأخير" : "Violations count for the last 12 months";
                LineKPI.Details = new System.Collections.ObjectModel.ObservableCollection<CubeDetailsDTO>();

                details.PlateNumber = details.VehicleViolations.First().PlateNumber;
                details.PlateKind = details.VehicleViolations.First().PlateKindName;
                details.PlateSource = details.VehicleViolations.First().PlateSourceName;
                details.PlateColor = details.VehicleViolations.First().PlateColorName;

                List<string> ViolationTypes = details.VehicleViolations.Select(x => x.ViolationTypeName).Distinct().ToList();
                List<int> ViolationMonths = details.VehicleViolations.Select(x => x.DateTaken.Month).Distinct().ToList();

                foreach (string type in ViolationTypes)
                {
                    CubeDetailsDTO detail = new CubeDetailsDTO();
                    detail.Key = type == null ? "N/A" : type;
                    detail.Value = details.VehicleViolations.Where(x => x.ViolationTypeName == type).ToList().Count;

                    details.VehiclePieKPIs.Details.Add(detail);
                }

                foreach (int month in ViolationMonths)
                {
                    CubeDetailsDTO detail = new CubeDetailsDTO();
                    detail.Key = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    detail.Value = details.VehicleViolations.Where(x => x.DateTaken.Month == month).ToList().Count;

                    LineKPI.Details.Add(detail);
                }

                details.VehicleLinerKPIs.Add(LineKPI);
            }

            return details;
        }

        public bool AddNewEvent(string XmlToSend, int? priorityId, out long MessageId)
        {
            EventsDAL eventsDAL = new EventsDAL();
            return eventsDAL.AddNewEvent(XmlToSend, priorityId, out MessageId);
        }

        public bool AddNewEvent(string XmlToSend)
        {
            EventsDAL eventsDAL = new EventsDAL();
            long msgId = 0;
            return eventsDAL.AddNewEvent(XmlToSend, null, out msgId);
        }

        public List<CubeDTO> GetTruckViolationTarget()
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationTarget();
        }

        public List<CubeDTO> GetTruckViolationRegionYearly()
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationRegionWiseYearly();
        }

        public List<CubeDTO> GetTruckViolationRegionMonthly(int year)
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetTruckViolationRegionWeekly(int year)
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationRegionWiseWeekly(year);
        }

        public List<CubeDTO> GetTruckViolationRegionDaily(DateTime WeekStartDate)
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);
            DateTime endDate = WeekStartDate.AddDays(7);
            return cubeDAL.GetTruckViolationRegionWiseDaily(WeekStartDate, endDate);
        }
        public List<CubeDTO> GetViolationCountMonthAndCity()
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisViolationConnectionString);

            return cubeDAL.GetViolationCountMonthAndCity();
        }

        public List<CubeDTO> GetViolationCountPerSpeedAndTime()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationPerSpeedAndTime();
        }

        public decimal GetWantedCarRadius()
        {
            return Properties.Settings.Default.WantedCarRadius;
        }

        public List<ViolationsGroupedByLocationsDTO> GetViolationsListGroupedByLocation(DateTime? StartDateTime = null, int? ViolationTypeId = null)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationsListGroupedByLocation(StartDateTime, ViolationTypeId);
        }

        public List<ViolationsHistoricalDTO> GetViolationHistoricalViewByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationHistoricalViewByDate(StartDateTime, EndDateTime, ScheduleType);
        }

        public List<IncidentHistoricalDTO> GetIncidentHistoricalViewByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType)
        {
            IncidentsDAL incidentsDAL = new IncidentsDAL();
            return incidentsDAL.GetIncidentHistoricalViewByDate(StartDateTime, EndDateTime, ScheduleType);
        }

        public List<IncidentsDTO> GetIncidentsHistoricalListByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType)
        {
            IncidentsDAL incidentsDAL = new IncidentsDAL();
            return incidentsDAL.GetIncidentssHistoryListByDate(StartDateTime, EndDateTime);
        }

        public IncidentsDTO GetIncidentHistoricalDetails(int IncidentId)
        {
            IncidentsDAL incidentsDAL = new IncidentsDAL();
            return incidentsDAL.GetIncidentHistoricalDetail(IncidentId);
        }
        public List<ViolationNotificationDTO> GetViolationsHistorySearchByDate(DateTime? StartDateTime, DateTime? EndDateTime, string PlateNumber)
        {
            return new ViolationNotificationDAL().GetViolationsHistoryListByDate(StartDateTime, EndDateTime, PlateNumber);
        }

        public List<ViolationNotificationDTO> GetDangerousVehicleViolations(DateTime? StartDateTime, DateTime? EndDateTime, string PlateNumber)
        {
            return new ViolationNotificationDAL().GetViolationsHistoryListByDate(StartDateTime, EndDateTime, PlateNumber);
        }

        //public List<ViolationNotificationDTO> GetViolationsSearchHistory(DateTime? StartDateTime = null,DateTime? EndDateTime=null,string PlateNumber="")
        //{
        //    ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
        //    return violationNotificationDAL.GetViolationsListGroupedByLocation(StartDateTime, ViolationTypeId);
        //}

        public List<ViolationsDetailsByLocationDTO> GetViolationsDetailsForLocation(string LocationCode, DateTime? StartDateTime = null, int? ViolationTypeId = null)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationsDetailsForLocation(LocationCode, StartDateTime, ViolationTypeId);
        }

        public List<ViolationNotificationDTO> GetUpdatedViolationsList(bool? IsNoticed = null)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetUpdatedViolationsList(IsNoticed);
        }

        public List<ViolationNotificationDTO> GetViolationListByVehicle(string plateNumber)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationListByVehicle(plateNumber);
        }

        public List<IncidentsDTO> GetActiveIncidentsList()
        {
            IncidentsDAL incidentsDAL = new IncidentsDAL();

            return incidentsDAL.GetActiveIncidentsList(null);
        }

        public IncidentsDTO GetIncidentDetails(int IncidentId)
        {
            IncidentsDAL incidentsDAL = new IncidentsDAL();

            return incidentsDAL.GetIncidentDetails(IncidentId);
        }

        public IncidentDetailsDTO GetIncidentFullDetails(int IncidentId)
        {
            var incidentFullDetails = new IncidentDetailsDTO();
            IncidentsDAL incidentsDAL = new IncidentsDAL();

            var incident = incidentsDAL.GetIncidentDetails(IncidentId);
            incidentFullDetails = new IncidentDetailsDTO
            {
                Lat = incident.Latitude.HasValue ? incident.Latitude.Value : 0,
                Lon = incident.Longitude.HasValue ? incident.Longitude.Value : 0,
                AssignedPatrol = "001",
                Comments = incident.MessageText,
                DispatchingTime = incident.DispatcheTime.HasValue ? incident.DispatcheTime.Value.ToString() : "",
                IncidentAddress = incident.IncidentAddress,
                IncidentFinishTime = incident.EndTime.HasValue ? incident.EndTime.Value.ToString() : "",
                IncidentStatus = incident.StatusName,
                IncidentTime = incident.CreatedTime.HasValue ? incident.CreatedTime.Value.ToString() : "",
                IncidentType = incident.IncidentTypeName,
                PatrolArrived = incident.ArrivedTime.HasValue ? incident.ArrivedTime.Value.ToString() : ""
            };
            incidentFullDetails.XML = incidentFullDetails.GetXML();
            return incidentFullDetails;
        }

        public List<AssetsViewDTO> GetAssetsList(int? AssetStatusId = null, int? AssetTypeId = null)
        {
            AssetsDAL assetsDAL = new AssetsDAL();
            return assetsDAL.GetAssetsList(AssetStatusId, AssetTypeId);
        }

        public List<AssetsViewDTO> GetAssetsListBySource(int? AssetStatusId, int? AssetTypeId, int? sourceId)
        {
            AssetsDAL assetsDAL = new AssetsDAL();
            return assetsDAL.GetAssetsListBySource(AssetStatusId, AssetTypeId, sourceId);
        }

        public List<AssetLastStatusDTO> GetAssetsLastStatusList(bool? IsNoticed = null)
        {
            AssetsDAL assetsDAL = new AssetsDAL();
            return assetsDAL.GetAssetsLastStatusList(IsNoticed);
        }

        public List<PatrolLastLocationDTO> GetPatrolsList(int? PatrolStatusId = null)
        {
            PatrolsDAL patrolsDAL = new PatrolsDAL();
            return patrolsDAL.GetPatrolsList(PatrolStatusId);
            //return new TFMIntegrationService().GetPatrolsLocations();
        }
        public List<PatrolLastLocationDTO> GetAllPatrolsList()
        {
            return new PatrolsDAL().GetAllPatrols();
        }

        public bool ChangePatrolActivation(int patrolId, bool isDeleted)
        {
            var res = new PatrolsDAL().ChangePatrolActivation(patrolId, isDeleted);
            new CrsipServices().PrepareCars();
            return res;
        }
        public List<OfficersLastLocationViewDTO> GetOfficersList()
        {
            OfficersDAL officersDAL = new OfficersDAL();
            return officersDAL.GetUpdatedOfficersList(null);
        }

        public void GetEventsList()
        {
            EventsDAL eventsDAL = new EventsDAL();
            eventsDAL.GetEventsList();
        }


        public List<PatrolStatusDimDTO> GetPatrolStatusList()
        {
            PatrolStatusDAL patrolStatusDAL = new PatrolStatusDAL();
            return patrolStatusDAL.GetPatrolStatusList();
        }

        public List<AssetTypeDimDTO> GetAssetTypesList()
        {
            AssetTypeDAL assetTypeDAL = new AssetTypeDAL();
            return assetTypeDAL.GetAssetTypesList();
        }

        public List<AssetStatusDimDTO> GetAssetStatusList()
        {
            AssetStatusDAL assetStatusDAL = new AssetStatusDAL();
            return assetStatusDAL.GetAssetStatusList();
        }

        public List<ViolationTypeDimDTO> GetViolationTypesList()
        {
            ViolationTypeDAL violationTypeDAL = new ViolationTypeDAL();
            return violationTypeDAL.GetViolationTypesList();
        }


        public List<ViolationsCountPerDayOfWeekDTO> GetViolationsCountPerDayOfWeek()
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountPerDayOfWeek();
        }

        public List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedByType()
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountGroupedbyViolationType();
        }

        public List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedByTypePerDayOfWeek(string dayofweek)
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountGroupedbyViolationType(dayofweek);
        }

        public List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedByTypePerDayOfWeekAndHour(string dayofweek, int hour)
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountGroupedbyViolationType(dayofweek, hour);
        }

        public List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedByLocation()
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountGroupedbyLocation();
        }

        public List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedByLocationPerDayOfWeek(string dayofweek)
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountGroupedbyLocation(dayofweek);
        }

        public List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedByLocationPerDayOfWeekAndHour(string dayofweek, int hour)
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountGroupedbyLocation(dayofweek, hour);
        }


        public List<ViolationsCountPerDayOfWeekAndHourDTO> GetViolationsCountPerDayOfWeekAndHour()
        {
            ViolationsKPIsDAL violationsKPIsDAL = new ViolationsKPIsDAL();
            return violationsKPIsDAL.GetViolationsCountPerDayOfWeekAndHour();
        }
        public List<ViolationNotificationDTO> GetViolationsListByDate(DateTime? StartDateTime, DateTime? EndDateTime)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationsListByDate(StartDateTime, EndDateTime);
        }

        public List<IncidentsDTO> GetIncidentsListByDate(DateTime? StartDateTime, DateTime? EndDateTime)
        {
            IncidentsDAL incidentsDAL = new IncidentsDAL();
            return incidentsDAL.GetIncidentsListByDate(StartDateTime, EndDateTime);
        }

        public ViolationDetailsDTO GetViolationDetailsByAsset(string LocationCode, DateTime? StartDateTime = null, int? ViolationTypeId = null)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            ViolationDetailsDTO violationDetails = violationNotificationDAL.GetViolationDetailsByAsset(LocationCode, StartDateTime, ViolationTypeId);
            violationDetails.XML = violationDetails.GetXML();
            return violationDetails;
        }
        public PatrolOfficersDetailsDTO GetPatrolDetails(int PatrolId)
        {
            var patrolEntity = new PatrolsDAL().GetPatrolDetails(PatrolId);
            if (patrolEntity == null)
                return null;

            var details = new TFMDAL().GetPatrolDetails(patrolEntity.PatrolOriginalId);
            foreach (var item in details.Officers)
            {
                item.ImagePath = string.Format("{0}/{1}", System.Configuration.ConfigurationSettings.AppSettings["TFMImagesPath"], item.ImagePath);
            }
            return details;
            //var tfmClient = new tfmStaffService.srvc_StaffSoapClient();

            //var officers = tfmClient.GetAllStaffByTransporterID(patrolEntity.PatrolOriginalId);
            //if(officers != null && officers.Any())
            //{

            //}
            //var patrol = new PatrolFullDetailsDTO();
            //patrol.PatrolDetails.BasicAllocationHub = "Muroor";
            //patrol.PatrolDetails.PatrolId = PatrolId;
            //patrol.PatrolOfficers.OfficersName = string.Format("{0},{1}", "Salem Mahmoud", "Ahmed Karim");
            //patrol.PatrolOfficers.OfficersPhoneNumbers = "05874322";
            //patrol.PatrolOfficers.OfficersMilitaryNumbers = string.Format("{0},{1}", "6464543", "4445332");
            //patrol.PatrolPerformance.AverageResponseTime = "00:02:00";
            //patrol.PatrolPerformance.LastIncidentTime = "3 May 2015 14:02:00";
            //patrol.PatrolPerformance.LastResponseTime = "00:03:00";
            //patrol.PatrolPerformance.NumActiveHours = 8;
            //patrol.PatrolPerformance.NumberOfIncidentHandled = 10;
            //return patrol;
        }
        public string GetAssetLocation(int assetId, bool isArabic)
        {
            return new AssetsDAL().GetAssetLocation(assetId, isArabic);
        }

        public string GetAssetSerialNumber(long assetId)
        {
            return new AssetsDAL().GetAssetSerialNumber(assetId);
        }
        public int GetMaxNotificationsCount()
        {
            return 200;
        }
        public bool SaveSendControlToUsers(string xmlToSend, List<string> Usernames)
        {
            return new UserUserControlBL(null).Save(xmlToSend, Usernames);
        }
        // public List<UsersDTO>
        public List<MessageTypeSOPDTO> GetMessageTypeSOP(int MessageType)
        {
            SopDAL sopDAL = new SopDAL();

            return sopDAL.GetMessageTypeSOP(MessageType);
        }

        public List<MessageTypeSOPDTO> GetMessageTypeSOPs(int MessageType, int MessageId)
        {
            SopDAL sopDAL = new SopDAL();
            int? priorityId = null;
            long? notificationId = null;
            var message = new EventsDAL().GetEventById(MessageId);

            if (message != null)
            {
                priorityId = message.PriorityId;
                notificationId = message.NotificationId;
            }
            return sopDAL.GetMessageTypeSOP(MessageType, priorityId,notificationId);
        }

        public List<AssetsViewDTO> GetNearByTowersByLatLon(double longitude, double latitude)
        {
            AssetsDAL assetsDAL = new AssetsDAL();
            return assetsDAL.GetNearByTowersByLatLon(longitude, latitude, GetRadiusForNearByAssets() * 1000);
        }
        public List<AssetsViewDTO> GetNearByRadarsByLatLon(double longitude, double latitude)
        {
            AssetsDAL assetsDAL = new AssetsDAL();
            return assetsDAL.GetNearByRadarsByLatLon(longitude, latitude, GetRadiusForNearByAssets() * 1000);
        }
        public List<AssetsViewDTO> GetNearByCamerasByLatLon(double longitude, double latitude)
        {
            AssetsDAL assetsDAL = new AssetsDAL();
            return assetsDAL.GetNearByCamerasByLatLon(longitude, latitude, GetRadiusForNearByAssets() * 1000);
        }
        public List<TowerActionsDTO> GetAllTowerActions()
        {
            var sopDAL = new SopDAL();
            return sopDAL.GetAllTowersActions();
        }
        public double GetRadiusForNearByAssets()
        {
            return double.Parse(ConfigurationManager.AppSettings.Get("RadiusForNearByAssetsKM"));
        }
        public MessageTypeConvertOutput ConvertXML(string xml, int generalTypeId)
        {
            return new ConfigurationDAL().GetTransformOutput(generalTypeId, xml);
        }


        public List<AssetsDetailsViewDTO> GetAllTowerCameras(long TowerId)
        {
            AssetsDAL assetsDAL = new AssetsDAL();
            return assetsDAL.GetAllTowerCameras(TowerId);
        }


        public List<PatrolLastLocationDTO> GetNearByPatrolsByLatLon(double longitude, double latitude, int patrolsCount)
        {
            PatrolsDAL patrolsDAL = new PatrolsDAL();
            var patrols = patrolsDAL.GetNearByPatrolsByLatLon(longitude, latitude, GetRadiusForNearByAssets() * 1000, patrolsCount);
            return patrols;
            //return new CrsipServices().GetPatrolsETAs(latitude, longitude, patrols);
        }

        public VehicleLiveTrackingDTO GetVehicleDetailsByPlateNumber(string plateNumber)
        {
            VehicleLiveTrackingDAL vehicleLiveTrackingDAL = new VehicleLiveTrackingDAL();
            return vehicleLiveTrackingDAL.GetVehicleDetailsByPlateNumber(plateNumber);
        }

        public List<ViolationNotificationDTO> GetViolationsDetailsByAsset(string serialNumber)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationsHistoryListByAsset(serialNumber);
        }

        public int GetViolationsCountByAsset(DateTime? StartDateTime, DateTime? EndDateTime, string serialNumber)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationsCountByAsset(StartDateTime, EndDateTime, serialNumber);
        }

        public List<ViolationsGroupedByLocationsDTO> GetHistoricalViolationsListGroupedByLocation(DateTime? StartDateTime, DateTime? EndDateTime, int? ViolationTypeId)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetHistoricalViolationsListGroupedByLocation(StartDateTime, EndDateTime, ViolationTypeId);
        }

        public List<string> GetViolationImageURLsById(long ViolationNotificationId)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationImageURLsById(ViolationNotificationId);
        }

        public List<Byte[]> GetViolationImagesById(long ViolationNotificationId)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationImagesById(ViolationNotificationId);
        }

        public string GetViolationVideoURLById(long ViolationNotificationId)
        {
            ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();
            return violationNotificationDAL.GetViolationVideoURLById(ViolationNotificationId);
        }

        public List<CubeDTO> GetViolationPerMonthAndViolationType()
        {
            CubeDAL cubeDal = new CubeDAL(_analysisViolationConnectionString);
            return cubeDal.GetViolationPerMonthAndViolationType();
        }

        public List<CubeDTO> GetViolationPerVehicleTypeAndDayOfWeek()
        {
            CubeDAL cubeDal = new CubeDAL(_analysisViolationConnectionString);
            return cubeDal.GetViolationPerVehicleTypeAndDayOfWeek();
        }

        public bool ChangeNotificationStatus(long NotificationId, int NewStatus, int UserId)
        {
            return new NotificationDAL().ChangeNotificationStatus(NotificationId, NewStatus, UserId);
        }

        public List<ViolationNotificationDTO> GetLatestViolationNotification()
        {
            return new NotificationDAL().GetLatestViolationNotification(_oldPeriod);
        }

        public List<IncidentsDTO> GetLatestIncidents()
        {
            return new NotificationDAL().GetLatestIncidents(_oldPeriod);
        }

        public List<UserUserControlDTO> GetLatestEvents(int UserId)
        {
            return new NotificationDAL().GetLatestEvents(_oldPeriod, UserId);
        }

        public bool UpdateAssetValue(long AssetId, string NewValue)
        {
            return new AssetsDAL().UpdateAssetValue(AssetId, NewValue);
        }

        public bool SaveSOPNotificationLog(int sopStepId, long notificationId, int? sopCommandId, int userId, string previousValue, string currentValue)
        {
            return new NotificationDAL().SaveSOPNotificationLog(sopStepId, notificationId, sopCommandId, userId, previousValue, currentValue);
        }

        public List<BusinessRulesDTO> GetAllBusinessRules(bool IsAcivatedOnly)
        {
            var isNewRules = false;
            if (System.Configuration.ConfigurationSettings.AppSettings["IsNewDynamicRules"] != null)
                bool.TryParse(System.Configuration.ConfigurationSettings.AppSettings["IsNewDynamicRules"].ToString(), out isNewRules);
            var res = new List<BusinessRulesDTO>();
            if (isNewRules)
            {
                var newDynamic = new DynamicRulesDAL().GetAllRules(true);
                if (newDynamic != null && newDynamic.Any())
                {
                    foreach (var item in newDynamic)
                    {
                        var model = new BusinessRulesDTO
                        {
                            BusinessName = item.RuleName,
                            BusinessRuleId = (int)item.RuleId,
                            InsideCityOverSpeedId = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.Speed) ? item.ViolationDetails.FirstOrDefault(x => x.ViolationType == ViolationTypesEnum.Speed).SpeedOverKMsDetails.OverSpeedId : 0,
                            InsideCityOverSpeedQty = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.Speed) ? item.ViolationDetails.FirstOrDefault(x => x.ViolationType == ViolationTypesEnum.Speed).ViolationQty : 0,
                            InsideCityOverSpeedValue = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.Speed) ? item.ViolationDetails.FirstOrDefault(x => x.ViolationType == ViolationTypesEnum.Speed).SpeedOverKMsDetails.OverSpeedValue : 0,
                            IsDeleted = false,
                            IsOverSpeedInsideCity = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.Speed),
                            IsOverSpeedOutsideCity = false,
                            IsTrafficCross = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.RedLight),
                            OutsideCityOverSpeedId = 0,
                            OutsideCityOverSpeedQty = 0,
                            OutsideCityOverSpeedValue = 0,
                            RuleInterval = item.TimeIntervalMins * 60,
                            TrafficCrossQty = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.RedLight) ? item.ViolationDetails.FirstOrDefault(x => x.ViolationType == ViolationTypesEnum.RedLight).ViolationQty : 0,
                            TrafficCrossTimesId = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.RedLight) ? item.ViolationDetails.FirstOrDefault(x => x.ViolationType == ViolationTypesEnum.RedLight).TrafficCrossDetails.TrafficCrossId : 0,
                            TrafficCrossTimesValue = item.ViolationDetails != null && item.ViolationDetails.Any(x => x.ViolationType == ViolationTypesEnum.RedLight) ? item.ViolationDetails.FirstOrDefault(x => x.ViolationType == ViolationTypesEnum.RedLight).TrafficCrossDetails.TrafficCrossValue : 0
                        };
                        if (item.VehicleDetails != null && item.VehicleDetails.VehicleType != null)
                            model.VehicleTypeId = item.VehicleDetails.VehicleType.VehicleTypeID;
                        res.Add(model);
                    }
                }
            }
            else
            {
                res = new BusinessRuleDAL().GetAllBusinessRules(IsAcivatedOnly);
            }
            return res;
        }

        public bool SaveBusinessRule(BusinessRulesDTO businessRule)
        {
            return new BusinessRuleDAL().SaveBusinessRule(businessRule);
        }

        public BusinessRulesDTO GetBusinessRuleByID(int businessRuleId)
        {
            var allRules = GetAllBusinessRules(true);
            if (allRules != null && allRules.Any())
                return allRules.FirstOrDefault(x => x.BusinessRuleId == businessRuleId);
            else
                return new BusinessRuleDAL().GetBusinessRuleByID(businessRuleId);
        }

        public List<BusinessRulePriorityDTO> GetAllPriorities()
        {
            return new BusinessRuleDAL().GetAllPriorities();
        }

        public List<OverSpeedDTO> GetAllOverSpeed()
        {
            return new BusinessRuleDAL().GetAllOverSpeed();
        }

        public List<TrafficCrossDTO> GetAllTrafficCrossTimes()
        {
            return new BusinessRuleDAL().GetAllTrafficCrossTimes();
        }

        public List<VehicleTypeDTO> GetAllVehicleTypes()
        {
            return new BusinessRuleDAL().GetAllVehicleTypes();
        }

        public List<PatrolDTO> GetAssignedPatrols(long notificationId)
        {
            return new PatrolsDAL().GetAssignedPatrolsByNotificationId(notificationId);
        }

        #region KPI Region

        [AspNetCacheProfile("CacheForTargets")]
        [WebGet()]
        public List<KPITargetDTO> GetAllTargets()
        {
            var res = new List<KPITargetDTO>();
            res = new KPITargetsDAL().GetAllTargets();
            return res;
        }



        #region Violation KPI
        public List<KpiDTO> GetViolationKPIs()
        {
            var res = new List<KpiDTO>();
            res.AddRange(GetViolationKPIPerViolationType());
            res.AddRange(GetViolationKPIPerVehicleType());
            return res;
        }

        public List<KpiDTO> GetViolationKPIPerVehicleType()
        {
            var res = new List<KpiDTO>();
            res.Add(GetCarViolationKPI());
            res.Add(GetTruckViolationKPI());
            return res;
        }

        private KpiDTO GetCarViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVEHICLETYPE_CAR",
                CurrentValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year, "Car"),
                PreviousValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year - 1, "Car"),
                LabelValueArabic = "مخالفات السيارات",
                LabelValueEnglish = "Car Violation",
                Percentage = GetTargetPercentage("TARGET_PERVEHICLETYPE_CAR")
            };
            return res;
        }

        private KpiDTO GetTruckViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVEHICLETYPE_TRUCK",
                CurrentValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year, "Truck"),
                PreviousValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year - 1, "Truck"),
                LabelValueArabic = "مخالفات الشاحنات",
                LabelValueEnglish = "Truck Violation",
                Percentage = GetTargetPercentage("TARGET_PERVEHICLETYPE_TRUCK")
            };
            return res;
        }


        public List<KpiDTO> GetViolationKPIPerViolationType()
        {
            var res = new List<KpiDTO>();
            res.Add(GetSpeedViolationKPI());
            res.Add(GetRedLightViolationKPI());
            return res;
        }

        public bool UpdateKPITarget(string keyname, double newValue, int userId)
        {
            return new KPITargetsDAL().UpdateTarget(keyname, newValue, userId);
        }

        private double GetTargetPercentage(string keyname)
        {
            double res = 0.00;
            var allTarget = GetAllTargets();
            if (allTarget != null)
            {
                var target = allTarget.FirstOrDefault(x => x.TargetName == keyname);
                if (target != null)
                {
                    res = target.TargetValue;
                }
            }
            return res;
        }

        public List<KpiDTO> GetAllTargetsList()
        {
            var list = new List<KpiDTO>();
            list.AddRange(GetViolationKPIs());
            list.AddRange(GetAccidentKPIs());
            return list;
        }

        public KpiDTO GetSpeedViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVIOLATIONTYPE_SPEED",
                CurrentValue = GetViolationCountPerYearPerType(DateTime.Now.Year, "SPEED"),
                PreviousValue = GetViolationCountPerYearPerType(DateTime.Now.Year - 1, "SPEED"),
                LabelValueArabic = "أعداد مخالفات السرعة",
                LabelValueEnglish = "Speed Violation",
                Percentage = GetTargetPercentage("TARGET_PERVIOLATIONTYPE_SPEED")
            };
            return res;
        }

        public KpiDTO GetRedLightViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVIOLATIONTYPE_RED",
                CurrentValue = GetViolationCountPerYearPerType(DateTime.Now.Year, "RED"),
                PreviousValue = GetViolationCountPerYearPerType(DateTime.Now.Year - 1, "RED"),
                LabelValueArabic = "أعداد مخالفات الاشارة الحمراء",
                LabelValueEnglish = "Red Light Violation",
                Percentage = GetTargetPercentage("TARGET_PERVIOLATIONTYPE_RED")
            };
            return res;
        }

        public double GetViolationCountPerYearPerType(int year, string type)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationCountPerYearPerType(year, type);
        }

        public double GetViolationCountPerYearPerVehicleType(int year, string vehicleType)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationCountPerYearAndVehicleType(year, vehicleType);
        }
        # endregion

        # region Accident KPI
        public List<KpiDTO> GetAccidentKPIs()
        {
            var res = new List<KpiDTO>();
            res.Add(GetFatalityKPI());
            res.AddRange(GetAccidentKPIPerAccidentTypes());
            res.Add(GetVehicleProblemAccidentsKPI());
            //res.AddRange(GetAccidentKPIPerRegions());
            res.Add(GetFatalInjuriesKPI());
            res.Add(GetRedLightAccidentsKPI());
            res.Add(GetRedLightFatalityKPI());
            return res;
        }

        public KpiDTO GetVehicleProblemAccidentsKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREASON_VEHICLEPROBLEM",
                CurrentValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year, "VehicleProblem"),
                PreviousValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year - 1, "VehicleProblem"),
                LabelValueArabic = "حوادث بسبب اعطال المركبة",
                LabelValueEnglish = "Accidents because of vehicles problems",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREASON_VEHICLEPROBLEM")
            };
            return res;
        }

        public KpiDTO GetRedLightAccidentsKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREASON_REDLIGHT",
                CurrentValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year, "RedLight"),
                PreviousValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year - 1, "RedLight"),
                LabelValueArabic = "حوادث بسبب تخطي الاشارة الحمراء",
                LabelValueEnglish = "Accidents because of violating red light",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREASON_REDLIGHT")
            };
            return res;
        }

        public List<KpiDTO> GetAccidentKPIPerRegions()
        {
            var res = new List<KpiDTO>();
            res.Add(GetFatalityKPIInAbuDhabi());
            res.Add(GetFatalityKPIInWestren());
            res.Add(GetFatalityKPIInAlAin());
            return res;
        }

        public KpiDTO GetFatalityKPIInAbuDhabi()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREGION_ABUDHABI",
                CurrentValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year, "Abu Dhabi Island"),
                PreviousValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year - 1, "Abu Dhabi Island"),
                LabelValueArabic = "الوفيات في جزيرة ابو ظبي",
                LabelValueEnglish = "Fatalities on Abu Dhabi island",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREGION_ABUDHABI")
            };
            return res;
        }

        public KpiDTO GetFatalityKPIInWestren()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREGION_WESTERN",
                CurrentValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year, "Western"),
                PreviousValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year - 1, "Western"),
                LabelValueArabic = "الوفيات في القطاع الغربي",
                LabelValueEnglish = "Fatalities on Western Region",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREGION_WESTERN")
            };
            return res;
        }

        public KpiDTO GetFatalityKPIInAlAin()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREGION_ALAIN",
                CurrentValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year, "AlAin"),
                PreviousValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year - 1, "AlAin"),
                LabelValueArabic = "الوفيات في العين",
                LabelValueEnglish = "Fatalities on Al-Ain",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREGION_ALAIN")
            };
            return res;
        }

        public List<KpiDTO> GetAccidentKPIPerAccidentTypes()
        {
            var res = new List<KpiDTO>();
            res.Add(GetInjuriesAccidentKPI());
            res.Add(GetFatalAccidentKPI());
            res.Add(GetNormalAccidentKPI());
            return res;
        }

        public KpiDTO GetInjuriesAccidentKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERTYPE_INJURIES",
                CurrentValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year, "InjAcc"),
                PreviousValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year - 1, "InjAcc"),
                LabelValueArabic = "حوادث اصابات",
                LabelValueEnglish = "Injuries accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_INJURIES")
            };
            return res;

        }

        public KpiDTO GetFatalAccidentKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERTYPE_FATAL",
                CurrentValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year, "FatAcc"),
                PreviousValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year - 1, "FatAcc"),
                LabelValueArabic = " نوع حوادث الوفيات",
                LabelValueEnglish = "Fatal accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_FATAL")
            };
            return res;

        }

        public KpiDTO GetNormalAccidentKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERTYPE_NORMAL",
                CurrentValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year, "NorAcc"),
                PreviousValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year - 1, "NorAcc"),
                LabelValueArabic = "حوادث سير بسيطة",
                LabelValueEnglish = "Normal accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_NORMAL")
            };
            return res;

        }

        public KpiDTO GetFatalityKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTS_FATALITYACCIDENTS",
                CurrentValue = GetFatalityCountPerYear(DateTime.Now.Year),
                PreviousValue = GetFatalityCountPerYear(DateTime.Now.Year - 1),
                LabelValueArabic = "حوادث الوفيات",
                LabelValueEnglish = "Fatality accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYACCIDENTS")
            };
            return res;
        }

        public KpiDTO GetFatalInjuriesKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTS_FATALITYINJURIES",
                CurrentValue = GetFatalInjuiresCountPerYear(DateTime.Now.Year),
                PreviousValue = GetFatalInjuiresCountPerYear(DateTime.Now.Year - 1),
                LabelValueArabic = "الاصابات الجسيمة",
                LabelValueEnglish = "Fatal Injuries",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYINJURIES")
            };
            return res;
        }

        public KpiDTO GetRedLightFatalityKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTS_FATALITYREDLIGHTACCIDENTS",
                CurrentValue = GetFatalityCountPerYearAndReason(DateTime.Now.Year, "RedLight"),
                PreviousValue = GetFatalityCountPerYearAndReason(DateTime.Now.Year - 1, "RedLight"),
                LabelValueArabic = "وفيات حوادث تخطي الاشارة الحمراء",
                LabelValueEnglish = "Fatalities of red light violation",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYREDLIGHTACCIDENTS")
            };
            return res;
        }

        private double GetFatalityCountPerYear(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalityCountPerYear(year);
        }

        private double GetFatalInjuiresCountPerYear(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalInjuriesCountPerYear(year);
        }

        private double GetAccidentCountPerYearAndAccidentType(int year, string accidentType)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetAccidentCountPerYearAndAccidentType(year, accidentType);
        }

        private double GetAccidentCountPerYearAndAccidentReason(int year, string accidentReason)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetAccidentCountPerYearAndAccidentReason(year, accidentReason);
        }

        private double GetFatalityCountPerYearAndRegion(int year, string region)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalityCountPerYearAndRegion(year, region);
        }

        private double GetFatalityCountPerYearAndReason(int year, string reason)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalityCountPerYearAndReason(year, reason);
        }

        # endregion
        #endregion

        #region Trend Analysis

        public List<CubeDTO> GetViolationTypeTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetViolationTypeTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetViolationTypeTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetViolationTypeTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendQuarterOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsTrendQuarterOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendQuarterOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendQuarterOfYear(startYear, endYear);
        }

        #endregion

        #region Region Wise Normal

        public List<CubeDTO> GetViolationsRegionWiseYearly()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseYearly();
        }

        public List<CubeDTO> GetViolationsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseDaily(WeekStartDate, WeekEndDate);
        }

        public List<CubeDTO> GetViolationsRegionWiseMonthly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetViolationsRegionWiseWeekly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseWeekly(year);
        }

        public List<CubeDTO> GetTruckViolationRegionWiseYearly()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationRegionWiseYearly();
        }

        public List<CubeDTO> GetTruckViolationRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationRegionWiseDaily(WeekStartDate, WeekEndDate);
        }

        public List<CubeDTO> GetTruckViolationRegionWiseMonthly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetTruckViolationRegionWiseWeekly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationRegionWiseWeekly(year);
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseYearly()
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseYearly();
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseDaily(WeekStartDate, WeekEndDate);
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseMonthly(int year)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseWeekly(int year)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseWeekly(year);
        }

        public List<CubeDTO> GetIncidentsRegionWiseYearly()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseYearly();
        }

        public List<CubeDTO> GetIncidentsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseDaily(WeekStartDate, WeekEndDate);
        }

        public List<CubeDTO> GetIncidentsRegionWiseMonthly(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetIncidentsRegionWiseWeekly(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseWeekly(year);
        }

        #endregion

        #region Region Wise Comparison


        public List<CubeDTO> GetTruckViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonDaily(firstDay, secondDay);
        }

        #endregion

        #region Statistical Normal

        public List<CubeDTO> GetTruckViolationsStatisticalYearly()
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStatisticalYearly();
        }

        public List<CubeDTO> GetTruckViolationsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetTruckViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetTruckViolationsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStatisticalDaily(weekStartDate, weekEndDate);

        }

        public List<CubeDTO> GetViolationsStatisticalYearly()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStatisticalYearly();
        }

        public List<CubeDTO> GetViolationsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetViolationsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStatisticalDaily(weekStartDate, weekEndDate);

        }

        public List<CubeDTO> GetIncidentsStatisticalYearly()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStatisticalYearly();
        }

        public List<CubeDTO> GetIncidentsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetIncidentsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetIncidentsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStatisticalDaily(weekStartDate, weekEndDate);

        }

        public List<CubeDTO> GetDangerousViolationsStatisticalYearly()
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStatisticalYearly();
        }

        public List<CubeDTO> GetDangerousViolationsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetDangerousViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetDangerousViolationsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStatisticalDaily(weekStartDate, weekEndDate);
        }

        #endregion

        #region Statistical Comparison

        public List<CubeDTO> GetTruckViolationsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetTruckViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetTruckViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetViolationsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetIncidentsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetIncidentsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetIncidentsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonDaily(firstDay, secondDay);
        }

        #endregion

        #region Target Analysis

        public List<CubeDTO> GetDangerousViolationTarget()
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTarget();
        }
        public List<CubeDTO> GetIncidentsTarget()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsTarget();
        }
        public List<CubeDTO> GetViolationsTarget()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsTarget();
        }

        #endregion

        #region Main Dashboard

        public List<CubeDetailsDTO> GetMainDashboard(string language)
        {
            List<CubeDetailsDTO> list = new List<CubeDetailsDTO>();

            list.Add(new CubeDAL(_analysisViolationConnectionString).GetTotalViolationsForMainDashboard(language));
            list.Add(new CubeDAL(_analysisIncidentConnectionString).GetTotalFatalitiesForMainDashboard(language));
            list.Add(new CubeDAL(_analysisIncidentConnectionString).GetTotalSlightInjuriesForMainDashboard(language));
            list.Add(new CubeDAL(_analysisIncidentConnectionString).GetTotalSevereInjuriesForMainDashboard(language));

            return list;
        }

        #endregion

        #region Fatality Target 2030

        public List<CubeDTO> GetFatalitiyTarget()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalitiyTarget();
        }

        #endregion

        #region ViolationCount

        public List<ViolationsCountForMapDTO> GetViolationsCountPerAsset(PeriodType type)
        {
            List<ViolationsCountForMapDTO> list = new List<ViolationsCountForMapDTO>();
            List<CubeDTO> cubeList = new CubeDAL(_analysisViolationConnectionString).GetViolationsCountForMap(type);

            List<AssetsViewDTO> assetList = GetAllAssets();

            if (assetList == null)
            {
                return null;
            }

            if (cubeList != null && cubeList.Count > 0)
            {
                foreach (CubeDTO item in cubeList)
                {
                    if (item.Details == null || item.Details.Count == 0)
                        continue;

                    ViolationsCountForMapDTO outputItem = new ViolationsCountForMapDTO();

                    outputItem.DateElement = item.LegendName;

                    outputItem.AssetDetails = new List<ViolationsGroupedByLocationsDTO>();

                    foreach (CubeDetailsDTO cubeDetail in item.Details)
                    {
                        AssetsViewDTO asset = assetList.Where(a => a.SerialNo.ToLower() == cubeDetail.Key.ToLower()).FirstOrDefault();

                        if (asset == null)
                            continue;

                        ViolationsGroupedByLocationsDTO outputDetail = new ViolationsGroupedByLocationsDTO();

                        outputDetail.ViolationsCount = (int)cubeDetail.Value;
                        outputDetail.LocationCode = cubeDetail.Key;
                        outputDetail.Latitude = asset.Latitude.Value;
                        outputDetail.Longitude = asset.Longitude.Value;
                        outputDetail.Altitude = 1;

                        outputItem.AssetDetails.Add(outputDetail);
                    }

                    list.Add(outputItem);
                }
            }

            return list;
        }

        #endregion

        #region Available Years

        public List<int> GetViolationsAvailableYears()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsAvailableYears();
        }

        public List<int> GetIncidentsAvailableYears()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsAvailableYears();
        }

        public DateTime GetIncidentFirstDate()
        {
            return new IncidentsDAL().GetIncidentFirstDate();
        }

        #endregion

        #region SocialMedia

        public bool PublishToFacebook(string post, byte[] imageBytes)
        {
            SocialMediaServiceOfflineReference.SocialMediaServiceOfflineClient client = new SocialMediaServiceOfflineReference.SocialMediaServiceOfflineClient();

            return client.PublishToFacebook(post, imageBytes);
        }

        public bool PublishToTwitter(string post, byte[] imageBytes)
        {
            SocialMediaServiceOfflineReference.SocialMediaServiceOfflineClient client = new SocialMediaServiceOfflineReference.SocialMediaServiceOfflineClient();

            return client.PublishToTwitter(post, imageBytes);
        }

        #endregion

        #region Users and Roles

        public List<UsersDTO> GetAllUsersList()
        {
            return new UsersDAL().GetUsersList();
        }

        public bool AddUser(string userPassword, UsersDTO user)
        {
            return new UsersDAL().AddUser(userPassword, user);
        }

        public bool UpdateUser(UsersDTO user)
        {
            return new UsersDAL().UpdateUser(user);
        }

        public bool DeactivateUser(int userId, bool isActive)
        {
            return new UsersDAL().DeactivateUser(userId, isActive);
        }

        public bool DeleteUser(UsersDTO user)
        {
            return new UsersDAL().DeleteUser(user);
        }

        public bool AddRank(RanksDTO Rank)
        {
            return new UsersDAL().AddRank(Rank);
        }

        public bool UpdateRank(RanksDTO Rank)
        {
            return new UsersDAL().UpdateRank(Rank);
        }

        public bool DeleteRank(RanksDTO Rank)
        {
            return new UsersDAL().DeleteRank(Rank);
        }

        public bool AddFeature(FeaturesDTO Feature)
        {
            return new UsersDAL().AddFeature(Feature);
        }

        public bool UpdateFeature(FeaturesDTO Feature)
        {
            return new UsersDAL().UpdateFeature(Feature);
        }

        public bool DeleteFeature(FeaturesDTO Feature)
        {
            return new UsersDAL().DeleteFeature(Feature);
        }

        public List<RanksDTO> GetRanksList()
        {
            return new UsersDAL().GetRanksList();
        }

        public List<FeaturesDTO> GetFeaturesList()
        {
            return new UsersDAL().GetFeaturesList();
        }

        public bool AddUserRole(UserRolesDTO role)
        {
            return new UsersDAL().AddUserRole(role);
        }

        public bool UpdateUserRole(UserRolesDTO role)
        {
            return new UsersDAL().UpdateUserRole(role);
        }

        public bool DeleteUserRole(UserRolesDTO role)
        {
            return new UsersDAL().DeleteUserRole(role);
        }

        public List<UserRolesDTO> GetUserRolesList()
        {
            return new UsersDAL().GetUserRolesList();
        }

        public UsersDTO GetUserById(int userId)
        {
            return new UsersDAL().GetUserById(userId);
        }

        public UsersDTO Login(string Username, string Password)
        {
            return new UsersDAL().Login(Username, Password);
        }

        public int GetSupervisorId()
        {
            return 1;
        }

        public string SendNotification(List<string> deviceRegIds, string message)
        {

            // Configuration
            var config = new GcmConfiguration("GCM-SENDER-ID", "AIzaSyBEMfVKUw1yiphlRJkGh4dPOYvRuY1jVeY", null);

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            string result = null;
            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        result = ("GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            result = ("GCM Notification Failed: ID={succeededNotification.MessageId}");
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e = failedKvp.Value;

                            result = ("GCM Notification Failed: ID={n.MessageId}, Desc={e.Description}");
                        }

                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

                        result = ("Device RegistrationId Expired: {oldId}");

                        if (!string.IsNullOrEmpty(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
                            result = ("Device RegistrationId Changed To: {newId}");
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                        result = ("GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                    }
                    else
                    {
                        result = ("GCM Notification Failed for some unknown reason");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) =>
            {
                result = ("GCM Notification Sent!");
            };

            // Start the broker
            gcmBroker.Start();

            foreach (var regId in deviceRegIds)
            {
                // Queue a notification to send
                gcmBroker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = new List<string> {
            regId
        },
                    Data = JObject.Parse("{ \"message\" : \" " + message + "\" }")
                });
            }

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();

            return result;

            StringBuilder builder = new StringBuilder(message);


        }
        private static string GetPostStringFrom(Dictionary<string, string> postFieldNameValue)
        {
            // return Newtonsoft.Json.JsonConvert.SerializeObject(postFieldNameValue);
            return "\"data\": {\"Message\": \"" + postFieldNameValue["data"] + "\"},\"registration_ids\":[\"" + postFieldNameValue["registration_ids"] + "\"]}";
        }
        public List<PublicUserDTO> GetActivePublicUsers()
        {
            return new UsersDAL().GetActivePublicUsers();
        }

        #endregion

        #region Supervisor Notification

        public bool SaveSupervisorNotification(SupervisorNotificationDTO supervisorNotification)
        {
            if (supervisorNotification != null)
            {
                if (supervisorNotification.DangerousViolatorDetails != null && supervisorNotification.DangerousViolatorDetails.MediaURL != null)
                {
                    var folderPath = string.Format(@"{0}\Files\ManualReport\{1}", Utility.GetExecutionPath(), supervisorNotification.DangerousViolatorDetails.PlateNumber);
                    var fileName = string.Format("{0}.{1}", DateTime.Now.ToString("ddMMyyyy-hhmmss"), supervisorNotification.DangerousViolatorDetails.MediaFileFormat);
                    var filePath = string.Format(@"{0}\{1}", folderPath, fileName);

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileBytes = Convert.FromBase64String(supervisorNotification.DangerousViolatorDetails.MediaURL);

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    File.WriteAllBytes(filePath, fileBytes);

                    supervisorNotification.DangerousViolatorDetails.MediaURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace("ServiceLayer.svc", "").Replace("mainsoap", "") + string.Format("/Files/ManualReport/{0}/{1}", supervisorNotification.DangerousViolatorDetails.PlateNumber, fileName);
                }

                return new SupervisorNotificationDAL().SaveSupervisorNotification(supervisorNotification);
            }

            return false;
        }

        public SupervisorNotificationDTO GetSupervisorNotificationById(int supervisorNotificationId)
        {
            return new SupervisorNotificationDAL().GetSupervisorNotificationById(supervisorNotificationId);
        }

        public List<SupervisorNotificationDTO> GetSupervisorNotificationsByUserId(int userId)
        {
            return new SupervisorNotificationDAL().GetSupervisorNotificationsByUserId(userId);
        }

        public bool SetSupervisorNotificationNoticed(long notificationId, bool isNoticed = true)
        {
            return new SupervisorNotificationDAL().SetSupervisorNotificationNoticed(notificationId, isNoticed);
        }

        #endregion

        #region Dangerous Vehicle Search
        public List<CorrelationMessagesLogDTO> GetCorrelationLogByVehicleDetails(string plateNumber, string plateColor, string plateSource, string plateKind)
        {
            return new CorrelationMessagesLogDAL().GetCorrelationLogByVehicleDetails(plateNumber, plateColor, plateSource, plateKind);
        }

        public List<CorrelationMessagesLogDTO> GetCorelationsLogByBusinessRule(int businessRuleId)
        {
            return new CorrelationMessagesLogDAL().GetCorelationsLogByBusinessRule(businessRuleId);
        }

        public bool IsDangerousVehicleActive(string plateNumber, string plateColor, string plateSource, string plateKind)
        {
            return new CorrelationMessagesLogDAL().IsDangerousVehicleActive(plateNumber, plateColor, plateSource, plateKind);
        }

        public void DeactivateDangeriousVehicle(string plateNumber, string plateColor, string plateSource, string plateKind)
        {

            new CorrelationMessagesLogDAL().DeactivateDangeriousVehicle(plateNumber, plateColor, plateSource, plateKind);
        }

        public void DeactivateDangerousViolatorList(List<PlateDetailsDTO> vehicles)
        {
            new CorrelationMessagesLogDAL().DeactivateDangerousViolatorList(vehicles);
        }

        #endregion

        public List<VehicleLiveTrackingDTO> GetAllWantedCarLiveTrack(string plateNumber, int maxHours)
        {
            return new VehicleLiveTrackingDAL().GetAllWantedCarPathInPeriod(plateNumber, maxHours);
        }
    }
}