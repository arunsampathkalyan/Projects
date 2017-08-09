using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class BusinessRuleDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();
        public List<BusinessRulesDTO> GetAllBusinessRules(bool IsActivatedOnly = true)
        {
            return operationalDataContext.CorrelationBusinessRules.Where(x => (!IsActivatedOnly) || (IsActivatedOnly && x.IsDeleted == false))
                .Select(y => new BusinessRulesDTO
                {
                    BusinessName = y.BusinessName,
                    BusinessRuleId = y.BusinessRuleId,
                    Priority = y.BusinessRulePriority != null ? y.BusinessRulePriority.PriorityNameEn : "-",
                    VehicleType = y.VehicleType != null ? y.VehicleType.VehicleTypeNameEn : "-",
                    CreatedAt = y.CreatedAt,
                    CreatedBy = y.CreatedBy,
                    InsideCityOverSpeedId = y.InsideCityOverSpeedId,
                    InsideCityOverSpeedQty = y.InsideCityOverSpeedQty,
                    InsideCityOverSpeedValue = y.OverSpeed != null ? y.OverSpeed.OverSpeedValue : 0,
                    IsDeleted = y.IsDeleted,
                    IsOverSpeedInsideCity = y.IsOverSpeedInsideCity,
                    IsOverSpeedOutsideCity = y.IsOverSpeedOutsideCity,
                    IsTrafficCross = y.IsTrafficCross,
                    LastModifiedAt = y.LastModifiedAt,
                    LastModifiedBy = y.LastModifiedBy,
                    OutsideCityOverSpeedId = y.OutsideCityOverSpeedId,
                    OutsideCityOverSpeedQty = y.OutsideCityOverSpeedQty,
                    OutsideCityOverSpeedValue = y.OverSpeed1 != null ? y.OverSpeed1.OverSpeedValue : 0,
                    PriorityId = y.PriorityId,
                    RuleInterval = y.RuleInterval,
                    TrafficCrossQty = y.TrafficCrossQty,
                    TrafficCrossTimesId = y.TrafficCrossTimesId,
                    TrafficCrossTimesValue = y.TrafficCrossTime != null ? y.TrafficCrossTime.TrafficCrossTimeValue : 0,
                    VehicleTypeId = y.VehicleTypeId,
                    NumOfOccur = y.NumberOfOccur.HasValue ? y.NumberOfOccur.Value : 0
                }).ToList();
        }
        public BusinessRulesDTO GetBusinessRuleByMessageId(long messageId)
        {
            var log = operationalDataContext.CorrelationMessagesLogs.FirstOrDefault(x => x.MessageId == messageId);
            if (log != null && log.NewCorrelationRule != null)
            {
                return new BusinessRulesDTO()
                {
                    BusinessName = log.NewCorrelationRule.RuleName,
                    BusinessRuleId = (int)log.BusinessRuleId,
                    BusinessRuleTime = (log.NewCorrelationRule.IntervalPeriod * 60).ToString()
                };
            }
            return null;
        }
        public void UpdateOccurNumber(int businessRuleId, long messageId, string pNumber, string pCategory, string pAuthority, string pColor)
        {
            var rule = operationalDataContext.NewCorrelationRules.FirstOrDefault(x => x.IsDeleted == false && x.BusinessRuleId == businessRuleId);
            if (rule != null)
            {
                    rule.NumberOfOccur++;
                

                if (messageId > 0)
                {
                    operationalDataContext.CorrelationMessagesLogs.Add(new CorrelationMessagesLog() { BusinessRuleId = businessRuleId, CorrelationDate = DateTime.Now, DateCreated = DateTime.Now, MessageId = messageId, PlateNumber = pNumber, PlateKind = pCategory, PlateSource = pAuthority, PlateColor = pColor });
                }
                operationalDataContext.SaveChanges();
            }
        }
        public bool SaveBusinessRule(BusinessRulesDTO businessRule)
        {
            var business = operationalDataContext.CorrelationBusinessRules.FirstOrDefault(x => x.BusinessRuleId == businessRule.BusinessRuleId);
            if (business == null)
            {
                business = new CorrelationBusinessRule();
                business.BusinessName = businessRule.BusinessName;
                business.BusinessRuleId = businessRule.BusinessRuleId;
                business.CreatedAt = businessRule.CreatedAt;
                business.CreatedBy = businessRule.CreatedBy;
                business.InsideCityOverSpeedId = businessRule.InsideCityOverSpeedId;
                business.InsideCityOverSpeedQty = businessRule.InsideCityOverSpeedQty;
                business.IsDeleted = businessRule.IsDeleted;
                business.IsOverSpeedInsideCity = businessRule.IsOverSpeedInsideCity;
                business.IsOverSpeedOutsideCity = businessRule.IsOverSpeedOutsideCity;
                business.IsTrafficCross = businessRule.IsTrafficCross;
                business.LastModifiedAt = businessRule.LastModifiedAt;
                business.LastModifiedBy = businessRule.LastModifiedBy;
                business.OutsideCityOverSpeedId = businessRule.OutsideCityOverSpeedId;
                business.OutsideCityOverSpeedQty = businessRule.OutsideCityOverSpeedQty;
                business.PriorityId = businessRule.PriorityId;
                business.RuleInterval = businessRule.RuleInterval;
                business.TrafficCrossQty = businessRule.TrafficCrossQty;
                business.TrafficCrossTimesId = businessRule.TrafficCrossTimesId;
                business.VehicleTypeId = businessRule.VehicleTypeId;
                operationalDataContext.CorrelationBusinessRules.Add(business);
            }
            else
            {
                business.IsDeleted = businessRule.IsDeleted;
            }
            return operationalDataContext.SaveChanges() > 0;
        }

        public BusinessRulesDTO GetBusinessRuleByID(int businessRuleId)
        {
            return operationalDataContext.CorrelationBusinessRules.Where(x => x.IsDeleted == false && x.BusinessRuleId == businessRuleId)
                .Select(y => new BusinessRulesDTO
                {
                    BusinessName = y.BusinessName,
                    BusinessRuleId = y.BusinessRuleId,
                    CreatedAt = y.CreatedAt,
                    CreatedBy = y.CreatedBy,
                    InsideCityOverSpeedId = y.InsideCityOverSpeedId,
                    InsideCityOverSpeedQty = y.InsideCityOverSpeedQty,
                    InsideCityOverSpeedValue = y.OverSpeed != null ? y.OverSpeed.OverSpeedValue : 0,
                    IsDeleted = y.IsDeleted,
                    IsOverSpeedInsideCity = y.IsOverSpeedInsideCity,
                    IsOverSpeedOutsideCity = y.IsOverSpeedOutsideCity,
                    IsTrafficCross = y.IsTrafficCross,
                    LastModifiedAt = y.LastModifiedAt,
                    LastModifiedBy = y.LastModifiedBy,
                    OutsideCityOverSpeedId = y.OutsideCityOverSpeedId,
                    OutsideCityOverSpeedQty = y.OutsideCityOverSpeedQty,
                    OutsideCityOverSpeedValue = y.OverSpeed1 != null ? y.OverSpeed1.OverSpeedValue : 0,
                    PriorityId = y.PriorityId,
                    RuleInterval = y.RuleInterval,
                    TrafficCrossQty = y.TrafficCrossQty,
                    TrafficCrossTimesId = y.TrafficCrossTimesId,
                    TrafficCrossTimesValue = y.TrafficCrossTime != null ? y.TrafficCrossTime.TrafficCrossTimeValue : 0,
                    VehicleTypeId = y.VehicleTypeId
                }).FirstOrDefault();
        }

        public List<BusinessRulePriorityDTO> GetAllPriorities()
        {
            return operationalDataContext.BusinessRulePriorities.Where(x => x.IsDeleted == false)
                .Select(x => new BusinessRulePriorityDTO
                {
                    IsDeleted = x.IsDeleted,
                    PriorityID = x.BusinessRulePriorityId,
                    PriorityNameAr = x.PriorityNameAr,
                    PriorityNameEn = x.PriorityNameEn
                }).ToList();
        }

        public List<OverSpeedDTO> GetAllOverSpeed()
        {
            return operationalDataContext.OverSpeeds.Where(x => x.IsDeleted == false)
                .Select(x => new OverSpeedDTO
                {
                    IsDeleted = x.IsDeleted,
                    OverSpeedId = x.OverSpeedId,
                    OverSpeedValue = x.OverSpeedValue
                }).ToList();
        }

        public List<TrafficCrossDTO> GetAllTrafficCrossTimes()
        {
            return operationalDataContext.TrafficCrossTimes.Where(x => x.IsDeleted == false)
                .Select(x => new TrafficCrossDTO
                {
                    IsDeleted = x.IsDeleted,
                    TrafficCrossId = x.TrafficCrossTimeId,
                    TrafficCrossValue = x.TrafficCrossTimeValue
                }).ToList();
        }

        public List<VehicleTypeDTO> GetAllVehicleTypes()
        {
            return operationalDataContext.VehicleTypes.Where(x => x.IsDeleted == false)
                .Select(x => new VehicleTypeDTO
                {
                    IsDeleted = x.IsDeleted,
                    VehicleTypeAr = x.VehicleTypeNameAr,
                    VehicleTypeEn = x.VehicleTypeNameEn,
                    VehicleTypeID = x.VehicleTypeId
                }).ToList();
        }
    }
}
