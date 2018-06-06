using System;
using System.Linq;
using System.Threading;
using LaRutaDelSoftware.DataAccess.Interfaces;
using LaRutaDelSoftware.DomainEntities;

namespace LaRutaDelSoftware.BussinessLogic.Services
{
    public class UserService
    {
        private IRepository<User> repositoryUser;

        public UserService(IRepository<User> repositoryUser)
        {
            this.repositoryUser = repositoryUser;
        }

        public void Login(User user, string sessionToken)
        {
            User loginOfUser = repositoryUser.GetAll().Where(l => l.UserName == user.UserName).SingleOrDefault();

            loginOfUser.CurrentSessionToken = sessionToken;
            loginOfUser.SessionStart = DateTime.Now;
            repositoryUser.Update(loginOfUser);
        }

        public void Logout(User user)
        {
            User loginOfUser = repositoryUser.GetAll().Where(l => l.UserName == user.UserName).SingleOrDefault();

            loginOfUser.CurrentSessionToken = null;
            loginOfUser.SessionStart = null;
            repositoryUser.Update(loginOfUser);
        }

        public User GetUser(string userName, string password)
        {
            User user = repositoryUser.GetAll().Where(u => u.UserName == userName && u.Password == password && u.IsActive).SingleOrDefault();

            return user;
        }

        public User GetUser(string sessionToken)
        {
            User loginOfUser = repositoryUser.GetAll().Where(l => l.CurrentSessionToken == sessionToken).SingleOrDefault();

            return loginOfUser;
        }

        public void CreateUser(User newUser)
        {
            repositoryUser.Create(newUser);
        }

        public void UpdateUser(User user)
        {
            repositoryUser.Update(user);
        }
        /// <summary>
        /// Return the current user logged.
        /// </summary>
        public User GetCurrentUser()
        {
            User user = this.repositoryUser.GetAll().SingleOrDefault(l => l.CurrentSessionToken == Thread.CurrentPrincipal.Identity.Name);
            return user;
        }

    }
}
