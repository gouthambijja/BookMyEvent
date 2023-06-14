using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{

    /// <summary>
    /// This interface is used to perform database operations on Administration Model
    /// </summary>
    public interface IAdministrationRepository
    {
        /// <summary>
        /// Method to Add a New Administrator in the Database
        /// 
        /// </summary>
        /// <param name="administrator">  Administration Model Object</param>
        /// <returns>Administration Model Object</returns>
        public Task<Administration?> AddAdministrator(Administration administrator);

        /// <summary>
        /// Method to get Administrator Details By Id
        /// </summary>
        /// <param name="Id">Administration Id</param>
        /// <returns>Administration Model Object</returns>
        public Task<Administration?> GetAdministratorById(Guid Id);

        /// <summary>
        /// Method to get Administrator Details By Email
        /// </summary>
        /// <param name="email">Administration Email</param>
        /// <returns>Administration Object Model</returns>
        public Task<Administration?> GetAdministratorByEmail(string email);

        /// <summary>
        /// Method to get Administrator Details By GoogleId
        /// </summary>
        /// <param name="GoogleId"> GoogleId</param>
        /// <returns>Administration Model Object</returns>
        public Task<Administration?> GetAdministratorByGoogleId(string GoogleId);

        /// <summary>
        /// Method to return all the administrators who are Active and Accepted
        /// </summary>
        /// <returns> List of Administrators who are active and accepted </returns>
        public Task<List<Administration>?> GetAdministrators();
        Task<List<Administration>?> GetSecondaryAdministrators();

        /// <summary>
        /// Method to get all the active Accounts that are accepted by provided Id
        /// </summary>
        /// <param name="Id"> Administration Id</param>
        /// <returns>List of Administrators Accepted by provided account Id </returns>
        public Task<List<Administration>?> GetAcceptedAdministratorsById(Guid Id);

        /// <summary>
        /// Method to get all the active Accounts that are created by provided Id
        /// </summary>
        /// <param name="Id"> Administration Id</param>
        /// <returns>List of Administrators Created by provided account Id </returns>
        public Task<List<Administration>?> GetCreatedAdministratorsById(Guid Id);

        /// <summary>
        /// Method to get all the active Primary Owner Accounts 
        ///  </summary>

        /// <returns> List of Active Primary Owner Accounts  </returns>
        public Task<List<Administration>?> GetPrimaryAdministrators();

        /// <summary>
        /// Method to get All the Secondary Owners of Provided Organisation
        /// </summary>
        /// <param name="OrgId"> Organisation Id </param>
        /// <returns>List of  All the Secondary Owners of Provided Organisation</returns>

        public Task<List<Administration>?> GetSecondaryAdministratorsByOrgId(Guid OrgId);

        /// <summary>
        /// Method to get All the active Peers of an Organisation
        /// </summary>
        /// <param name="OrgId"> Organisation Id </param>
        /// <returns> List of All Active Peers of an Organisation </returns>
        public Task<List<Administration>?> GetPeerAdministratorsByOrgId(Guid OrgId);

        /// <summary>
        /// Method to get All primary administrator requests
        /// </summary>
        /// <returns> List of Primary Administrators whose requests are yet to accept/reject (role id=2, isAccepted=false and isactive=true) </returns>
        public Task<List<Administration>?> GetPrimaryAdministratorRequests();

        /// <summary>
        /// Method to get All peer administrator requests of an Organisation
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns> List of Peer Administrators whose requests are yet to accept/reject (role id=4, isAccepted=false and isactive=true) </returns>
        public Task<List<Administration>?> GetPeerAdministratorRequests(Guid OrgId);

        /// <summary>
        /// Method to Update Administrator Details
        /// </summary>
        /// <param name="updatedAdministrator"> Administration Object Model </param>
        /// <returns> Administration Object Model </returns>
        public Task<Administration?> UpdateAdministrator(Administration updatedAdministrator);

        /// <summary>
        /// Method to delete Administrator permanently from Databse
        /// </summary>
        /// <param name="Id">Administration Id</param>
        /// <returns>
        /// True:If deleted successfully
        /// False:If delete operation fails
        /// </returns>
        public Task<bool> DeleteAdministratorById(Guid Id);

        /// <summary>
        /// Method to toggle IsActive Field of Administration 
        /// </summary>
        /// <param name="Id">Administration Id</param>
        /// <returns>
        /// True:If toggled successfully
        /// False:If toggle operation fails
        /// </returns>
        public Task<bool> ToggleIsActive(Guid Id);

        /// <summary>
        /// Method to update IsAcceptedBy field
        /// Used to Accept a new administrator account request
        /// </summary>
        /// <param name="acceptedByUserId"> Id of administrator who is accepting the Account </param>
        /// <param name="acceptedAccountId"> Id of the administrator whose account need to be accepted </param>
        /// <returns>
        /// True:If updated successfully
        /// False:If update operation fails
        /// </returns>
        public Task<bool> UpdateIsAcceptedAndAcceptedBy(Guid? acceptedByUserId, Guid acceptedAccountId);

        /// <summary>
        /// Method to update rejectedBy field
        /// Used when administrator rejects new administrator account request
        /// </summary>
        /// <param name="rejectedAccountId"> Id of the administrator whose account need to be rejected </param>
        /// <param name="rejectedByUserId"> Id of administrator who is rejecting the Account </param>
        /// <param name="reason"> Reason for rejecting the account </param>
        /// <returns>
        /// True:If updated successfully
        /// False:If update operation fails
        /// </returns>
        public Task<bool> UpdateRejectedByAndIsActive(Guid rejectedAccountId, Guid rejectedByUserId, string reason);

        /// <summary>
        /// Method to Delete Administrator Account
        /// </summary>
        /// <param name="deletedByUserId"> Id of administrator who is deleting the Account </param>
        /// <param name="deletedAccountId"> Id of the administrator whose account need to be deleted </param>
        /// <returns>
        /// True:If deleted successfully
        /// False:If delete operation fails
        /// </returns>
        public Task<bool> UpdateDeletedByAndIsActive(Guid deletedByUserId, Guid deletedAccountId);

        /// <summary>
        /// Method to delete all the Administrators of an Organisation
        /// </summary>
        /// <param name="deletedByUserId"> Id of administrator who is deleting all Accounts</param>
        /// <param name="OrgId"> Id of the Organisation whose account need to be deleted</param>
        /// <returns>
        /// True:If deleted successfully
        /// False:If delete operation fails</returns>
        public Task<bool> DeleteAdministratorsByOrgId(Guid deletedByUserId, Guid OrgId);

        /// <summary>
        /// Method to Change Admin Password
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="Password"></param>
        /// <returns>return true if Password Changes Successfully else false</returns>
        Task<bool> ChangeAdministratorPassword(Guid AdministratorID, string Password);

        /// <summary>
        /// Method to update IsActive field of all the Organisation Organisers
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="updatedBy"
        /// <returns>
        /// Returns bool value indicating whether IsActive field of all the Organisation Organisers is updated or not
        /// </returns>
        Task<bool> UpdateAllOrganisationOrganisersIsActive(Guid orgId, Guid updatedBy);

        /// <summary>
        /// Method to get all the Administrators of an Organisation
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns>
        /// Returns list of all the Administrators of an Organisation
        /// </returns>
        Task<List<Administration>> GetAdministrationsByOrgId(Guid OrgId);

        Task<bool> IsEmailExists(string Email);
    }
}
