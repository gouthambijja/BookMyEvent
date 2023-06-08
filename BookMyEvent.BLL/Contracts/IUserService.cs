using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface IUserService
    {
        Task<(bool IsUserAdded, string Message)> AddUser(BLUser user);
        Task<int> Login(BLLoginModel login);
        Task<BLUser> UpdateUser(BLUser user);
        Task<(bool IsDeleted, string Message)> DeleteUser(Guid UserId);
        Task<bool> ChangePassword(Guid UserId, string Password);
        bool BlockUser(Guid UserId);
        Task<List<BLUser>> GetUsers();
        Task<BLUser> GetUserByEmail(string Email);
        Task<BLUser> GetUserById(Guid UserID);
        Task<BLUser> ToggleIsActiveById(Guid Id);
        Task<(User, string Message)> UpdateUser(User _user);
    }
}
