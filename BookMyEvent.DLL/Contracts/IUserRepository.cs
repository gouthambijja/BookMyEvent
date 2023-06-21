using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    public interface IUserRepository
    {
        /// <summary>
        /// An Asynchronous method that adds a new user to the database
        /// </summary>
        /// <param name="_user">
        /// Takes the User object as input
        /// </param>
        /// <returns>
        /// Returns a tuple of bool to say if User is added successfully or not and a string message
        /// </returns>
        Task<(bool IsUserRegistered, string Message)> AddUser(User _user);

        /// <summary>
        /// An Asynchronous method that gets a user by its Id
        /// </summary>
        /// <param name="Id">
        /// Takes the Id of the user as input as Guid type
        /// </param>
        /// <returns>
        /// Returns a User object
        /// </returns>
        Task<User> GetUserById(Guid Id);

        /// <summary>
        /// An Asynchronous method that gets a user by its Email
        /// </summary>
        /// <param name="email">
        /// Takes the Email of the user as input as string type
        /// </param>
        /// <returns>
        /// Returns a User object
        /// </returns>
        Task<User> GetUserByEmail(string email);

        /// <summary>
        /// An Asynchronous method that updates a user
        /// </summary>
        /// <param name="_user">
        /// Takes the User object as input
        /// </param>
        /// <returns>
        /// Returns a tuple of User object and a string message
        /// </returns>
        Task<(User, string Message)> UpdateUser(User _user);

        /// <summary>
        /// An Asynchronous method that deletes a user
        /// </summary>
        /// <param name="id">
        /// Takes the Id of the user as input as Guid type
        /// </param>
        /// <returns>
        /// Returns a tuples of bool to say if User is deleted successfully or not and a string message
        /// </returns>
        Task<(bool, string Message)> DeleteUser(Guid id);

        /// <summary>
        /// An Asynchronous method that gets all users
        /// </summary>
        /// <param name="Id">
        /// Takes the Id of the user as input as Guid type
        /// </param>
        /// <returns>
        /// Returns a User object
        /// </returns>
        Task<User> ToggleIsActiveById(Guid Id);

        /// <summary>
        /// An Asynchronous method that gets all users
        /// </summary>
        /// <returns>
        /// Returns a list of User objects
        /// </returns>
        Task<List<User>> GetAllUsers();
        /// <summary>
        /// Method Used to Change the User Password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <returns>returns true if Password Changes Successfully or else false</returns>
        Task<bool> ChangePassword(Guid UserId, string Password);
        /// <summary>
        /// Method Used to Login User
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password">Returns Id of the User</param>
        /// <returns></returns>
        Task<Guid> IsUserExists(string Email, string Password);
        /// <summary>
        /// Method to Block User by User ID
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>return the UserID with the Message</returns>
        Task<(Guid UserId, string Message)> BlockUser(Guid UserId);

        Task<bool> IsEmailExists(string Email);
    }
}
