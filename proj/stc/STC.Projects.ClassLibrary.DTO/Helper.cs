using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    public static class Helper
    {
        static public string Serialize(MapNotificationPopUpDTO details)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MapNotificationPopUpDTO));
            var stringwriter = new System.IO.StringWriter();
            serializer.Serialize(stringwriter, details);
            return stringwriter.ToString();
        }

        static public MapNotificationPopUpDTO DesrializeXml(string xml)
        {
            var obj = new MapNotificationPopUpDTO();
            XmlSerializer serializer = new XmlSerializer(typeof(MapNotificationPopUpDTO));
            var stringwriter = new System.IO.StringReader(xml);
            obj = serializer.Deserialize(stringwriter) as MapNotificationPopUpDTO;
            return obj;
        }

        public static int GetSegmentId(DateTime startDate, DateTime date, PeriodCategory schedule)
        {
            if (schedule == PeriodCategory.Daily)
            {
                return (date - startDate).Duration().Days + 1;
            }
            else if (schedule == PeriodCategory.Weekly)
            {
                return ((date - startDate).Duration().Days + 1) / 7 + 1;
            }
            else if (schedule == PeriodCategory.Monthly)
            {
                return ((date - startDate).Duration().Days + 1) / 30 + 1;
            }
            else if (schedule == PeriodCategory.Monthly)
            {
                return ((date - startDate).Duration().Days + 1) / 365 + 1;
            }
            return 1;
        }
    }
}
