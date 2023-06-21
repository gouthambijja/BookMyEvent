using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    //Contains All the Bll Methods for the User Service//
    public interface IUserService
    {
        /// <summary>
        /// Method to Add the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns bool and associated message</returns>
        Task<(bool IsUserAdded, string Message)> AddUser(BLUser user);
        /// <summary>
        /// Method to Login User
        /// </summary>
        /// <param name="login"></param>
        /// <returns>returns UserId</returns>
        Task<Guid> Login(BLLoginModel login);
        /// <summary>
        /// Method to Update the User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns User object and a Message</returns>
        Task<(BLUser, string Message)> UpdateUser(BLUser user);
        /// <summary>
        /// Method to Delete the User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>returns bool and associated message</returns>
        Task<(bool IsDeleted, string Message)> DeleteUser(Guid UserId);
        /// <summary>
        /// Method to Change UserPassword
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <returns>returns true if password changes successfully else false</returns>
        Task<bool> ChangePassword(Guid UserId, string Password);
        /// <summary>
        /// Method to Block a particular User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>returns the User Id and Particular Message</returns>
        Task<(Guid UserId,string Message)>BlockUser(Guid UserId);
        /// <summary>
        /// Method to Get all the Users
        /// </summary>
        /// <returns>List of Users</returns>
        Task<List<BLUser>> GetUsers();
        /// <summary>
        /// Method to get User by Email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>returns an User Object</returns>
        Task<BLUser> GetUserByEmail(string Email);
        /// <summary>
        /// Method to Get User By Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>User Object</returns>
        Task<BLUser> GetUserById(Guid UserID);
        /// <summary>
        /// Method to Delete User Account
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>returns User Object</returns>
        Task<BLUser> ToggleIsActiveById(Guid Id);

        Task<(bool IsUserEmailExists, string Message)> IsUserAvailableWithEmail(string email);
    }
}
