using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class ViolationsKPIsDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();
        public List<ViolationsCountPerDayOfWeekDTO> GetViolationsCountPerDayOfWeek()
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountPerDayOfWeekViews
                    .Select(ViolationsCount => new ViolationsCountPerDayOfWeekDTO
                    {
                        Count = ViolationsCount.Count,
                        DayOfWeek = ViolationsCount.DayOfWeekName,
                        id = ViolationsCount.id
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedbyViolationType()
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountGroupedByTypeViews
                    .Select(ViolationsCount => new ViolationsCountGroupedByTypeDTO
                    {
                        Count = ViolationsCount.Count,
                        ViolationTypeId = ViolationsCount.ViolationTypeId,
                        ViolationTypeName = ViolationsCount.ViolationTypeName
                    }).ToList();
             
                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedbyViolationType(string dayOfWeek)
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountGroupedByTypePerDayOfWeekViews.Where(x => String.IsNullOrEmpty(dayOfWeek) == true || x.DayOfWeekName == dayOfWeek.ToLower())
                    .Select(ViolationsCount => new ViolationsCountGroupedByTypeDTO
                    {
                        Count = ViolationsCount.Count,
                        ViolationTypeId = ViolationsCount.ViolationTypeId,
                        ViolationTypeName=ViolationsCount.ViolationTypeName
                    }).ToList();
                
                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedbyViolationType(string dayOfWeek,int hour)
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountGroupedByTypePerDayOfWeekAndHourViews.Where(
                    x => (String.IsNullOrEmpty(dayOfWeek) == true || x.DayOfWeekName == dayOfWeek.ToLower())
                        && (hour==x.ViolationHour)
                    )
                    .Select(ViolationsCount => new ViolationsCountGroupedByTypeDTO
                    {
                        Count = ViolationsCount.Count,
                        ViolationTypeId = ViolationsCount.ViolationTypeId,
                        ViolationTypeName = ViolationsCount.ViolationTypeName
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedbyLocation()
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountGroupedByLocationViews
                    .Select(ViolationsCount => new ViolationsCountGroupedByLocationDTO
                    {
                        Count = ViolationsCount.Count,
                        LocationCode = ViolationsCount.LocationCode,
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedbyLocation(string dayOfWeek)
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountGroupedByLocationPerDayOfWeekViews.Where(x => dayOfWeek == null || x.DayOfWeekName == dayOfWeek.ToLower())
                    .Select(ViolationsCount => new ViolationsCountGroupedByLocationDTO
                    {
                        Count = ViolationsCount.Count,
                        LocationCode = ViolationsCount.LocationCode,
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedbyLocation(string dayOfWeek,int hour)
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountGroupedByLocationPerDayOfWeekAndHourViews.Where(
                    x => (String.IsNullOrEmpty(dayOfWeek) == true || x.DayOfWeekName == dayOfWeek.ToLower())
                    && x.ViolationHour==hour
                    )
                    .Select(ViolationsCount => new ViolationsCountGroupedByLocationDTO
                    {
                        Count = ViolationsCount.Count,
                        LocationCode = ViolationsCount.LocationCode,
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<ViolationsCountPerDayOfWeekAndHourDTO> GetViolationsCountPerDayOfWeekAndHour()
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationsCountPerDayOfWeekAndHourViews
                    .Select(ViolationsCount => new ViolationsCountPerDayOfWeekAndHourDTO
                    {
                        Count = ViolationsCount.Count,
                        DayOfWeek = ViolationsCount.DayOfWeekName,
                        ViolationHour = ViolationsCount.ViolationHour
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
