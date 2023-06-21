using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using db.Models;

namespace BookMyEvent.DLL.Contracts
{
    /// <summary>
    /// This interface is used to perform database operations on Organisation Model
    /// </summary>
    public interface IOrganisationRepository
    {
       /// <summary>
       /// Method to add new organisation
       /// </summary>
       /// <param name="organisation">Organisation Object Model </param>
       /// <returns>
       /// Returns a tuple of Organisation Model Object, bool IsSuccessfull added or not and a string Message
       /// </returns>
        public Task<(Organisation org, bool IsSuccessfull, string Message)> AddOrganisation(Organisation organisation);

        /// <summary>
        /// Method to get Organisation details by organisation Id
        /// </summary>
        /// <param name="orgId">Organisation Id</param>
        /// <returns> Organisation Object Model </returns>
        public Task<Organisation?> GetOrganisationById(Guid orgId);

        /// <summary>
        /// Method to get all Active Organisation Models
        /// </summary>
        /// <returns>
        /// Returns a tuple of List of Organisation Model Object and total number of Organisations
        /// </returns>
        public  Task<(List<Organisation>? organisations, int totalOranisations)> GetAllOrganisation(int pageNumber, int pageSize);

        /// <summary>
        /// Method to update an Organisation Model
        /// </summary>
        /// <param name="updatedOrganisation"> Organisation Model Object </param>
        /// <returns> Updated Organisation Model Object </returns>
        public Task<Organisation?> UpdateOrganisation(Organisation updatedOrganisation);

        /// <summary>
        /// Method to delete an Organisation Model
        /// </summary>
        /// <param name="orgId"> Organisation Id </param>
        /// <returns> 
        /// True:if deleted successfully
        /// False: if delete operation fails
        /// </returns>
        public Task<bool> DeleteOrganisationById(Guid orgId);

        /// <summary>
        /// Method to check if Organisation Name is available or not
        /// </summary>
        /// <param name="orgName"></param>
        /// <returns>
        ///  True: if Organisation Name is available
        ///  False: if Organisation Name is not available
        /// </returns>
        public Task<bool> IsOrgNameAvailable(string orgName);

        /// <summary>
        /// Method to check if organisation by its name and get the organisation Id
        /// </summary>
        /// <param name="orgName"></param>
        /// <returns>
        /// (orgId):if Organisation Name is available
        /// (null):if Organisation Name is NOT available
        /// </returns>
        public Task<Guid?> GetOrgIdByName(string orgName);

        /// <summary>
        /// Method to toggle IsActive property of Organisation Model
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>
        /// Returns bool is operation successful or not 
        /// </returns>
        public Task<bool> ToggleIsActive(Guid orgId);


    }
}
