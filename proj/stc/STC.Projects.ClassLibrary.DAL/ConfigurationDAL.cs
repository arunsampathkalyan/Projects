using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
namespace STC.Projects.ClassLibrary.DAL
{
    public class ConfigurationDAL
    {
        private STCOperationalDataContext _operationalDataContext = new STCOperationalDataContext();

        public List<PageDTO> GetPagesByUserId(int userId)
        {
            var pagesLst = new List<PageDTO>();

            var pages = _operationalDataContext.ConfigurationViews.Where(x=> x.UserId == userId).ToList(); 
            if(pages != null && pages.Any())
            {
                foreach (var item in pages)
                {
                    bool addPage = true;
                    bool addPlaceHolder = true;
                    var pge = new PageDTO();
                    pge.Layout = new LayoutDTO();
                    pge.Layout.PlaceHolders = new List<PlaceHolderDTO>();
                    pge.PageId = item.PageId;
                    pge.PageName = item.Name;
                    pge.Layout.LayoutXaml = item.Xaml;
                    pge.Layout.LayoutId = item.LayoutId;
                    if(pagesLst.Any(x=> x.PageId == item.PageId))
                    {
                        pge = pagesLst.FirstOrDefault(x => x.PageId == item.PageId);
                        addPage = false;
                    }
                    var placeHolder = new PlaceHolderDTO();
                    placeHolder.PlaceHolderId = item.PlaceHolderID;
                    placeHolder.PlaceHolderName = item.PlaceHolderName;
                    placeHolder.UserControl = new UserControlDTO();
                    placeHolder.UserControl.UserControlId = item.UserControlId;
                    placeHolder.UserControl.UserControlName = item.UserControlName;
                    placeHolder.UserControl.MessageTypes = new List<string>();
                    placeHolder.UserControl.PublishMessages = new List<PublisherMessagesDTO>();
                    if(pge.Layout.PlaceHolders.Any(x=> x.PlaceHolderId == item.PlaceHolderID))
                    {
                        placeHolder = pge.Layout.PlaceHolders.FirstOrDefault(x=> x.PlaceHolderId == item.PlaceHolderID);
                        addPlaceHolder = false;
                    }
                    if (item.MessageTypeId > -1 && !placeHolder.UserControl.MessageTypes.Any(x => x == item.SubscriberMessage))
                    {
                        placeHolder.UserControl.MessageTypes.Add(item.SubscriberMessage);
                    }
                    if (item.PublisherMessageTypeId > -1 && !placeHolder.UserControl.PublishMessages.Any(x => x.MessageType == item.PublisherMessage))
                    {
                        placeHolder.UserControl.PublishMessages.Add(new PublisherMessagesDTO() { MessageExpression = item.PublisherExpression, MessageType = item.PublisherMessage });
                    }
                    if(addPlaceHolder)
                        pge.Layout.PlaceHolders.Add(placeHolder);
                    if (addPage)
                        pagesLst.Add(pge);
                   
                }
                
            }
            return pagesLst;
        }

        public List<ConfPage> GetAllConfiguredPages()
        {
            var list = new List<ConfPage>();
            var pages = _operationalDataContext.ConfigurationViews.ToList();
            if(pages != null && pages.Any())
            {
                foreach (var item in pages)
                {
                    bool addPage = true;
                    bool addDetails = true;
                    var pge = new ConfPage();
                    pge.PageId = (int)item.PageId;
                    pge.PageName = item.Name;
                    pge.LayoutId = item.LayoutId;
                    pge.PageDetails = new ObservableCollection<ConfPageDetails>();
                    if (list.Any(x => x.PageId == item.PageId))
                    {
                        pge = list.FirstOrDefault(x => x.PageId == item.PageId);
                        addPage = false;
                    }
                    var pgeDetail = new ConfPageDetails();
                    pgeDetail.PlaceHolderLayoutId = (int)item.LayoutPlaceHolderId;
                    pgeDetail.UserControl = new ConfPageDetailsControl();
                    pgeDetail.UserControl.UserControlId = item.UserControlId;
                    pgeDetail.UserControl.UserControlName = item.UserControlName;
                    pgeDetail.PlaceHolderName = item.PlaceHolderName;
                    if(pge.PageDetails.Any(x=> x.PlaceHolderLayoutId == (int)item.LayoutPlaceHolderId))
                    {
                        pgeDetail = pge.PageDetails.FirstOrDefault(x => x.PlaceHolderLayoutId == (int)item.LayoutPlaceHolderId);
                        addDetails = false;
                    }

                    if (item.MessageTypeId > -1 && !pgeDetail.UserControl.SubscriberMessages.Any(x => x.UserControlMessageTypeId == item.MessageTypeId))
                    {
                        pgeDetail.UserControl.SubscriberMessages.Add(new MessageTypes() { UserControlMessageTypeId = item.MessageTypeId,MessageTypeName = item.SubscriberMessage, IsSelected = true});
                    }
                    if (item.PublisherMessageTypeId > -1 && !pgeDetail.UserControl.PublisherMessages.Any(x => x.UserControlMessageTypeId == item.PublisherMessageTypeId))
                    {
                        pgeDetail.UserControl.PublisherMessages.Add(new MessageTypes() { UserControlMessageTypeId = item.PublisherMessageTypeId, MessageTypeName = item.PublisherMessage, IsSelected = true,Expression = item.PublisherExpression });
                    }
                    if (addDetails)
                        pge.PageDetails.Add(pgeDetail);
                    if (addPage)
                        list.Add(pge);
                }
            }
            return list;
        }

        public List<ConfLayout> GetAllLayouts()
        {
            var layouts = _operationalDataContext.Conf_Layout.Include(x => x.Conf_LayoutsPlaceHolders).ToList();
            var allLayouts = new List<ConfLayout>();
            if(layouts != null && layouts.Any())
            {
                foreach (var item in layouts)
                {
                    var layout = new ConfLayout();
                    layout.LayoutId = item.LayoutId;
                    layout.LayoutName = item.LayoutName;
                    layout.PlaceHolders = new ObservableCollection<ConfPlaceHolder>();
                    foreach (var place in item.Conf_LayoutsPlaceHolders)
                    {
                        layout.PlaceHolders.Add(new ConfPlaceHolder() { LayoutPlaceHolderId = (int)place.LayoutPlaceHolderId, PlaceHolderId = place.PlaceHolderID, PlaceHolderName = place.Conf_PlaceHolder.PlaceHolderName });
                    }
                    allLayouts.Add(layout);
                }
            }
            return allLayouts;
        }

        public List<ConfControls> GetAllControls()
        {
            var userControls = _operationalDataContext.Conf_UserControl.Include(x => x.Conf_UserControlMessage).Include(x=> x.Conf_UserControlPublisherMessage).ToList();
            var allControls = new List<ConfControls>();
            if (userControls != null && userControls.Any())
            {
                foreach (var item in userControls)
                {
                    var control = new ConfControls();
                    control.ControlId = item.UserControlId;
                    control.UserControlName = item.UserControlName;
                    control.SubscriberMessages = new ObservableCollection<MessageTypes>();
                    control.PublisherMessages = new ObservableCollection<MessageTypes>();
                    foreach (var msg in item.Conf_UserControlMessage)
                    {
                        control.SubscriberMessages.Add(new MessageTypes() { UserControlMessageTypeId = msg.UserControlMessageId,MessageTypeId = msg.MessageTypeId,MessageTypeName = msg.Conf_MessageType.MessageTypeName,IsSelected = true});
                    }
                    foreach (var msg in item.Conf_UserControlPublisherMessage)
                    {
                        control.PublisherMessages.Add(new MessageTypes() { UserControlMessageTypeId = msg.UserControlPublisherMessageId, MessageTypeId = msg.MessageTypeId, MessageTypeName = msg.Conf_MessageType.MessageTypeName, IsSelected = true });
                    }
                    allControls.Add(control);
                }
            }
            return allControls;
        }

        public bool SaveConfiguration(ConfPage page)
        {
            var pageEntity = new Conf_Pages();
            if (page.PageId.HasValue)
            {
                pageEntity = _operationalDataContext.Conf_Pages.FirstOrDefault(x => x.PageId == page.PageId.Value);
            }
            if(pageEntity != null)
            {
                pageEntity.Name = page.PageName;
                if(pageEntity.Conf_UsersPages == null)
                {
                    pageEntity.Conf_UsersPages = new Collection<Conf_UsersPages>();
                }
                if (pageEntity.Conf_UsersPages.FirstOrDefault(x => x.UserId == 1) == null)
                    pageEntity.Conf_UsersPages.Add(new Conf_UsersPages { UserId = 1 });
                if (page.PageId.HasValue)
                {
                    var oldDetails = _operationalDataContext.Conf_PageDetails.Where(x => x.PageId == page.PageId.Value);
                    var oldSubsMessages = _operationalDataContext.Conf_ControlPageMessages.Where(x=> oldDetails.Any(y=> y.PageDetailsId == x.PageDetailsId));
                    var oldPublishMessages = _operationalDataContext.Conf_ControlPagePublisherMessages.Where(x => oldDetails.Any(y => y.PageDetailsId == x.PageDetailsId));
                    _operationalDataContext.Conf_ControlPageMessages.RemoveRange(oldSubsMessages);
                    _operationalDataContext.Conf_ControlPagePublisherMessages.RemoveRange(oldPublishMessages);
                    _operationalDataContext.Conf_PageDetails.RemoveRange(oldDetails);
                }
                pageEntity.Conf_PageDetails = new Collection<Conf_PageDetails>();
                
                foreach (var item in page.PageDetails)
                {
                    var pageDetail = new Conf_PageDetails();
                    pageDetail.UserControlId = item.UserControl.UserControlId;
                    pageDetail.LayoutPlaceHolderId = item.PlaceHolderLayoutId;
                    pageDetail.Conf_ControlPageMessages = new Collection<Conf_ControlPageMessages>();
                    pageDetail.Conf_ControlPagePublisherMessages = new Collection<Conf_ControlPagePublisherMessages>();
                    if (item.UserControl.SubscriberMessages != null && item.UserControl.SubscriberMessages.Any(x => x.IsSelected))
                    {
                        foreach (var msg in item.UserControl.SubscriberMessages.Where(x=> x.IsSelected))
                        {
                            pageDetail.Conf_ControlPageMessages.Add(new Conf_ControlPageMessages() { UserControlMessageId = msg.UserControlMessageTypeId });
                        }
                    }
                    if(item.UserControl.PublisherMessages != null && item.UserControl.PublisherMessages.Any(x=> x.IsSelected))
                    {
                        foreach (var msg in item.UserControl.PublisherMessages.Where(x => x.IsSelected))
                        {
                            pageDetail.Conf_ControlPagePublisherMessages.Add(new Conf_ControlPagePublisherMessages() { UserControlPublisherMessageId = msg.UserControlMessageTypeId, PublisherExpression = msg.Expression });
                        }
                    }
                    pageEntity.Conf_PageDetails.Add(pageDetail);
                    if (!page.PageId.HasValue)
                        _operationalDataContext.Conf_Pages.Add(pageEntity);
                }
            }
            var count = _operationalDataContext.SaveChanges();
            return count > 0;
        }

        public MessageTypeConvertOutput GetTransformOutput(int generalType, string xml)
        {
            var t = _operationalDataContext.TransformXMLs.Include(x=> x.Conf_MessageType).Where(x => x.GeneralTypeId == generalType).FirstOrDefault();
            if(t != null && t.Conf_MessageType != null)
            {
                var xslT = t.XSLTMessage;
                var messageTypeName = t.Conf_MessageType.MessageTypeName;
                XslCompiledTransform myXslTransform;
                myXslTransform = new XslCompiledTransform();
                var xmlReader = new StringReader(xml);
                var xsltReader = new StringReader(xslT);
                var xslTLoader = XmlReader.Create(xsltReader);
                var xmlLoader = XmlReader.Create(xmlReader);
                StringBuilder outputXml = new StringBuilder();
                var xmlWriter = XmlWriter.Create(outputXml);
                myXslTransform.Load(xslTLoader);
                myXslTransform.Transform(xmlLoader, xmlWriter);
                if(outputXml != null && outputXml.Length > 0)
                {
                    return new MessageTypeConvertOutput
                    {
                        MessageName = messageTypeName,
                        NewXML = outputXml.ToString()
                    };
                }
            }
            return null;
        }
    }
}
