using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.AnalysisServices;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccidentsLayer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccidentsLayer.svc or AccidentsLayer.svc.cs at the Solution Explorer and start debugging.
    public class AccidentsLayer : IAccidentsLayer
    {
        public bool UploadAccidentImage(byte[] image, string refNum)
        {
            var client = new TimsService.TimsArmsServiceClient();

            client.insertAccidentImageToDb(new TimsService.TRAFFIC_ACC_PHOTO
            {
                PHOTO = image,
                ACC_KEY = refNum
            });

            return client.insertAccidentImageToFile(new TimsService.TRAFFIC_ACC_PHOTO
            {
                PHOTO = image,
                ACC_KEY = refNum
            });
            //client.vehicleSearch()
        }

        public UploadedFileReportDTO UploadAccidents(List<AccidentStandardDTO> accidents)
        {
            try
            {
                var dal = new AccidentStandardDAL();

                if (!dal.SaveAccidentListToOperational(accidents) || !dal.SaveAccidentsListToCDS(accidents))
                    return null;

                return new UploadedFileReportDTO
                {
                    InsertedRowsCount = dal.insertedRowsCount,
                    DuplicatedRowsCount = dal.duplicatedRowsCount,
                    CorruptedRowsCount = dal.corruptedRowsCount
                };
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return null;
            }
        }

        public List<IncidentsDTO> SearchAccidents(AccidentSearchRequestDTO searchReq)
        {
            return new IncidentsDAL().SearchActiveIncidents(searchReq);
        }
        public List<IncidentAreaDTO> GetAllAreas()
        {
            return new IncidentsDAL().GetAllAreas();
        }

        public List<IncidentCityDTO> GetAllCities()
        {
            return new IncidentsDAL().GetAllCities();
        }

        public List<IncidentEmirateDTO> GetAllEmirates()
        {
            return new IncidentsDAL().GetAllEmirates();
        }

        public List<IncidentLocationDTO> GetAllLocations()
        {
            return new IncidentsDAL().GetAllLocations();
        }

        public List<IncidentLocationTypeDTO> GetAllLocationTypes()
        {
            return new IncidentsDAL().GetAllLocationsTypes();
        }

        public List<IncidentRoadTypesDTO> GetAllRoadTypes()
        {
            return new IncidentsDAL().GetAllRoadTypes();
        }


        public List<IncidentCrashTypeDTO> GetAllCrashTypes()
        {
            return new IncidentsDAL().GetAllCrashTypes();
        }

        public List<IncidentWeatherDTO> GetAllWeather()
        {
            return new IncidentsDAL().GetAllWeather();
        }


        public List<IncidentLightingDTO> GetAllLighting()
        {
            return new IncidentsDAL().GetAllLighting();
        }


        public List<IncidentSevertiesDTO> GetAllSeverties()
        {
            return new IncidentsDAL().GetAllSeverties();
        }


        public List<IncidentPConditionDTO> GetAllPCondition()
        {
            return new IncidentsDAL().GetAllPCondition();
        }


        public List<IncidentPoliceStationDTO> GetAllPoliceStations()
        {
            return new IncidentsDAL().GetAllPoliceStations();
        }


        public List<IncidentReportTypesDTO> GetAllReportTypes()
        {
            return new IncidentsDAL().GetAllReportTypes();
        }

        public bool AddUploadedFile(UploadedFileDTO uploadedFile)
        {
            return new AccidentStandardDAL().AddUploadedFile(uploadedFile);
        }

        public List<UploadedFileDTO> GetAccidentUploadedFiles()
        {
            return new AccidentStandardDAL().GetAccidentUploadedFiles();
        }

        public bool ProcessAccidentCube()
        {
            try
            {
                Server server = new Server();

                server.Connect("Data source=.;Timeout=7200000;Integrated Security=SSPI");

                Database database = server.Databases.FindByName("CallOfServiceFinalCube");

                database.Process(ProcessType.ProcessFull);

                return true;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return false;
            }
        }

        public List<ExcelMapDTO> GetAccidentExcelMappingDetails()
        {
            return new AccidentStandardDAL().GetAccidentExcelMappingDetails();
        }
    }
}
