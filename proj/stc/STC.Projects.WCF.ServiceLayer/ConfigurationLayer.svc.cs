using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Xsl;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ConfigurationLayer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ConfigurationLayer.svc or ConfigurationLayer.svc.cs at the Solution Explorer and start debugging.
    public class ConfigurationLayer : IConfigurationLayer
    {
        public List<PageDTO> GetAllPages(int UserId)
        {
            return new ConfigurationDAL().GetPagesByUserId(UserId);
        }

        public List<ConfPage> GetAllConfiguredPages()
        {
            return new ConfigurationDAL().GetAllConfiguredPages();
        }

        public List<ConfControls> GetAllControls()
        {
            return new ConfigurationDAL().GetAllControls();
        }

        public List<ConfLayout> GetAllLayouts()
        {
            return new ConfigurationDAL().GetAllLayouts();
        }

        public bool SaveConfiguration(ConfPage Page)
        {
            return new ConfigurationDAL().SaveConfiguration(Page);
        }

        public List<MessageTypeSOPDTO> GetAllMessageTypesSOP()
        {
            return new SopDAL().GetAllMessageTypesSOP();
        }

       public bool SaveSOPConfiguration(List<MessageTypeSOPDTO> messageTypes)
        {
            return new SopDAL().SaveSOPConfiguration(messageTypes);
        }

        public List<SopDTO> GetAllSopSteps()
       {
           return new SopDAL().GetAllSopSteps();
       }

        public List<SopSourcesDTO> GetAllSopSources()
        {
            return new SopDAL().GetAllSopSources();
        }

        public List<MessageTypeSOPDTO> GetAllMessageTypesForSopSource(int eventId, int? priorityId)
        {
            return new SopDAL().GetMessageTypeSOP(eventId, priorityId);
        }
        //public string GetTransformedXML(string XML)
        //{
        //    XslCompiledTransform myXslTransform;
        //    myXslTransform = new XslCompiledTransform();
        //    myXslTransform.Load(@"F:\R&D DEV\TestXSLT\TestXSLT\MappingMapTotest2.xslt");
        //    myXslTransform.Transform(@"F:\R&D DEV\TestXSLT\TestXSLT\test.xml", @"F:\R&D DEV\TestXSLT\TestXSLT\fog.xml");
        //}
    }
}
