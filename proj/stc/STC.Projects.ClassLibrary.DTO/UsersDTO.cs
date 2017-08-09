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
    public class UsersDTO
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string FullNameAr { get; set; }
        [DataMember]
        public string FullNameEn { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public int RankId { get; set; }
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public UserRolesDTO Role { get; set; }
        [DataMember]
        public bool IsOwner { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string RankNameAR { get; set; }
        [DataMember]
        public string RankNameEN { get; set; }

        public byte[] EncPassword { get; set; }
        public byte[] Salt { get; set; }
       
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
