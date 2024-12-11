using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Services;
using DomainModel;
using DTO;

namespace BLL.Services
{
    public class UserSession : IUserSession
    {
        public UserDto CurrentUser { get; set; }
        public bool IsAuthenticated { get; set; }

        public void ClearSession()
        {
            CurrentUser.id = 0;
            CurrentUser.login = "Guest";
            CurrentUser.password = string.Empty;
            CurrentUser.name = "Guest";
            CurrentUser.surname = "Guestson";
            CurrentUser.reg_date = DateTime.MinValue;
            CurrentUser.e_mail = string.Empty;
            CurrentUser.password = string.Empty;
            CurrentUser.bonuses = 0;
            IsAuthenticated = false;
        }
        public void AuthenticateUser(users user)
        {
            CurrentUser = new UserDto(user);
            IsAuthenticated = true;
        }
    }
}
