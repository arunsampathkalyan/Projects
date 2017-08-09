using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IConfigurationLayer" in both code and config file together.
    [ServiceContract]
    public interface IConfigurationLayer
    {
        [OperationContract]
        [WebGet]
        List<PageDTO> GetAllPages(int UserId);
        [OperationContract]
        [WebGet]
        List<ConfPage> GetAllConfiguredPages();
        [OperationContract]
        [WebGet]
        List<ConfControls> GetAllControls();
        [OperationContract]
        [WebGet]
        List<ConfLayout> GetAllLayouts();
        [OperationContract]
        [WebGet]
        bool SaveConfiguration(ConfPage Page);
        [OperationContract]
        [WebGet]
        List<MessageTypeSOPDTO> GetAllMessageTypesSOP();
        [OperationContract]
        [WebGet]
        List<MessageTypeSOPDTO> GetAllMessageTypesForSopSource(int eventId, int? priorityId);
        [OperationContract]
        [WebGet]
        bool SaveSOPConfiguration(List<MessageTypeSOPDTO> messageTypes);
        [OperationContract]
        [WebGet]
        List<SopDTO> GetAllSopSteps();
        [OperationContract]
        [WebGet]
        List<SopSourcesDTO> GetAllSopSources();
    }
}
