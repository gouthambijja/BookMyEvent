using AutoMapper;
using BookMyEvent.BLL.Utilities;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.DLL.Contracts;
using BookMyEvent.DLL.Repositories;
using db.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BookMyEvent.BLL;
namespace BookMyEvent.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepositoryDal;
        private readonly IAccountCredentialsRepository _accountCredentialsRepository;
        private readonly Mapper mapper;
        public UserService(IUserRepository userRepositoryDal, IAccountCredentialsRepository accountCredentialsRepository)
        {
            this.UserRepositoryDal = userRepositoryDal;
            _accountCredentialsRepository = accountCredentialsRepository;
            mapper = Automapper.InitializeAutomapper();
        }
        public async Task<(bool IsUserAdded, string Message)> AddUser(BLUser user)
        {
            try
            {
                if (user != null)
                {
                    var accountCred = await _accountCredentialsRepository.AddCredential(new AccountCredential { Password = user.Password, UpdatedOn = DateTime.Now });
                    user.AccountCredentialsId = accountCred.AccountCredentialsId;
                    var User = mapper.Map<BLUser, User>(user);
                    return await UserRepositoryDal.AddUser(User);
                }
                return (false, "Error due to user is null");
            }
            catch (Exception ex)
            {
                return (false,ex.Message);
            }
        }
       
        public async Task<(Guid UserId, string Message)> BlockUser(Guid UserId)
        {
            try
            {
                if (!UserId.Equals(string.Empty))
                {
                    return await UserRepositoryDal.BlockUser(UserId);
                }
                return (Guid.Empty, "Error in try Catch");
            }
            catch (Exception ex)
            {
                return (UserId, ex.Message);
            }
        }
        public async Task<bool> ChangePassword(Guid UserId, string Password)
        {
            try
            {
                if (Password is not null)
                {
                    return await UserRepositoryDal.ChangePassword(UserId, Password);
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }
        public async Task<(bool IsDeleted, string Message)> DeleteUser(Guid UserId)
        {
            try
            {
                if (!UserId.Equals(string.Empty))
                {
                    return await UserRepositoryDal.DeleteUser(UserId);
                }
                return (false, "Error in the tryCatch");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<BLUser> GetUserByEmail(string Email)
        {
            try
            {
                if (Email is not null)
                {
                    User user = await UserRepositoryDal.GetUserByEmail(Email);
                    return mapper.Map<User, BLUser>(user);
                }
                return null;
            }
            catch 
            {
                return null;
            }
        }
        public async Task<BLUser> GetUserById(Guid UserId)
        {
            User user = await UserRepositoryDal.GetUserById(UserId);
            return mapper.Map<User, BLUser>(user);
        }
        public async Task<List<BLUser>> GetUsers()
        {
            List<User> UsersList = await UserRepositoryDal.GetAllUsers();
            return mapper.Map<List<User>, List<BLUser>>(UsersList);
        }
        public async Task<Guid> Login(BLLoginModel login)
        {
            if (login is not null)
            {
                return await UserRepositoryDal.IsUserExists(login.Email, login.Password);
            }
            return Guid.Empty;
        }
        public async Task<BLUser> ToggleIsActiveById(Guid Id, Guid BlockedBy)
        {
            if (!Id.Equals(string.Empty))
            {
                User user = await UserRepositoryDal.ToggleIsActiveById(Id, BlockedBy);
                return mapper.Map<User, BLUser>(user);
            }
            return null;
        }
        public async Task<(BLUser, string Message)> UpdateUser(BLUser user)
        {
            if (user is not null)
            {
                User user1 = mapper.Map<BLUser, User>(user);
                var resp = await UserRepositoryDal.UpdateUser(user1);
                return (mapper.Map<BLUser>(resp.Item1), resp.Item2);
            }
            return (null, "Not Updated");
        }

        public async Task<(bool IsUserEmailExists, string Message)> IsUserAvailableWithEmail(string email)
        {
            try
            {
                if (await UserRepositoryDal.IsEmailExists(email))
                {
                    return (true, "Email already exists");
                }
                else
                {
                    return (false, "Email doesn't exists");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<BLUser>> GetFilteredUsers(string name = null, string email = null, string phoneNumber = null, bool? isActive = null)
        {
            try
            {
                Console.WriteLine("i've reached bll get filtered users _________________________________________________________________________________________________________________________________________");
                var users = await UserRepositoryDal.GetFilteredUsers(name, email, phoneNumber, isActive);
                return mapper.Map<List<BLUser>>(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
