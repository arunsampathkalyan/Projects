using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
namespace STC.Projects.ClassLibrary.DAL
{
    public class DynamicRulesDAL
    {
        public STCOperationalDataContext _operationDB = new STCOperationalDataContext();

        public bool ActivateDeactivateRule(long ruleId, bool IsDeleted)
        {
            var ruleEntity = _operationDB.NewCorrelationRules.Where(x => x.BusinessRuleId == ruleId).FirstOrDefault();
            if (ruleEntity == null)
                return false;
            ruleEntity.IsDeleted = IsDeleted;
            return _operationDB.SaveChanges() > 0;
        }
        public bool SaveRule(DynamicRulesDTO rule)
        {
            var allRules = GetAllRules(true);
            if (allRules != null && allRules.Any(x => x.RuleName.ToLower().Trim() == rule.RuleName.ToLower().Trim()))
                return false;
            var ruleEntity = new NewCorrelationRule
            {
                CreatedBy = rule.CreatedById,
                DateCreated = DateTime.Now,
                IntervalPeriod = rule.TimeIntervalMins,
                ModifiedBy = rule.ModifiedBy,
                RuleName = rule.RuleName,
                RulePriority = rule.Priority != null ? rule.Priority.PriorityID : 1
            };
            if (rule.DriverDetails != null)
            {
                ruleEntity.CorrelationDriverDetail = new CorrelationDriverDetail
                {
                    AgeFrom = rule.DriverDetails.MinAge ?? 0,
                    AgeTo = rule.DriverDetails.MaxAge ?? 180,
                };
            }
            if (rule.ViolationDetails != null && rule.ViolationDetails.Any())
            {
                ruleEntity.CorrelationViolationDetails = new List<CorrelationViolationDetail>();
                foreach (var item in rule.ViolationDetails)
                {
                    var model = new CorrelationViolationDetail
                    {
                        IsDeleted = false,
                        ViolationTypeId = (int)item.ViolationType,
                        ViolationQty = item.ViolationQty
                    };
                    if (item.SpeedOverKMsDetails != null && item.ViolationType == ViolationTypesEnum.Speed)
                    {
                        model.OverSpeedDetailsId = item.SpeedOverKMsDetails.OverSpeedId;
                    };
                    if (item.TrafficCrossDetails != null && item.ViolationType == ViolationTypesEnum.RedLight)
                    {
                        model.TrraficCrossTimeDetailsId = item.TrafficCrossDetails.TrafficCrossId;
                    };
                    ruleEntity.CorrelationViolationDetails.Add(model);
                }
            }
            if (rule.LocationsDetails != null)
            {
                if (rule.LocationsDetails.RegionTypes == (int)RegionType.NORMAL)
                {
                    ruleEntity.LocationDetailsId = rule.LocationsDetails.RegionId;
                }
                if (rule.LocationsDetails.RegionTypes == (int)RegionType.CUSTEM)
                {
                    var region = new Region
                    {
                        IsDeleted = false,
                        RegionName = rule.LocationsDetails.RegionName,
                        RegionTypeId = (int)rule.LocationsDetails.RegionTypes,
                    };
                    var points = "";
                    foreach (var item in rule.LocationsDetails.RegionPoints)
                    {
                        points += string.Format("{0}&{1};", item.Latitude, item.Longitude);
                    }
                    region.RegionPoints = points.TrimEnd(';');
                    ruleEntity.Region = region;
                }
            }
            if (rule.VehicleDetails != null && (rule.VehicleDetails.VehicleBrand != null || rule.VehicleDetails.VehicleYear != null || rule.VehicleDetails.VehicleType != null))
            {
                ruleEntity.VehicleDetail = new VehicleDetail();
                if (rule.VehicleDetails.VehicleBrand != null)
                {
                    ruleEntity.VehicleDetail.VehicleBrandUTSId = rule.VehicleDetails.VehicleBrand.VehicleBrandId;
                }
                if (rule.VehicleDetails.VehicleYear != null)
                    ruleEntity.VehicleDetail.VehicleModelYear = rule.VehicleDetails.VehicleYear;
                if (rule.VehicleDetails.VehicleType != null)
                    ruleEntity.VehicleDetail.VehicleTypeId = rule.VehicleDetails.VehicleType.VehicleTypeID;

            }

            if (rule.TimeDetails != null)
            {
                ruleEntity.TimeDetail = new TimeDetail()
                {
                    FromDate = rule.TimeDetails.FromDate,
                    ToDate = rule.TimeDetails.ToDate,
                    FromTime = rule.TimeDetails.FromTime,
                    ToTime = rule.TimeDetails.ToTime,
                    TimeType = (int)rule.TimeDetails.TimeType,
                };
                if (rule.TimeDetails.WeekDays != null && rule.TimeDetails.WeekDays.Any())
                {
                    var weekStr = "";
                    foreach (var item in rule.TimeDetails.WeekDays)
                    {
                        weekStr += string.Format("{0},", item);
                    }
                    ruleEntity.TimeDetail.WeekDays = weekStr.TrimEnd(',');
                }
                //ruleEntity.t = new TimeDetail();
            }
            _operationDB.NewCorrelationRules.Add(ruleEntity);
            return _operationDB.SaveChanges() > 0;
        }

        public DynamicRulesDTO GetRuleById(int ruleId)
        {
            var res = new DynamicRulesDTO();
            var entity = _operationDB.NewCorrelationRules.Where(x => x.IsDeleted == false && x.BusinessRuleId == ruleId).FirstOrDefault();
            if (entity != null)
                res = PrepareModel(entity);
            return res;
        }

        private DynamicRulesDTO PrepareModel(NewCorrelationRule item)
        {
            var model = new DynamicRulesDTO()
            {
                CreatedById = item.CreatedBy,
                IsDeleted = item.IsDeleted,
                ModifiedBy = item.ModifiedBy,
                RuleId = item.BusinessRuleId,
                RuleName = item.RuleName,
                TimeIntervalMins = item.IntervalPeriod,
                NumberOfOccerance = item.CorrelationMessagesLogs.Count()
            };
            if (item.RulePriority > 0)
            {
                model.Priority = new BusinessRulePriorityDTO()
                {
                    PriorityID = item.RulePriority
                };
            }

            if (item.CorrelationDriverDetail != null)
            {
                model.DriverDetails = new BusinessDriverDetailsDTO
                {
                    MaxAge = item.CorrelationDriverDetail.AgeTo,
                    MinAge = item.CorrelationDriverDetail.AgeFrom
                };
            }
            if (item.CorrelationViolationDetails != null && item.CorrelationViolationDetails.Any())
            {
                model.ViolationDetails = new List<BusinessRuleViolationsDetailsDTO>();
                foreach (var viol in item.CorrelationViolationDetails)
                {
                    var violModel = new BusinessRuleViolationsDetailsDTO();
                    violModel.ViolationQty = viol.ViolationQty;
                    if (viol.OverSpeed != null)
                    {
                        violModel.ViolationType = ViolationTypesEnum.Speed;
                        violModel.SpeedOverKMsDetails = new OverSpeedDTO
                        {
                            IsDeleted = viol.OverSpeed.IsDeleted,
                            OverSpeedId = viol.OverSpeed.OverSpeedId,
                            OverSpeedValue = viol.OverSpeed.OverSpeedValue
                        };
                    }
                    if (viol.TrafficCrossTime != null)
                    {
                        violModel.ViolationType = ViolationTypesEnum.RedLight;
                        violModel.TrafficCrossDetails = new TrafficCrossDTO
                        {
                            IsDeleted = viol.TrafficCrossTime.IsDeleted,
                            TrafficCrossId = viol.TrafficCrossTime.TrafficCrossTimeId,
                            TrafficCrossValue = viol.TrafficCrossTime.TrafficCrossTimeValue
                        };
                    }
                    model.ViolationDetails.Add(violModel);
                }
            }
            if (item.Region != null)
            {
                model.LocationsDetails = GetRegionById(item.Region.RegionId);
            }
            if (item.TimeDetail != null)
            {
                model.TimeDetails = new TimeDetailsDTO
                {
                    FromDate = item.TimeDetail.FromDate,
                    FromTime = item.TimeDetail.FromTime,
                    TimeDetailsId = item.TimeDetail.TimeDetailsId,
                    TimeType = item.TimeDetail.TimeType,
                    ToDate = item.TimeDetail.ToDate,
                    ToTime = item.TimeDetail.ToTime
                };
                if (item.TimeDetail.TimeType == (int)ScheduleType.WEEKDAYS && item.TimeDetail.WeekDays != "")
                {
                    try
                    {
                        model.TimeDetails.WeekDays = new List<int>();

                        if (item.TimeDetail.WeekDays != null)
                        {
                            var allDays = item.TimeDetail.WeekDays.Split(',');
                            if (allDays != null && allDays.Any())
                            {
                                foreach (var day in allDays)
                                {
                                    model.TimeDetails.WeekDays.Add(int.Parse(day));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            if (item.VehicleDetail != null)
            {
                model.VehicleDetails = new BusinessRuleVehicleDetailsDTO()
                {
                    VehicleYear = item.VehicleDetail.VehicleModelYear
                };
                if (item.VehicleDetail.VehicleBrandUTSId.HasValue)
                {
                    model.VehicleDetails.VehicleBrand = new VehicleBrandDTO
                    {
                        VehicleBrandId = item.VehicleDetail.VehicleBrandUTSId.Value
                    };
                }
                if (item.VehicleDetail.VehicleTypeId.HasValue)
                {
                    model.VehicleDetails.VehicleType = new VehicleTypeDTO
                    {
                        VehicleTypeID = item.VehicleDetail.VehicleTypeId.Value
                    };
                }
            }
            return model;
        }
        public List<DynamicRulesDTO> GetAllRules(bool isActiveOnly = false)
        {
            var res = new List<DynamicRulesDTO>();
            var entities = _operationDB.NewCorrelationRules.Where(x => x.IsDeleted == false || !isActiveOnly).ToList();

            if (entities == null)
                return new List<DynamicRulesDTO>();

            foreach (var item in entities)
            {
                var model = PrepareModel(item);
                res.Add(model);
            }
            return res.OrderByDescending(r => r.NumberOfOccerance).ToList();
        }

        public BusinessRuleRegionDTO GetRegionById(int regionId)
        {
            var entity = _operationDB.Regions.FirstOrDefault(x => x.IsDeleted == false && x.RegionId == regionId);
            if (entity != null)
            {
                var model = new BusinessRuleRegionDTO()
                {
                    RegionId = entity.RegionId,
                    RegionName = entity.RegionName,
                    RegionTypes = (int)RegionType.NORMAL
                };
                if (entity.RegionPoints != null && entity.RegionPoints != "")
                {
                    var points = new List<MapPointDTO>();
                    var allPoints = entity.RegionPoints.Split(';');
                    if (allPoints != null && allPoints.Any())
                    {
                        foreach (var point in allPoints)
                        {
                            var latLonPair = point.Split('&');
                            if (latLonPair != null && latLonPair.Count() == 2)
                            {
                                var mapPoint = new MapPointDTO
                                {
                                    Latitude = double.Parse(latLonPair[0]),
                                    Longitude = double.Parse(latLonPair[1])
                                };
                                points.Add(mapPoint);
                            }
                        }
                    }
                    model.RegionPoints = points;
                }
                return model;
            }
            return new BusinessRuleRegionDTO();
        }

        public List<BusinessRuleRegionDTO> GetAllRegions()
        {
            var res = new List<BusinessRuleRegionDTO>();
            var entities = _operationDB.Regions.Where(x => x.IsDeleted == false && x.RegionTypeId == (int)RegionType.NORMAL).ToList();
            if (entities != null && entities.Any())
            {
                foreach (var item in entities)
                {
                    var model = new BusinessRuleRegionDTO()
                    {
                        RegionId = item.RegionId,
                        RegionName = item.RegionName,
                        RegionTypes = (int)RegionType.NORMAL
                    };
                    if (item.RegionPoints != null && item.RegionPoints != "")
                    {
                        var points = new List<MapPointDTO>();
                        var allPoints = item.RegionPoints.Split(';');
                        if (allPoints != null && allPoints.Any())
                        {
                            foreach (var point in allPoints)
                            {
                                var latLonPair = point.Split('&');
                                if (latLonPair != null && latLonPair.Count() == 2)
                                {
                                    var mapPoint = new MapPointDTO
                                    {
                                        Latitude = double.Parse(latLonPair[0]),
                                        Longitude = double.Parse(latLonPair[1])
                                    };
                                    points.Add(mapPoint);
                                }
                            }
                        }
                        model.RegionPoints = points;
                    }
                    res.Add(model);
                }
            }
            return res;
        }

        public DynamicRulesDTO ValidateBusinessRuleName(string ruleName)
        {
            var rule = _operationDB.NewCorrelationRules.Where(x => x.RuleName == ruleName).FirstOrDefault();

            if (rule == null)
                return null;

            return PrepareModel(rule);
        }
    }
}
