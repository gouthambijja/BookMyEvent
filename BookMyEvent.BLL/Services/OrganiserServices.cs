using AutoMapper;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Services
{
    public class Organiserservices : IOrganiserServices
    {
        private readonly IAdministrationRepository _administrationRepository;
        private readonly IOrganisationServices _organisationServices;
        private readonly IAccountCredentialsRepository _accountCredentialsRepository;
        private readonly Mapper mapper;

        public Organiserservices(IAdministrationRepository administrationRepository, IOrganisationServices organisationServices, IAccountCredentialsRepository accountCredentialsRepository)
        {
            _administrationRepository = administrationRepository;
            _organisationServices = organisationServices;
            _accountCredentialsRepository = accountCredentialsRepository;
            mapper = Automapper.InitializeAutomapper();
        }
        public async Task<bool> AcceptOrganiser(Guid administratorId, Guid? acceptedBy, byte RoleId, Guid orgId)
        {
            try
            {
                Console.WriteLine(RoleId);
                if (RoleId == 2)
                {
                    var isOrganisationStatusChanged = await _organisationServices.AcceptOrganisation(orgId);
                    if (!isOrganisationStatusChanged)
                    {
                        return false;
                    }
                }
                return await _administrationRepository.UpdateIsAcceptedAndAcceptedBy(acceptedBy, administratorId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<(bool IsSuccessfull, string Message)> BlockAllOrganisationOrganisers(Guid orgId, Guid blockedBy)
        {
            try
            {
                if (await _administrationRepository.UpdateAllOrganisationOrganisersIsActive(orgId, blockedBy))
                {
                    return (true, "All Organisers Blocked");
                }
                else
                {
                    return (false, "All Organisers Not Blocked");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccessfull, string Message)> BlockOrganiser(Guid administratorId, Guid blockedBy)
        {
            try
            {
                if (await _administrationRepository.UpdateDeletedByAndIsActive(blockedBy, administratorId))
                {
                    return (true, "Organiser Blocked");
                }
                else
                {
                    return (false, "Organiser Not Blocked");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccessfull, string Message, BLAdministrator organiser)> CreateSecondaryOwner(BLAdministrator administrator)
        {
            try
            {
                //var (IsAvailable, Message) = await IsOrganiserAvailableWithEmail(administrator.Email);
                //if (IsAvailable)
                //{
                //    return (false, Message, null);
                //}
                //else
                //{  
                //    var passModel = await _accountCredentialsRepository.AddCredential(new AccountCredential { Password = administrator.Password, UpdatedOn = DateTime.Now });
                //    administrator.AccountCredentialsId = passModel.AccountCredentialsId;
                //    Console.WriteLine(administrator.AccountCredentialsId);
                //    Console.WriteLine("this is the cred Id ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                //    if (passModel != null)
                //    {
                //        var newAdministrator = await _administrationRepository.AddAdministrator(mapper.Map<Administration>(administrator));
                //        Console.WriteLine(newAdministrator.AdministratorId);
                //        Console.WriteLine("this is the admin Id ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                //        if (newAdministrator != null)
                //        {
                //            return (true, "Organiser registration successfull", mapper.Map<BLAdministrator>(newAdministrator));
                //        }
                //        else
                //        {
                //            return (false, "Organiser registration unsuccessfull", new BLAdministrator());
                //        }
                //    }
                //    else
                //    {
                //        return (false, "Organiser password is not createded", new BLAdministrator());
                //    }
                //}

                (bool IsAvailable, string Message) = await IsOrganiserAvailableWithEmail(administrator.Email);
                if (IsAvailable)
                {
                    return (false, Message, null);
                }
                else
                {
                    //var newOrg = await _organisationServices.CreateOrganisation(bLOrganisation);
                    //if (newOrg.IsSuccessfull)
                    //{
                    var passModel = await _accountCredentialsRepository.AddCredential(new AccountCredential { Password = administrator.Password, UpdatedOn = DateTime.Now });
                    administrator.AccountCredentialsId = passModel.AccountCredentialsId;
                    Console.WriteLine(administrator.AccountCredentialsId);
                    Console.WriteLine("this is the cred Id ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                    if (passModel != null)
                    {
                        var newAdministrator = await _administrationRepository.AddAdministrator(mapper.Map<Administration>(administrator));
                        Console.WriteLine(newAdministrator.AdministratorId);
                        Console.WriteLine("this is the admin Id ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                        if (newAdministrator != null)
                        {
                            return (true, "Organiser registration successfull", mapper.Map<BLAdministrator>(newAdministrator));
                        }
                        else
                        {
                            return (false, "Organiser registration unsuccessfull", null);
                        }
                    }
                    else
                    {
                        return (false, "organiser password creation  unsuccessfull", null);
                    }

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public async Task<List<BLAdministrator>> GetAllOrganisationOrganisers(Guid orgId)
        {
            try
            {
                var result = await _administrationRepository.GetAdministrationsByOrgId(orgId);
                return mapper.Map<List<BLAdministrator>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BLAdministrator>> GetAllOwners()
        {
            try
            {
                var result = await _administrationRepository.GetPrimaryAdministrators();
                return mapper.Map<List<BLAdministrator>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BLAdministrator>> GetAllRequestedOrganisers(Guid orgId)
        {

            try
            {
                var result = await _administrationRepository.GetPeerAdministratorRequests(orgId);
                return mapper.Map<List<BLAdministrator>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<int> GetNoOfRequestedOrganisers(Guid orgId)
        {

            try
            {
                var result = await _administrationRepository.GetNoOfPeerAdministratorRequests(orgId);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<BLAdministrator>> GetAllRequestedOwners()
        {
            try
            {
                var result = await _administrationRepository.GetPrimaryAdministratorRequests();
                return mapper.Map<List<BLAdministrator>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BLAdministrator> GetOrganiserById(Guid administratorId)
        {
            try
            {
                var result = await _administrationRepository.GetAdministratorById(administratorId);
                return mapper.Map<BLAdministrator>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool IsOrganiserEmailAvailable, string Message)> IsOrganiserAvailableWithEmail(string email)
        {
            try
            {
                if (await _administrationRepository.IsEmailExists(email))
                {
                    return (true, "Email already exists");
                }
                else
                {
                    return (false, "Email doesn't exists");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<bool> IsOwner(Guid id)
        {
            try
            {
                var result = await _administrationRepository.GetAdministratorById(id);
                if (result.RoleId == 2)
                {
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

        public async Task<(Guid administratorId, byte roleId, bool IsSuccessfull, string Message)> LoginOrganiser(string email, string password)
        {
            try
            {
                var result = await _administrationRepository.GetAdministratorByEmail(email);
                if (result != null)
                {
                    if (result.RoleId == 1) return (Guid.Empty, 0, false, "Email doesn't exists");
                    var res = await _accountCredentialsRepository.CheckPassword(result.AccountCredentialsId, password);
                    if (res)
                    {
                        return (result.AdministratorId, result.RoleId, true, "Login Successfull");
                    }
                    else
                    {
                        return (Guid.Empty, 0, false, "Password is incorrect");
                    }
                }
                else
                {
                    return (Guid.Empty, 0, false, "Email doesn't exists");
                }
            }
            catch (Exception ex)
            {
                return (Guid.Empty, 0, false, ex.Message);
            }
        }

        public async Task<(bool IsSuccessfull, string Message)> RegisterOwner(BLAdministrator owner, BLOrganisation bLOrganisation)
        {
            try
            {

                (bool IsAvailable, string Message) = await IsOrganiserAvailableWithEmail(owner.Email);
                if (IsAvailable)
                {
                    return (false, Message);
                }
                else
                {
                    var newOrg = await _organisationServices.CreateOrganisation(bLOrganisation);
                    if (newOrg.IsSuccessfull)
                    {
                        owner.OrganisationId = newOrg.org.OrganisationId;
                        var passModel = await _accountCredentialsRepository.AddCredential(new AccountCredential { Password = owner.Password, UpdatedOn = DateTime.Now });
                        owner.AccountCredentialsId = passModel.AccountCredentialsId;
                        Console.WriteLine(owner.AccountCredentialsId);
                        Console.WriteLine("this is the cred Id ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                        if (passModel != null)
                        {
                            var newAdministrator = await _administrationRepository.AddAdministrator(mapper.Map<Administration>(owner));
                            Console.WriteLine(newAdministrator.AdministratorId);
                            Console.WriteLine("this is the admin Id ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                            if (newAdministrator != null)
                            {
                                return (true, "Organiser registration successfull");
                            }
                            else
                            {
                                return (false, "Organiser registration unsuccessfull");
                            }
                        }
                        else
                        {
                            return (false, "Organiser password creation  unsuccessfull");
                        }
                    }
                    else
                    {
                        return (false, newOrg.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccessfull, string Message)> RegisterPeer(BLAdministrator peer)
        {
            try
            {

                var res = await IsOrganiserAvailableWithEmail(peer.Email);
                if (res.IsOrganiserEmailAvailable)
                {
                    return (false, res.Message);
                }
                else
                {
                    var passModel = await _accountCredentialsRepository.AddCredential(new AccountCredential { Password = peer.Password, UpdatedOn = DateTime.Now });
                    peer.AccountCredentialsId = passModel.AccountCredentialsId;
                    var newAdministrator = await _administrationRepository.AddAdministrator(mapper.Map<Administration>(peer));
                    if (newAdministrator != null)
                    {
                        return (true, "Peer registration successfull");
                    }
                    else
                    {
                        return (false, "Peer registration unsuccessfull");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<bool> RejectOrganiser(Guid administratorId, Guid? rejectedBy, string reason)
        {
            try
            {
                var result = await _administrationRepository.UpdateRejectedByAndIsActive(administratorId, rejectedBy, reason);
                if (result)
                {
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

        public async Task<BLAdministrator> UpdateOrganiser(BLAdministrator administrator)
        {
            try
            {
                var result = await _administrationRepository.UpdateAdministrator(mapper.Map<Administration>(administrator));
                return mapper.Map<BLAdministrator>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
