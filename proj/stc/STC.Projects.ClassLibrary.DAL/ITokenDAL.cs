using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public interface ITokenDAL
    {
        TokenDto GetAuthenticationToken(int userId);
        TokenDto GenerateToken(int userId);
        bool ValidateToken(string tokenId);
        bool Kill(string tokenId);
        bool DeleteByUserId(int userId);
        UserRole GetRole(string token);
        IEnumerable<TokenDto> GetAllTokens();
        PublicUserDTO GetUserByToken(string token);
    }
}
