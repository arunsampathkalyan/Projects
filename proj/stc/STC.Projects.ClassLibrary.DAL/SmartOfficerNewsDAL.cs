using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class SmartOfficerNewsDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public bool AddSmartOfficerNews(SmartOfficerNewsDTO news)
        {
            try
            {
                if (news == null)
                    return false;

                var officerNews = new SmartOfficerNew
                {
                    OfficerNewsText = news.OfficerNewsText,
                    OfficerNewsImage = news.OfficerNewsImage,
                    OfficerNewsCreatedBy = news.OfficerNewsCreatedBy,
                    OfficerNewsDate = news.OfficerNewsDate,
                    IsNoticed = false
                };

                officerNews = operationalDataContext.SmartOfficerNews.Add(officerNews);

                return operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);

                return false;
            }
        }

        public List<SmartOfficerNewsDTO> GetSmartOfficerOldNews(DateTime startDate)
        {
            try
            {
                var news = operationalDataContext.SmartOfficerNews.Where(x => x.OfficerNewsDate >= startDate).Select(s => new SmartOfficerNewsDTO
                {
                    OfficerNewsId = s.OfficerNewsId,
                    OfficerNewsText = s.OfficerNewsText,
                    OfficerNewsImage = s.OfficerNewsImage,
                    OfficerNewsCreatedBy = s.OfficerNewsCreatedBy ?? 0,
                    OfficerNewsDate = s.OfficerNewsDate.Value,
                    IsNoticed = s.IsNoticed ?? false
                }).ToList();

                return news;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }
    }
}
