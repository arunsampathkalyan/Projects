using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class SMSDTO
    {
        [DataMember]
        public string RecipientNumber { get; set; }
        [DataMember]
        public string MessageBody { get; set; }
        [DataMember]
        public SMSLanguage Language { get; set; }
    }
}
