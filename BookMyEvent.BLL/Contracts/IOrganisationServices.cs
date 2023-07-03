using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface IOrganisationServices
    {
        /// <summary>
        /// An asynchronous method that gets all the organisations
        /// </summary>
        /// <returns>
        /// Returns a list of BLOrganisation objects
        /// </returns>
        Task<(List<BLOrganisation> bLOrganisations, int totalBLOrganisations)> GetAllOrganisations(int pageNumber, int pageSize);

        /// <summary>
        /// An asynchronous method that gets an organisation by id
        /// </summary>
        /// <param name="organisationId"></param>
        /// <returns>
        /// Returns a BLOrganisation object
        /// </returns>
        Task<BLOrganisation> GetOrganisationById(Guid organisationId);


        /// <summary>
        /// Method to get Organisation Id by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// (orgId):if Organisation Name is available
        /// (null):if Organisation Name is NOT available
        /// </returns>
        Task<Guid?> GetOrgIdByName(string name);
        /// <summary>
        /// An asynchronous method that creates an organisation
        /// </summary>
        /// <param name="organisation"></param>
        /// <returns>
        /// REturns a tuple of BLOrganisation object, bool saying is organisation created successfully or not and a string message
        /// </returns>
        Task<(BLOrganisation org, bool IsSuccessfull, string Message)> CreateOrganisation(BLOrganisation organisation);

        /// <summary>
        /// An asynchronous method that updates an organisation
        /// </summary>
        /// <param name="organisation"></param>
        /// <returns>
        /// Returns a BLOrganisation object
        /// </returns>
        Task<BLOrganisation> UpdateOrganisation(BLOrganisation organisation);

        /// <summary>
        /// An asynchronous method that deletes an organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="blockerId"
        /// <returns>
        /// Returns a tuple of bool saying is organisation deleted successfully or not and a string message
        /// </returns>
        Task<(bool IsOrganisationBlockToggled, string Message)> BlockOrganisation(Guid organisationId, Guid blockerId);

        /// <summary>
        /// An asynchronous method that checks if the organisation name is taken or not
        /// </summary>
        /// <param name="orgName"></param>
        /// <returns>
        /// Returns a bool saying is organisation name taken or not
        /// </returns>
        Task<bool> IsOrganisationNameTaken(string orgName);
        Task<bool> AcceptOrganisation(Guid orgId);
    }
}
