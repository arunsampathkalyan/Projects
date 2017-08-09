using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System.Data.Entity.Spatial;

namespace STC.Projects.ClassLibrary.DAL
{
    public class OfficersDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<OfficersLastLocationViewDTO> GetUpdatedOfficersList(bool? IsNoticed)
        {
            try
            {
                var lstOfficers = operationalDataContext.OfficersLastLocationsViews
                    .Where(x=> ! IsNoticed.HasValue || (x.IsNoticed == IsNoticed.Value))
                  .Select(Officer => new OfficersLastLocationViewDTO
                  {
                      Altitude = Officer.Altitude,
                      Latitude = Officer.Latitude,
                      LocationDate = Officer.LocationDate,
                      Longitude = Officer.Longitde,
                      OfficerCode = Officer.OfficerCode,
                      OfficerId = Officer.OfficerId,
                      OfficerLastLocationId = Officer.OfficerLastLocationId,
                      Speed = Officer.Speed,
                      OfficerName = Officer.OfficerName,
                      CameraURL = Officer.CameraURL,
                      IsNoticed = Officer.IsNoticed,
                  }).ToList();

                return lstOfficers;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

    }
}
