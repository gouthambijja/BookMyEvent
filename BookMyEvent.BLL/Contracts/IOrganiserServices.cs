using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface IOrganiserServices
    {

        /// <summary>
        /// An asynchronous method that registers the owner
        /// </summary>
        /// <param name="administrator"></param>
        /// <param name="bLOrganisation"></param>
        /// <returns>
        /// Returns a tuple with boolean value saying isOwner registered successfully or not and a string message
        /// </returns>
        Task<(bool IsSuccessfull, string Message)> RegisterOwner(BLAdministrator owner, BLOrganisation bLOrganisation);

        /// <summary>
        /// An asynchronous method that registers the peer
        /// </summary>
        /// <param name="peer"></param>
        /// <returns>
        /// Returns a tuple with  boolean value saying isPeer registered successfully or not and a string message
        /// </returns>
        Task<(bool IsSuccessfull, string Message)> RegisterPeer(BLAdministrator peer);

        /// <summary>
        /// An asynchronous method that logs in the organiser
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Returns a tuple with Guid value saying is organiser logged in successfully or not, a byte value saying the role of the organiser, a boolean value saying is organiser logged in successfully or not and a string message
        /// </returns>
        Task<(Guid administratorId, byte roleId, bool IsSuccessfull, string Message)> LoginOrganiser(string email, string password);

        /// <summary>
        /// An asynchronous method that creates a secondary owner
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns>
        /// Returns a tuple with boolean value saying is secondary owner created successfully or not, a string message and a BLAdministrator object
        /// </returns>
        Task<(bool IsSuccessfull, string Message, BLAdministrator organiser)> CreateSecondaryOwner(BLAdministrator administrator);

        /// <summary>
        /// An asynchronous method that updates the organiser
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns>
        /// Returns an updated BLAdministrator object
        /// </returns>
        Task<BLAdministrator> UpdateOrganiser(BLAdministrator administrator);

        /// <summary>
        /// An asynchronous method that gets the organiser by id
        /// </summary>
        /// <param name="administratorId"></param>
        /// <returns>
        /// Returns a BLAdministrator object
        /// </returns>
        Task<BLAdministrator> GetOrganiserById(Guid administratorId);

        /// <summary>
        /// An asynchronous method that gets all the organisers who have requested to join the organisation
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>
        /// Returns a list of BLAdministrator objects
        /// </returns>
        Task<List<BLAdministrator>> GetAllRequestedOrganisers(Guid orgId);

        /// <summary>
        /// An asynchronous method that gets all the organisers of the organisation
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>
        /// Returns a list of BLAdministrator objects
        /// </returns>
        Task<List<BLAdministrator>> GetAllOrganisationOrganisers(Guid orgId);

        /// <summary>
        /// An asynchronous method that blocks/unblocks the organiser
        /// </summary>
        /// <param name="administratorId"></param>
        /// <returns>
        /// Returns a tuple with boolean value saying is organiser blocked/unblocked successfully or not and a string message
        /// </returns>
        Task<(bool IsSuccessfull, string Message)> BlockOrganiser(Guid administratorId);

        /// <summary>
        /// An asynchronous method that unblocks the organiser
        /// </summary>
        /// <param name="administratorId"></param>
        /// <param name="acceptedBy"></param>
        /// <returns>
        /// Returns a bool value saying is organiser accepted successfully or not
        /// </returns>
        Task<bool> AcceptOrganiser(Guid administratorId, Guid acceptedBy);

        /// <summary>
        /// An asynchronous method that rejects the organiser
        /// </summary>
        /// <param name="administratorId"></param>
        /// <param name="rejectedBy"></param>
        /// <param name="reason"></param>
        /// <returns>
        /// Returns a bool value saying is organiser rejected successfully or not
        /// </returns>
        Task<bool> RejectOrganiser(Guid administratorId, Guid rejectedBy, string reason);

        /// <summary>
        /// An asynchronous method that checks if the organiser with email is available or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// Returns a tuple with boolean value saying is organiser with email available or not and a string message
        /// </returns>
        Task<(bool IsOrganiserEmailAvailable, string Message)> IsOrganiserAvailableWithEmail(string email);

        /// <summary>
        /// An asynchronous method that blocks all the organisers of the organisation
        /// </summary>
        /// <param name="administratorId"></param>
        /// <returns>
        /// Returns a tuple with boolean value saying is all organisers blocked successfully or not and a string message
        /// </returns>
        Task<(bool IsSuccessfull, string Message)> BlockAllOrganisationOrganisers(Guid administratorId);

        /// <summary>
        /// An asynchronous method to check if the organiser is owner or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Returns a boolean value saying is organiser owner or not
        /// </returns>
        Task<bool> IsOwner(Guid id);

        /// <summary>
        /// An asynchronous method that gets all the requested owners
        /// </summary>
        /// <returns>
        /// Returns a list of BLAdministrator objects
        /// </returns>
        Task<List<BLAdministrator>> GetAllRequestedOwners();

        /// <summary>
        /// An asynchronous method that gets all the owners
        /// </summary>
        /// <returns></returns>
        Task<List<BLAdministrator>> GetAllOwners();
    }
}
