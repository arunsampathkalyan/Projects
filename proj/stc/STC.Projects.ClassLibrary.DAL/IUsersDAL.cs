using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.DAL
{
  public  interface IUsersDAL
    {
        List<UsersDTO> GetUsersList();
        UsersDTO GetUserById(int id);
        bool AddUser(string userPassword, UsersDTO userData);
        bool UpdateUser( UsersDTO user);
        bool DeleteUser(int id);
        bool UpdatePassword(int id, string oldPassword, string newPassword);
        string ResetPassword(string userName);
        UsersDTO Login(string username, string password);
    }
}
