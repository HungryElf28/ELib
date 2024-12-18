using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Services;
using DomainModel;
using DTO;
using Interfaces.Repository;

namespace BLL.Services
{
    public class UserSession : IUserSession
    {
        private IDbRepos db;
        public UserDto CurrentUser { get; set; }
        public bool IsAuthenticated { get; set; }
        public UserSession(IDbRepos db)
        {
            this.db = db;
        }

        public void ClearSession()
        {
            CurrentUser = new UserDto();
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
            db.userTariff.RemoveExpiredTariffs(CurrentUser.id);
            db.offline.DeleteExpiredBooks(CurrentUser.id);
        }
        public void UpdateUser(UserDto user)
        {
            var CurrUser = db.users.GetAll().FirstOrDefault(u => u.id == user.id);
            CurrUser.login = user.login;
            CurrUser.name = user.name;
            CurrUser.surname = user.surname;
            CurrUser.e_mail = user.e_mail;
            db.users.Update(CurrUser);
            db.Save();
            CurrentUser = user;
        }
        public void SpareBonuses(int bn)
        {
            var CurrUser = db.users.GetAll().FirstOrDefault(u => u.id == CurrentUser.id);
            CurrUser.bonuses -= bn;
            db.users.Update(CurrUser);
            db.Save();
            CurrentUser = new UserDto(CurrUser);
        }
        public bool ValidateLogin( int id, string login)
        {
            return !db.users.GetAll().Any(u => u.login == login && u.id != id);
        }
        public bool ValidateEmail(int id, string email)
        {
            return !db.users.GetAll().Any(u => u.e_mail == email && u.id != id);
        }
    }
}
