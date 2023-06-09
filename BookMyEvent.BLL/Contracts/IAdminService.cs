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
        Task<BLAdministrator> AddSecondaryAdministrator(BLAdministrator secondaryAdmin);
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
        /// Method To get list of Organization Owners
        /// </summary>
        /// <returns>list of Org Owners</returns>
        Task<List<BLAdministrator>> GetAllOrganizationOwners();
        /// <summary>
        /// Method to Get all the Secondary Owners of a Particular Organization
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns>List of secondary owners</returns>
        Task<List<BLAdministrator>> GetAllSecondaryOrganizationOwners(Guid OrgId);
        /// <summary>
        /// Method to Get all the Requests of Organization Owners
        /// </summary>
        /// <returns>List of Requests of Owners</returns>
        Task<List<BLAdministrator>> GetOrganizationPrimaryOwnersRequests();
        /// <summary>
        /// Method to get the requests of all the peers in an Organization
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns> list of all the requested peers</returns>
        Task<List<BLAdministrator>> GetAllPeerRequests(Guid OrgId);
        /// <summary>
        /// Method to Get ll the peers in an Organization
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns>List of peer in an organization</returns>
        Task<List<BLAdministrator>> GetAllPeers(Guid OrgId);
        /// <summary>
        /// Method to Delete an admin
        /// </summary>
        /// <param name="Deletedby"></param>
        /// <param name="SecondaryAdminId"></param>
        /// <returns>returns true if deleted else false</returns>
        Task<bool> DeleteAdmin(Guid Deletedby, Guid SecondaryAdminId);
        /// <summary>
        /// Method to reject a request
        /// </summary>
        /// <param name="Rejectedby"></param>
        /// <param name="AdminId"></param>
        /// <returns>True if rejected else false</returns>
        Task<bool> RejectAdminRequest(Guid Rejectedby, Guid AdminId);
        /// <summary>
        /// Method to Delete the Organization
        /// </summary>
        /// <param name="Deletedby"></param>
        /// <param name="OrgId"></param>
        /// <returns>true if deleted else false</returns>
        Task<bool> DeleteOrganizationAdmins(Guid Deletedby, Guid OrgId);
        /// <summary>
        /// Mehtod to Update an Admin
        /// </summary>
        /// <param name="secondaryAdmin"></param>
        /// <returns>returns the Updated Admin</returns>
        Task<BLAdministrator> UpdateAdministrator(BLAdministrator secondaryAdmin);
    }
}
