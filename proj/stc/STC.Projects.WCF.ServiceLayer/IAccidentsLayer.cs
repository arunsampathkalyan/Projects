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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccidentsLayer" in both code and config file together.
    [ServiceContract]
    public interface IAccidentsLayer
    {
        [OperationContract]
        [WebGet]
        bool UploadAccidentImage(byte[] image, string refNum);

        [OperationContract]//(IsOneWay = true)]
        [WebGet]
        UploadedFileReportDTO UploadAccidents(List<AccidentStandardDTO> accidents);

        [OperationContract]
        [WebGet]
        List<IncidentsDTO> SearchAccidents(AccidentSearchRequestDTO searchReq);

        [OperationContract]
        [WebGet]
        List<IncidentAreaDTO> GetAllAreas();

        [OperationContract]
        [WebGet]
        List<IncidentCityDTO> GetAllCities();

        [OperationContract]
        [WebGet]
        List<IncidentEmirateDTO> GetAllEmirates();

        [OperationContract]
        [WebGet]
        List<IncidentLocationDTO> GetAllLocations();

        [OperationContract]
        [WebGet]
        List<IncidentLocationTypeDTO> GetAllLocationTypes();

        [OperationContract]
        [WebGet]
        List<IncidentRoadTypesDTO> GetAllRoadTypes();

        [OperationContract]
        [WebGet]
        List<IncidentCrashTypeDTO> GetAllCrashTypes();

        [OperationContract]
        [WebGet]
        List<IncidentWeatherDTO> GetAllWeather();

        [OperationContract]
        [WebGet]
        List<IncidentLightingDTO> GetAllLighting();

        [OperationContract]
        [WebGet]
        List<IncidentSevertiesDTO> GetAllSeverties();

        [OperationContract]
        [WebGet]
        List<IncidentPConditionDTO> GetAllPCondition();

        [OperationContract]
        [WebGet]
        List<IncidentPoliceStationDTO> GetAllPoliceStations();

        [OperationContract]
        [WebGet]
        List<IncidentReportTypesDTO> GetAllReportTypes();
        [OperationContract]
        [WebGet]
        bool AddUploadedFile(UploadedFileDTO uploadedFile);
        [OperationContract]
        [WebGet]
        List<UploadedFileDTO> GetAccidentUploadedFiles();
        [OperationContract]
        [WebGet]
        bool ProcessAccidentCube();
        [OperationContract]
        [WebGet]
        List<ExcelMapDTO> GetAccidentExcelMappingDetails();
    }
}
