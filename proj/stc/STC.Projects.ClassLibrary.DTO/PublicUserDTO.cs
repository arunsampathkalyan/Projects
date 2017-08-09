using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PublicUserDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public byte[] EncPassword { get; set; }
        [DataMember]
        public byte[] Salt { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string IdentityNumber { get; set; }
        public string Phone { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [DataMember]
        public int NationalityId { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public DateTime Issuedate { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string NotificationToken { get; set; }

        public bool IsAuthentic(string password)
        {
            byte[] storedPassword = this.EncPassword;
            byte[] storedSalt = this.Salt;
            var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt);
            pbkdf2.IterationCount = 1000;
            byte[] computedPassword = pbkdf2.GetBytes(32);
            return storedPassword.SequenceEqual(computedPassword);
        }
    }
}
