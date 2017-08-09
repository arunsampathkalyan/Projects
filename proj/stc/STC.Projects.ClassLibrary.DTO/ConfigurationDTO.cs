using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ConfPage
    {
        public ConfPage()
        {
            PageDetails = new ObservableCollection<ConfPageDetails>();
        }
        [DataMember]
        public string PageName { get; set; }
        [DataMember]
        public int? PageId { get; set; }
        [DataMember]
        public int LayoutId { get; set; }
        [DataMember]
        public ObservableCollection<ConfPageDetails> PageDetails { get; set; }

    }

    [DataContract]
    public class ConfPageDetails
    {
        public ConfPageDetails()
        {
            AllControls = new ObservableCollection<ConfPageDetailsControl>();
            UserControl = new ConfPageDetailsControl();
        }
        [DataMember]
        public int PlaceHolderLayoutId { get; set; }

        [DataMember]
        public string PlaceHolderName { get; set; }

        [DataMember]
        public ObservableCollection<ConfPageDetailsControl> AllControls { get; set; }
        [DataMember]
        public ConfPageDetailsControl UserControl { get; set; }
    }

    [DataContract]
    public class ConfPageDetailsControl
    {
        public ConfPageDetailsControl()
        {
            SubscriberMessages = new ObservableCollection<MessageTypes>();
            PublisherMessages = new ObservableCollection<MessageTypes>();

        }
        [DataMember]
        public int UserControlId { get; set; }
        [DataMember]
        public string UserControlName { get; set; }
        [DataMember]
        public ObservableCollection<MessageTypes> SubscriberMessages { get; set; }
        [DataMember]
        public ObservableCollection<MessageTypes> PublisherMessages { get; set; }
        //public override bool Equals(object obj)
        //{
        //    if (obj == null || !(obj is ConfPageDetailsControl))
        //        return false;

        //    return ((ConfPageDetailsControl)obj).UserControlId == this.UserControlId;
        //}

    }

    [DataContract]
    public class ConfLayout
    {
        public ConfLayout()
        {
            PlaceHolders = new ObservableCollection<ConfPlaceHolder>();
        }
        [DataMember]
        public int LayoutId { get; set; }
        [DataMember]
        public string LayoutName { get; set; }
        [DataMember]
        public ObservableCollection<ConfPlaceHolder> PlaceHolders { get; set; }
    }

    [DataContract]
    public class ConfPlaceHolder
    {
        [DataMember]
        public int LayoutPlaceHolderId { get; set; }
        [DataMember]
        public int PlaceHolderId { get; set; }
        [DataMember]
        public string PlaceHolderName { get; set; }
    }

    [DataContract]
    public class ConfControls
    {
        public ConfControls()
        {
            SubscriberMessages = new ObservableCollection<MessageTypes>();
            PublisherMessages = new ObservableCollection<MessageTypes>();
        }
        [DataMember]
        public int ControlId { get; set; }
        [DataMember]
        public string UserControlName { get; set; }
        [DataMember]
        public ObservableCollection<MessageTypes> SubscriberMessages { get; set; }
        [DataMember]
        public ObservableCollection<MessageTypes> PublisherMessages { get; set; }

    }

    [DataContract]
    public class MessageTypes
    {
        public MessageTypes()
        {
            Expression = "1 = 1";
        }
        [DataMember]
        public int MessageTypeId { get; set; }
        [DataMember]
        public int UserControlMessageTypeId { get; set; }
        [DataMember]
        public string MessageTypeName { get; set; }
        [DataMember]
        public bool IsSelected { get; set; }
        [DataMember]

        public string Expression { get; set; }
    }

    [DataContract]
    public class MessageTypeConvertOutput
    {
        [DataMember]
        public string NewXML { get; set; }
        [DataMember]
        public string MessageName { get; set; }
    }

    [DataContract]
    public class TransformXMLDTO
    {
        [DataMember]
        public int GeneralTypeId { get; set; }
        [DataMember]
        public int ConfMessageId { get; set; }
        [DataMember]
        public string ConfMessageName { get; set; }
        [DataMember]
        public string XSLTMessage { get; set; }

    }
    [DataContract]
    public class SopDTO
    {
        [DataMember]
        public int SOPId { get; set; }
        [DataMember]
        public string SOPContent { get; set; }
        [DataMember]
        public string SOPViewModelType { get; set; }
        [DataMember]
        public string SOPViewDetailsControl { get; set; }
        [DataMember]
        public Nullable<int> SOPViewDetailsMessageId { get; set; }
        [DataMember]
        public Nullable<int> SOPViewListMessageId { get; set; }
    }
    [DataContract]
    public class SopSourcesDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ArabicDescription { get; set; }
        [DataMember]
        public string EnglishDescription { get; set; }
    }
}
