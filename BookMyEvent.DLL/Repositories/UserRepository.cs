using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventManagementSystemTeamZealContext _db;
        public UserRepository(EventManagementSystemTeamZealContext db)
        {
            _db = db;
        }
        public async Task<(bool IsUserRegistered, string Message)> AddUser(User _user)
        {
            try
            {
                _db.Users.Add(_user);
                _db.SaveChanges();
                return (true, "User Registered Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string Message)> DeleteUser(Guid id)
        {
            try
            {
                var user = _db.Users.Where(x => x.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                    return (true, "User Deleted Successfully");
                }
                else
                {
                    return (false, "User Not Found");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _db.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<User> { };
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                return new User();
            }

        }

        public async Task<User> GetUserById(Guid Id)
        {
            try
            {
                return await _db.Users.FindAsync(Id);
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public async Task<User> ToggleIsActiveById(Guid Id)
        {
            try
            {
                var user = await _db.Users.FindAsync(Id);
                if (user != null)
                {
                    user.IsActive = !user.IsActive;
                    _db.SaveChanges();
                    return user;
                }
                else
                {
                    return new User();
                }
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public async Task<(User, string Message)> UpdateUser(User _user)
        {
            try
            {
                var user = _db.Users.Where(x => x.UserId == _user.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.Name = _user.Name;
                    user.Email = _user.Email;
                    user.PhoneNumber = _user.PhoneNumber;
                    //user.Address = _user.Address;
                    user.UpdatedOn = _user.UpdatedOn;
                    user.IsActive = _user.IsActive;
                    _db.SaveChanges();
                    return (_user, "User Updated Successfully");
                }
                else
                {
                    return (new User(), "User Not Found");
                }
            }
            catch (Exception ex)
            {
                return (new User(), ex.Message);
            }
        }
        public async Task<bool> ChangePassword(Guid UserId, string Password)
        {
            try
            {
                var user = await _db.Users.FindAsync(UserId);
                if (user.AccountCredentialsId != null)
                {
                    var credential = await _db.AccountCredentials.FindAsync(user.AccountCredentialsId);
                    credential.Password = Password;
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Guid> IsUserExists(string Email, string Password)
        {
            var user = await _db.Users.FindAsync(Email);
            if (user is not null)
            {
                var password = await _db.AccountCredentials.FindAsync(Password);
                if (password != null)
                {
                    return user.UserId;
                }
            }
            return user.UserId;
        }
    }
}
