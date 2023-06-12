using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    /// <summary>
    /// Contains all the methods of Administration Services
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Method to Add An Administration
        /// </summary>
        /// <param name="secondaryAdmin"></param>
        /// <returns>returns added Admin object</returns>
        Task<BLAdministrator> CreateAdministrator(BLAdministrator secondaryAdmin);
        /// <summary>
        /// Method to Block admin
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns>returns bool</returns>
        Task<bool> BlockAdmin(Guid AdminId);
        /// <summary>
        /// Method to Chanage the Admin Password
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="Password"></param>
        /// <returns>returns true if Changes else false</returns>
        Task<bool> ChangeAdminPassword(Guid AdminID, string Password);
        /// <summary>
        /// Method to Get an Admin 
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns>returns Admin Object</returns>
        Task<BLAdministrator> GetAdminById(Guid AdminId);
        /// <summary>
        /// Method to get the list of All Secondary admins
        /// </summary>
        /// <returns>list of Admins</returns>
        Task<List<BLAdministrator>> GetAllSecondaryAdmins();
        /// <summary>
        /// Method to Delete an admin
        /// </summary>
        /// <param name="Deletedby"></param>
        /// <param name="SecondaryAdminId"></param>
        /// <returns>returns true if deleted else false</returns>
        Task<bool> DeleteAdmin(Guid Deletedby, Guid SecondaryAdminId);

        /// <summary>
        /// Mehtod to Update an Admin
        /// </summary>
        /// <param name="secondaryAdmin"></param>
        /// <returns>returns the Updated Admin</returns>
        Task<BLAdministrator> UpdateAdministrator(BLAdministrator secondaryAdmin);
        /// <summary>
        /// Method to Login Admin
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns>returns the Admin Object</returns>
        Task<BLAdministrator> LoginAdmin(string email,string password,string role);
        /// <summary>
        /// Method to get Admins Created By Specific Admin.
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns>List of Created Admins</returns>
        Task<List<BLAdministrator>> AdminsCreatedByAdmin(Guid AdminId);
    }
}
