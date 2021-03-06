﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STC.Projects.WPFControlLibrary.MapControl.AccidentsServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AccidentsServiceReference.IAccidentsLayer")]
    public interface IAccidentsLayer {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/UploadAccidentImage", ReplyAction="http://tempuri.org/IAccidentsLayer/UploadAccidentImageResponse")]
        bool UploadAccidentImage(byte[] image, string refNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/UploadAccidentImage", ReplyAction="http://tempuri.org/IAccidentsLayer/UploadAccidentImageResponse")]
        System.Threading.Tasks.Task<bool> UploadAccidentImageAsync(byte[] image, string refNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/SearchAccidents", ReplyAction="http://tempuri.org/IAccidentsLayer/SearchAccidentsResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentsDTO[] SearchAccidents(STC.Projects.ClassLibrary.DTO.AccidentSearchRequestDTO searchReq);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/SearchAccidents", ReplyAction="http://tempuri.org/IAccidentsLayer/SearchAccidentsResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentsDTO[]> SearchAccidentsAsync(STC.Projects.ClassLibrary.DTO.AccidentSearchRequestDTO searchReq);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllAreas", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllAreasResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentAreaDTO[] GetAllAreas();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllAreas", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllAreasResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentAreaDTO[]> GetAllAreasAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllCities", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllCitiesResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentCityDTO[] GetAllCities();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllCities", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllCitiesResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentCityDTO[]> GetAllCitiesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllEmirates", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllEmiratesResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentEmirateDTO[] GetAllEmirates();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllEmirates", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllEmiratesResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentEmirateDTO[]> GetAllEmiratesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllLocations", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllLocationsResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentLocationDTO[] GetAllLocations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllLocations", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllLocationsResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentLocationDTO[]> GetAllLocationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllLocationTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllLocationTypesResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentLocationTypeDTO[] GetAllLocationTypes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllLocationTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllLocationTypesResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentLocationTypeDTO[]> GetAllLocationTypesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllRoadTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllRoadTypesResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentRoadTypesDTO[] GetAllRoadTypes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllRoadTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllRoadTypesResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentRoadTypesDTO[]> GetAllRoadTypesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllCrashTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllCrashTypesResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentCrashTypeDTO[] GetAllCrashTypes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllCrashTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllCrashTypesResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentCrashTypeDTO[]> GetAllCrashTypesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllWeather", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllWeatherResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentWeatherDTO[] GetAllWeather();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllWeather", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllWeatherResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentWeatherDTO[]> GetAllWeatherAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllLighting", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllLightingResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentLightingDTO[] GetAllLighting();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllLighting", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllLightingResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentLightingDTO[]> GetAllLightingAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllSeverties", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllSevertiesResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentSevertiesDTO[] GetAllSeverties();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllSeverties", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllSevertiesResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentSevertiesDTO[]> GetAllSevertiesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllPCondition", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllPConditionResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentPConditionDTO[] GetAllPCondition();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllPCondition", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllPConditionResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentPConditionDTO[]> GetAllPConditionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllPoliceStations", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllPoliceStationsResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentPoliceStationDTO[] GetAllPoliceStations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllPoliceStations", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllPoliceStationsResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentPoliceStationDTO[]> GetAllPoliceStationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllReportTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllReportTypesResponse")]
        STC.Projects.ClassLibrary.DTO.IncidentReportTypesDTO[] GetAllReportTypes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccidentsLayer/GetAllReportTypes", ReplyAction="http://tempuri.org/IAccidentsLayer/GetAllReportTypesResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentReportTypesDTO[]> GetAllReportTypesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAccidentsLayerChannel : STC.Projects.WPFControlLibrary.MapControl.AccidentsServiceReference.IAccidentsLayer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AccidentsLayerClient : System.ServiceModel.ClientBase<STC.Projects.WPFControlLibrary.MapControl.AccidentsServiceReference.IAccidentsLayer>, STC.Projects.WPFControlLibrary.MapControl.AccidentsServiceReference.IAccidentsLayer {
        
        public AccidentsLayerClient() {
        }
        
        public AccidentsLayerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AccidentsLayerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccidentsLayerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccidentsLayerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool UploadAccidentImage(byte[] image, string refNum) {
            return base.Channel.UploadAccidentImage(image, refNum);
        }
        
        public System.Threading.Tasks.Task<bool> UploadAccidentImageAsync(byte[] image, string refNum) {
            return base.Channel.UploadAccidentImageAsync(image, refNum);
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentsDTO[] SearchAccidents(STC.Projects.ClassLibrary.DTO.AccidentSearchRequestDTO searchReq) {
            return base.Channel.SearchAccidents(searchReq);
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentsDTO[]> SearchAccidentsAsync(STC.Projects.ClassLibrary.DTO.AccidentSearchRequestDTO searchReq) {
            return base.Channel.SearchAccidentsAsync(searchReq);
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentAreaDTO[] GetAllAreas() {
            return base.Channel.GetAllAreas();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentAreaDTO[]> GetAllAreasAsync() {
            return base.Channel.GetAllAreasAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentCityDTO[] GetAllCities() {
            return base.Channel.GetAllCities();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentCityDTO[]> GetAllCitiesAsync() {
            return base.Channel.GetAllCitiesAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentEmirateDTO[] GetAllEmirates() {
            return base.Channel.GetAllEmirates();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentEmirateDTO[]> GetAllEmiratesAsync() {
            return base.Channel.GetAllEmiratesAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentLocationDTO[] GetAllLocations() {
            return base.Channel.GetAllLocations();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentLocationDTO[]> GetAllLocationsAsync() {
            return base.Channel.GetAllLocationsAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentLocationTypeDTO[] GetAllLocationTypes() {
            return base.Channel.GetAllLocationTypes();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentLocationTypeDTO[]> GetAllLocationTypesAsync() {
            return base.Channel.GetAllLocationTypesAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentRoadTypesDTO[] GetAllRoadTypes() {
            return base.Channel.GetAllRoadTypes();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentRoadTypesDTO[]> GetAllRoadTypesAsync() {
            return base.Channel.GetAllRoadTypesAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentCrashTypeDTO[] GetAllCrashTypes() {
            return base.Channel.GetAllCrashTypes();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentCrashTypeDTO[]> GetAllCrashTypesAsync() {
            return base.Channel.GetAllCrashTypesAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentWeatherDTO[] GetAllWeather() {
            return base.Channel.GetAllWeather();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentWeatherDTO[]> GetAllWeatherAsync() {
            return base.Channel.GetAllWeatherAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentLightingDTO[] GetAllLighting() {
            return base.Channel.GetAllLighting();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentLightingDTO[]> GetAllLightingAsync() {
            return base.Channel.GetAllLightingAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentSevertiesDTO[] GetAllSeverties() {
            return base.Channel.GetAllSeverties();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentSevertiesDTO[]> GetAllSevertiesAsync() {
            return base.Channel.GetAllSevertiesAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentPConditionDTO[] GetAllPCondition() {
            return base.Channel.GetAllPCondition();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentPConditionDTO[]> GetAllPConditionAsync() {
            return base.Channel.GetAllPConditionAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentPoliceStationDTO[] GetAllPoliceStations() {
            return base.Channel.GetAllPoliceStations();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentPoliceStationDTO[]> GetAllPoliceStationsAsync() {
            return base.Channel.GetAllPoliceStationsAsync();
        }
        
        public STC.Projects.ClassLibrary.DTO.IncidentReportTypesDTO[] GetAllReportTypes() {
            return base.Channel.GetAllReportTypes();
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.IncidentReportTypesDTO[]> GetAllReportTypesAsync() {
            return base.Channel.GetAllReportTypesAsync();
        }
    }
}
