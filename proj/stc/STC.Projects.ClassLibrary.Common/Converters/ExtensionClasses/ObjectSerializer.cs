using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STC.Projects.ClassLibrary.Common.ExtensionClasses
{
    public static class ObjectSerializer
    {
        public static string SerializeObject<T>(this T Object)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Object.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, Object);
                return textWriter.ToString();
            }
        }

        public static T DeserializeObject<T>(this string XmlText)
        {
            try
            {
                var stringReader = new StringReader(XmlText);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

    }
}
