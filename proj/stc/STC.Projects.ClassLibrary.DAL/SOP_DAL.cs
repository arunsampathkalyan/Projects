using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class SopDAL
    {

        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<MessageTypeSOPDTO> GetMessageTypeSOP(int MessageType)
        {
            try
            {
                var lstMessageTypeSOP = operationalDataContext.Conf_SOPSourceSOPView
                    .Where(S => S.SourceId == MessageType)
                    .Select(MessageTypeSOP => new MessageTypeSOPDTO
                    {
                        MessageTypeId = MessageTypeSOP.SourceId,
                        MessageTypeName = MessageTypeSOP.SourceName,
                        Rank = MessageTypeSOP.Rank,
                        PriorityId = MessageTypeSOP.PriorityId,
                        SOPContent = MessageTypeSOP.SOPContent,
                        SOPId = MessageTypeSOP.SOPId,
                        SOPControlName = MessageTypeSOP.SOPViewModelType,
                        SOPDetailsControlName = MessageTypeSOP.SOPViewDetailsControl,
                        SOPDetailsDataMessageType = MessageTypeSOP.SOPViewDetailsMessage,
                        SOPDetailsXSLT = MessageTypeSOP.MessageDetailsXSLT,
                        SOPListDataMessageType = MessageTypeSOP.SOPViewListMessage,
                        SOPListXSLT = MessageTypeSOP.MessageListXSLT
                    }).OrderBy(S => S.Rank).ToList();

                return lstMessageTypeSOP;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<MessageTypeSOPDTO> GetMessageTypeSOP(int MessageType, int? PriorityId, long? notificationId = null)
        {
            try
            {
                var lstMessageTypeSOP = operationalDataContext.Conf_SOPSourceSOPView
                    .Where(S => S.SourceId == MessageType && ((!PriorityId.HasValue && !S.PriorityId.HasValue) || (PriorityId.HasValue && PriorityId == S.PriorityId)))
                    .Select(MessageTypeSOP => new MessageTypeSOPDTO
                    {
                        MessageTypeId = MessageTypeSOP.SourceId,
                        MessageTypeName = MessageTypeSOP.SourceName,
                        Rank = MessageTypeSOP.Rank,
                        PriorityId = MessageTypeSOP.PriorityId,
                        SOPContent = MessageTypeSOP.SOPContent,
                        SOPId = MessageTypeSOP.SOPId,
                        SOPControlName = MessageTypeSOP.SOPViewModelType,
                        SOPDetailsControlName = MessageTypeSOP.SOPViewDetailsControl,
                        SOPDetailsDataMessageType = MessageTypeSOP.SOPViewDetailsMessage,
                        SOPDetailsXSLT = MessageTypeSOP.MessageDetailsXSLT,
                        SOPListDataMessageType = MessageTypeSOP.SOPViewListMessage,
                        SOPListXSLT = MessageTypeSOP.MessageListXSLT,
                        SOPViewDetailsMessageId = MessageTypeSOP.SOPViewDetailsMessageId,
                        SOPViewListMessageId = MessageTypeSOP.SOPViewListMessageId,
                        IsDone = (notificationId.HasValue && operationalDataContext.NotificationSOPLogs.Any(x=> x.NotificationId == notificationId.Value && x.SOPStepId == MessageTypeSOP.SOPId)),
                    }).OrderBy(S => S.Rank).ToList();

                return lstMessageTypeSOP;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<MessageTypeSOPDTO> GetAllMessageTypesSOP()
        {
            try
            {
                var lstMessageTypeSOP = operationalDataContext.Conf_SOPSourceSOPView
                    .Select(MessageTypeSOP => new MessageTypeSOPDTO
                    {
                        MessageTypeId = MessageTypeSOP.SourceId,
                        MessageTypeName = MessageTypeSOP.SourceName,
                        Rank = MessageTypeSOP.Rank,
                        PriorityId = MessageTypeSOP.PriorityId,
                        SOPContent = MessageTypeSOP.SOPContent,
                        SOPId = MessageTypeSOP.SOPId,
                        SOPControlName = MessageTypeSOP.SOPViewModelType,
                        SOPDetailsControlName = MessageTypeSOP.SOPViewDetailsControl,
                        SOPDetailsDataMessageType = MessageTypeSOP.SOPViewDetailsMessage,
                        SOPDetailsXSLT = MessageTypeSOP.MessageDetailsXSLT,
                        SOPListDataMessageType = MessageTypeSOP.SOPViewListMessage,
                        SOPListXSLT = MessageTypeSOP.MessageListXSLT,
                        SOPViewDetailsMessageId = MessageTypeSOP.SOPViewDetailsMessageId,
                        SOPViewListMessageId = MessageTypeSOP.SOPViewListMessageId
                    }).OrderBy(S => S.Rank).ToList();

                return lstMessageTypeSOP;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public bool SaveSOPConfiguration(List<MessageTypeSOPDTO> messageTypes)
        {
            if (messageTypes != null && messageTypes.Any())
            {
                var msgType = messageTypes[0].MessageTypeId;
                var priId = messageTypes[0].PriorityId;
                var oldEntities = operationalDataContext.Conf_SOPSourceSOP.Where(x => x.SourceId == msgType && x.PriorityId == priId).ToList();
                if (oldEntities != null && oldEntities.Any())
                    operationalDataContext.Conf_SOPSourceSOP.RemoveRange(oldEntities);
                foreach (var item in messageTypes)
                {
                    var entity = new Conf_SOPSourceSOP
                    {
                        PriorityId = item.PriorityId,
                        Rank = item.Rank,
                        SOPId = item.SOPId,
                        SOPViewDetailsControl = item.SOPDetailsControlName,
                        SOPViewDetailsMessageId = item.SOPViewDetailsMessageId,
                        SOPViewListMessageId = item.SOPViewListMessageId,
                        SOPViewModelType = item.SOPControlName,
                        SourceId = item.MessageTypeId
                    };
                    operationalDataContext.Conf_SOPSourceSOP.Add(entity);
                }
                return operationalDataContext.SaveChanges() > 0;
            }
            return false;
        }

        public List<SopDTO> GetAllSopSteps()
        {
            return operationalDataContext.Conf_SOP.Where(x => x.IsConfigure.HasValue && x.IsConfigure.Value).Select(x =>
                 new SopDTO()
                 {
                     SOPContent = x.SOPContent,
                     SOPId = x.SOPId,
                     SOPViewDetailsControl = x.SOPViewDetailsControl,
                     SOPViewDetailsMessageId = x.SOPViewDetailsMessageId,
                     SOPViewListMessageId = x.SOPViewListMessageId,
                     SOPViewModelType = x.SOPViewModelType
                 }).ToList();
        }

        public List<SopSourcesDTO> GetAllSopSources()
        {
            return operationalDataContext.Conf_SOPSource.Where(x => x.IsConfigure.HasValue && x.IsConfigure.Value).Select(x =>
                 new SopSourcesDTO()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     ArabicDescription = x.ArabicDescription,
                     EnglishDescription = x.EnglishDescription
                 }).ToList();
        }
        public List<TowerActionsDTO> GetAllTowersActions()
        {
            var actions = operationalDataContext.TowerActions.Select(x => new TowerActionsDTO
            {
                Description = x.Description,
                TowerActionId = x.TowerActionId.ToString()
            }).ToList();
            return actions;
        }
    }
}
