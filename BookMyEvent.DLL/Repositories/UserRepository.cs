    using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
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
                return null;
            }

        }
        public async Task<bool> IsEmailExists(string Email)
        {
            try
            {
                var user = await _db.Users.Where(a => a.Email == Email).FirstOrDefaultAsync();
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    

        public async Task<User> GetUserById(Guid Id)
        {
            try
            {
               var user =  await _db.Users.Where(e => e.UserId == Id).FirstAsync();
                user.GoogleId = "";
                user.AccountCredentialsId = null;
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> ToggleIsActiveById(Guid Id, Guid blockedBy)
        {
            try
            {
                var user = await _db.Users.FindAsync(Id);
                if (user != null)
                {
                    if (user.IsActive == true)
                        user.DeletedBy = blockedBy;
                    else
                        user.DeletedBy = null;
                    user.IsActive = !user.IsActive;
                    user.UpdatedOn = DateTime.Now;

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
                    user.UserAddress = _user.UserAddress;
                    user.UpdatedOn = DateTime.Now;
                    user.IsActive = _user.IsActive;
                    await _db.SaveChangesAsync();
                    await _db.Entry(_user).GetDatabaseValuesAsync();

                    return (_user, "User Updated Successfully");
                }
                else
                {
                    return (null, "User Not Found");
                }
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
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
            var user = await _db.Users.Where(e =>e.Email == Email).FirstAsync();
            if (user is not null)
            {
                var password = await _db.AccountCredentials.Where(e => e.Password == Password).FirstAsync();
                if (password != null)
                {
                    return user.UserId;
                }
            }
            return Guid.Empty;
        }
        public async Task<(Guid UserId,string Message)> BlockUser(Guid UserId)
        {
            var user = await _db.Users.FindAsync(UserId);
            if (user is not null)
            {
                user.IsActive = false;
                await _db.SaveChangesAsync();
                return (user.UserId,"User Blocked");
            }
            return (UserId,"User Not Blocked");
        }

        public async Task<List<User>> GetFilteredUsers(string name = null, string email = null, string phoneNumber = null, bool? isActive = null)
        {
            try
            {
                Console.WriteLine("I've reached into the user repository---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                var users = await _db.Users.ToListAsync();
                if (name != null)
                {
                    users = users.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
                }
                if (email != null)
                {
                    users = users.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
                }
                if (phoneNumber != null)
                {
                    users = users.Where(x => x.PhoneNumber.ToLower().Contains(phoneNumber.ToLower())).ToList();
                }
                if (isActive != null)
                {
                    users = users.Where(x => x.IsActive == isActive).ToList();
                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
    }
}
