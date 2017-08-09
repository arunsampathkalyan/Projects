using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class UserControlDTO
    {
        public UserControlDTO()
        {
            PublishMessages = new List<PublisherMessagesDTO>();
        }
        [DataMember]
        public string UserControlName { get; set; }
        [DataMember]
        public int UserControlId { get; set; }
        [DataMember]
        public List<string> MessageTypes { get; set; }
        [DataMember]
        public List<PublisherMessagesDTO> PublishMessages { get; set; }
    }

    [DataContract]
    public class PublisherMessagesDTO
    {
        [DataMember]
        public string MessageType { get; set; }
        [DataMember]
        public string MessageExpression { get; set; }
    }
}
