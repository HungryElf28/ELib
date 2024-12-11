using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Interfaces.Repository;
using Interfaces.Services;
using DomainModel;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private IDbRepos db;
        private IUserSession UserSession;
        public UserService(IDbRepos db, IUserSession session)
        {
            this.db = db;
            this.UserSession = session;
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
        public UserDto GetUser(int id)
        {
            return new UserDto(db.users.GetItem(id));
        }
        public void CreateUser(UserDto us)
        {
            db.users.Create(new users()
            { id = us.id, login = us.login, password = us.password, name = us.name, surname = us.surname, reg_date = us.reg_date, e_mail = us.e_mail, bonuses = us.bonuses });
            Save();
        }
        public void UpdateUser(UserDto us)
        {
            users usr = db.users.GetItem(us.id);
            usr.login = us.login;
            usr.password = us.password;
            usr.name = us.name;
            usr.surname = us.surname;
            usr.reg_date = us.reg_date;
            usr.e_mail = us.e_mail;
            usr.bonuses = us.bonuses;
            db.Save();

        }
        public void DeleteUser(int id)
        {
            users usr = db.users.GetItem(id);
            if(usr != null)
            {
                db.users.Delete(usr.id);
                db.Save();
            }
        }
        public bool ValidateUser(string login, string password)
        {
            var user = db.users.GetAll().FirstOrDefault(u => u.login == login && u.password == password);
            if(user != null)
            {
                UserSession.AuthenticateUser(user);
                return true;
            }
            return false;
        }
        public void RegisterUser(UserDto us)
        {
            var user = new users()
            { id = us.id, login = us.login, password = us.password, name = us.name, surname = us.surname, reg_date = us.reg_date, e_mail = us.e_mail, bonuses = us.bonuses };
            db.users.Create(user);
            Save();
            UserSession.AuthenticateUser(user);
        }
        public bool ValidateLogin(string login)
        {
            return !db.users.GetAll().Any(u => u.login == login);
        }
        public bool ValidateEmail(string email)
        {
            return !db.users.GetAll().Any(u => u.e_mail == email);
        }
    }
}
