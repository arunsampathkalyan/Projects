using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{

    public class CorrelationMessagesLogDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<CorrelationMessagesLogDTO> GetCorelationsLogByBusinessRule(int businessRuleId)
        {
            List<CorrelationMessagesLogDTO> list = new List<CorrelationMessagesLogDTO>();

            CorrelationMessagesLogDTO dto = null;

            var occurances = operationalDataContext.CorrelationMessagesLogViews.Where(x => x.BusinessRuleId == businessRuleId).ToList();

            if (occurances != null && occurances.Count > 0)
            {
                foreach (var item in occurances)
                {
                    dto = new CorrelationMessagesLogDTO();

                    dto.BusinessRuleId = item.BusinessRuleId;
                    dto.BusinessRuleName = item.RuleName;
                    dto.CorrelationDate = item.CorrelationDate.Value;
                    dto.PlateNumber = item.PlateNumber;
                    dto.PlateColor = item.PlateColor;
                    dto.PlateKind = item.PlateKind;
                    dto.PlateSource = item.PlateSource;

                    list.Add(dto);
                }
            }

            return list;
        }

        public List<CorrelationMessagesLogDTO> GetCorrelationLogByVehicleDetails(string plateNumber, string plateColor, string plateSource, string plateKind)
        {
            var res = new List<CorrelationMessagesLogDTO>();
            var output = operationalDataContext.SearchForDangerousViolatorViews
                .Where(x => (plateColor == "" || x.PlateColor == plateColor) && (plateSource == "" || x.PlateSource == plateSource) && (plateKind == "" || x.PlateKind == plateKind))
                .OrderByDescending(x => x.CorrelationDate).ToList().Where(x => (plateNumber == "" || x.PlateNumber.Contains(plateNumber)));

            if (output != null)
            {
                foreach (var item in output)
                {
                    res.Add(new CorrelationMessagesLogDTO()
                    {
                        Id = item.Id,
                        MessageId = item.MessageId,
                        DateCreated = item.DateCreated,
                        CorrelationDate = item.CorrelationDate,
                        PlateNumber = item.PlateNumber,
                        PlateColor = item.PlateColorName,
                        PlateKind = item.PlateKindName,
                        PlateSource = item.PlateSourceName,
                        BusinessRuleId = (int)item.BusinessRuleId,
                        BusinessRuleName = item.RuleName
                    });
                }
                //return res;
            }

            return res;
        }

        public bool IsDangerousVehicleActive(string plateNumber, string plateColor, string plateSource, string plateKind)
        {
            var output = operationalDataContext.CorrelationMessagesLogs
                            .Where(x => x.PlateNumber == plateNumber && x.PlateColor == plateColor && x.PlateSource == plateSource && x.PlateKind == plateKind).ToList();

            if (output != null && output.Count > 0)
            {
                foreach (var item in output)
                {
                    var usercontrol = operationalDataContext.UserUserControlViews.Where(y => y.UserUserControlsID == item.MessageId).FirstOrDefault();

                    if (usercontrol != null)
                    {
                        var notification = operationalDataContext.Notifications.Where(y => y.NotificationId == usercontrol.NotificationId).FirstOrDefault();

                        if (notification != null)
                        {
                            return notification.LastStatus == 1 || notification.LastStatus == 2;
                        }
                    }
                }
            }

            return false;
        }

        public void DeactivateDangerousViolatorList(List<PlateDetailsDTO> vehicles)
        {
            if (vehicles != null && vehicles.Count > 0)
            {
                foreach (var item in vehicles)
                {
                    DeactivateDangeriousVehicle(item.plateNumber, item.plateColor, item.plateSource, item.plateKind);
                }
            }
        }

        public void DeactivateDangeriousVehicle(string plateNumber, string plateColor, string plateSource, string plateKind)
        {
            var output = operationalDataContext.CorrelationMessagesLogs
                            .Where(x => x.PlateNumber == plateNumber && x.PlateColor == plateColor && x.PlateSource == plateSource && x.PlateKind == plateKind).ToList();

            if (output != null && output.Count > 0)
            {
                foreach (var item in output)
                {
                    item.IsActive = false;
                }

                operationalDataContext.SaveChanges();
            }
        }
    }
}
