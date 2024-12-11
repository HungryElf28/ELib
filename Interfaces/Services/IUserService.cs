using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DTO;

namespace Interfaces.Services
{
    public interface IUserService
    {
        UserDto GetUser(int id);
        void CreateUser(UserDto us);
        void UpdateUser(UserDto us);
        void DeleteUser(int id);
        bool ValidateUser(string login, string password);
        bool ValidateLogin(string login);
        bool ValidateEmail(string email);
        void RegisterUser(UserDto us);
    }
}
