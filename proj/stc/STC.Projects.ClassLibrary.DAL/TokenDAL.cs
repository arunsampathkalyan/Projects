using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using Telerik.Windows.Data;

namespace STC.Projects.ClassLibrary.DAL
{
    public class TokenDAL : ITokenDAL
    {
        private readonly STCOperationalDataContext _dBContext;
        public TokenDAL()
        {
            _dBContext = new STCOperationalDataContext();
        }

        public TokenDto GetAuthenticationToken(int userId)
        {
            return _dBContext.UserTokens.FirstOrDefault(t => t.UserId == userId).ToTokenDto();
        }

        public TokenDto GenerateToken(int userId)
        {
            string authToken = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddDays(
                                              Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            var token = new TokenDto()
            {
                UserId = userId,
                AuthToken = authToken,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };
            //cleans expired tokens form annonymus calls
            foreach (var token1 in _dBContext.UserTokens.ToList().Where(t => t.UserId == userId && t.ExpiresOn < DateTime.Now))
            {
                _dBContext.UserTokens.Remove(token1);
                _dBContext.SaveChanges();
            }
            _dBContext.UserTokens.Add(token.ToUserToken());
            _dBContext.SaveChanges();
            return token;
        }

        public bool ValidateToken(string tokenId)
        {
            var token = _dBContext.UserTokens.ToList().FirstOrDefault(t => t.AuthToken == tokenId && t.ExpiresOn > DateTime.Now);
            if (token != null && !(DateTime.Now > token.ExpiresOn))
            {
                if (token.ExpiresOn != null)
                    token.ExpiresOn = token.ExpiresOn.Value.AddSeconds(
                        Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
                _dBContext.UserTokens.Attach(token);
                _dBContext.Entry(token).State = EntityState.Modified;
                _dBContext.SaveChanges();


                return true;
            }
            return false;
        }

        public bool Kill(string tokenId)
        {
            _dBContext.UserTokens.Remove(_dBContext.UserTokens.FirstOrDefault(t=>t.AuthToken==tokenId));
            _dBContext.SaveChanges();
            var isNotDeleted = _dBContext.UserTokens.Any(x => x.AuthToken == tokenId);
            return !isNotDeleted;
        }
        
        public bool DeleteByUserId(int userId)
        {
            _dBContext.UserTokens.Remove(_dBContext.UserTokens.FirstOrDefault(t => t.UserId == userId));
            _dBContext.SaveChanges();
            var isNotDeleted = _dBContext.UserTokens.Any(x => x.UserId == userId);
            return !isNotDeleted;
        }


        public UserRole GetRole(string token)
        {

            if (!_dBContext.UserTokens.Any(t => t.AuthToken == token))
            {
                return new UserRole();
            }
            var userId=0;
            var userToken = _dBContext.UserTokens.FirstOrDefault(t => t.AuthToken == token);
            if (userToken != null)
            {
                if (userToken.UserId != null) userId = userToken.UserId.Value;
            }
            var user = _dBContext.Users.FirstOrDefault(u=>u.UserId==userId);
            return  user.UserRole ;
        }


        public IEnumerable<TokenDto> GetAllTokens()
        {
            return _dBContext.UserTokens.ToList().ToTokenDtos();
        }


        public PublicUserDTO GetUserByToken(string token)
        {
            var storedToken = _dBContext.UserTokens.FirstOrDefault(t => t.AuthToken == token);
            return storedToken== null ? null : _dBContext.PublicUsers.FirstOrDefault(u => u.UserId == storedToken.UserId).ToPublicUserDto();
        }
    }
}
