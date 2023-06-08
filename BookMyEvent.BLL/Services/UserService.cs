using AutoMapper;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Services
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepositoryDal { get; set; }
        public UserService(IUserRepository userRepositoryDal)
        {
            this.UserRepositoryDal = userRepositoryDal;
        }
        public async Task<(bool IsUserAdded, string Message)> AddUser(BLUser user)
        {
            if (user != null)
            {
                var mapper = Automapper.InitializeAutomapper();
                var User = mapper.Map<BLUser, User>(user);
                return await UserRepositoryDal.AddUser(User);
            }
            return (false, string.Empty);
        }
        public bool BlockUser(Guid UserId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ChangePassword(Guid UserId, string Password)
        {
            if (Password is not null)
            {
                return await UserRepositoryDal.ChangePassword(UserId, Password);
            }
            return false;
        }
        public async Task<(bool IsDeleted, string Message)> DeleteUser(Guid UserId)
        {
            if (!UserId.Equals(string.Empty))
            {
                return await UserRepositoryDal.DeleteUser(UserId);
            }
            return (false, "Bll Error");
        }
        public async Task<BLUser> GetUserByEmail(string Email)
        {
            User user = await UserRepositoryDal.GetUserByEmail(Email);
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<User, BLUser>(user);
        }
        public async Task<BLUser> GetUserById(Guid UserId)
        {
            User user = await UserRepositoryDal.GetUserById(UserId);
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<User, BLUser>(user);
        }
        public async Task<List<BLUser>> GetUsers()
        {
            List<User> UsersList = await UserRepositoryDal.GetAllUsers();
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<User>, List<BLUser>>(UsersList);
        }
        public Task<int> Login(BLLoginModel login)
        {
            throw new NotImplementedException();
        }
        public Task<BLUser> ToggleIsActiveById(Guid Id)
        {
            throw new NotImplementedException();
        }
        public Task<BLUser> UpdateUser(BLUser user)
        {
            throw new NotImplementedException();
        }
        public Task<(User, string Message)> UpdateUser(User _user)
        {
            throw new NotImplementedException();
        }
    }
}
