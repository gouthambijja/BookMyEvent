using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using BookMyEvent.WebApi.Utilities;

namespace BookMyEvent.BLL.Services
{
    public class AdminServices : IAdminService
    {
        private readonly IAdministrationRepository _administrationRepository;
        private readonly IAccountCredentialsRepository _accountCredentialsRepository;
        public AdminServices(IAdministrationRepository administrationRepository,IAccountCredentialsRepository accountCredentialsRepository)
        {
            this._administrationRepository = administrationRepository;
            this._accountCredentialsRepository = accountCredentialsRepository;
        }
        public async Task<BLAdministrator> AddSecondaryAdministrator(BLAdministrator secondaryAdmin)
        {
            if (secondaryAdmin is not null)
            {
                var mapper = Automapper.InitializeAutomapper();
                Administration administration = mapper.Map<Administration>(secondaryAdmin);
                await _administrationRepository.AddAdministrator(administration);
                return secondaryAdmin;
            }
            return new BLAdministrator();
        }

        public async Task<bool> BlockAdmin(Guid AdminId)
        {
            if (!AdminId.Equals(string.Empty))
            {
                return await _administrationRepository.ToggleIsActive(AdminId);
            }
            return false;
        }

        public async Task<bool> ChangeAdminPassword(Guid AdminId, string Password)
        {
            if (AdminId.Equals(string.Empty))
            {
                return await _administrationRepository.ChangeAdminPassword(AdminId, Password);
            }
            return false;
        }
        public async Task<BLAdministrator> GetAdminById(Guid AdminId)
        {
            if (!AdminId.Equals(string.Empty))
            {
                Administration Admin = await _administrationRepository.GetAdministratorById(AdminId);
                var mapper = Automapper.InitializeAutomapper();
                return mapper.Map<Administration, BLAdministrator>(Admin);
            }
            return new BLAdministrator();

        }
        public async Task<List<BLAdministrator>> GetAllSecondaryAdmins()
        {
            List<Administration> ListOfAdmins = await _administrationRepository.GetAdministrators();
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<Administration>, List<BLAdministrator>>(ListOfAdmins);
        }
        public async Task<List<BLAdministrator>> GetAllOrganizationOwners()
        {
            List<Administration> ListOrganizationOwners = await _administrationRepository.GetPrimaryAdministrators();
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<Administration>, List<BLAdministrator>>(ListOrganizationOwners);
        }
        public async Task<List<BLAdministrator>> GetAllSecondaryOrganizationOwners(Guid OrgId)
        {
            List<Administration> ListOrganizationOwners = await _administrationRepository.GetSecondaryAdministratorsByOrgId(OrgId);
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<Administration>, List<BLAdministrator>>(ListOrganizationOwners);
        }
        public async Task<List<BLAdministrator>> GetAllPeers(Guid OrgId)
        {
            List<Administration> Peers = await _administrationRepository.GetPeerAdministratorsByOrgId(OrgId);
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<Administration>, List<BLAdministrator>>(Peers);
        }
        public async Task<List<BLAdministrator>> GetOrganizationPrimaryOwnersRequests()
        {
            List<Administration> SecondaryAdminRequests = await _administrationRepository.GetPrimaryAdministratorRequests();
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<Administration>, List<BLAdministrator>>(SecondaryAdminRequests);
        }
        public async Task<List<BLAdministrator>> GetAllPeerRequests(Guid OrgId)
        {
            List<Administration> PeerRequests = await _administrationRepository.GetPeerAdministratorRequests(OrgId);
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<Administration>, List<BLAdministrator>>(PeerRequests);
        }
        public async Task<bool> DeleteAdmin(Guid Deletedby,Guid SecondaryAdminId)
        {
            if (!SecondaryAdminId.Equals(Guid.Empty))
            {
                return await _administrationRepository.UpdateDeletedByAndIsActive(Deletedby,SecondaryAdminId);
            }
            return false;
        }
        public async Task<bool> RejectAdminRequest(Guid Rejectedby, Guid AdminId)
        {
            if (AdminId.Equals(Guid.Empty))
            {
                return await _administrationRepository.UpdateRejectedByAndIsActive(Rejectedby, AdminId);
            }
            return false;
        }
        public async Task<bool> DeleteOrganizationAdmins(Guid Deletedby, Guid OrgId)
        {
            if (OrgId.Equals(Guid.Empty))
            {
                return await _administrationRepository.DeleteAdministratorsByOrgId(Deletedby, OrgId);   
            }
            return false;
        }
        public async Task<BLAdministrator> UpdateAdministrator(BLAdministrator secondaryAdmin)
        {
            if(secondaryAdmin is not null)
            {
                var mapper = Automapper.InitializeAutomapper();
                Administration Admin = await _administrationRepository.UpdateAdministrator(mapper.Map<BLAdministrator,Administration>(secondaryAdmin));
                return secondaryAdmin;
            }
            return new BLAdministrator();   
        }

        public async Task<BLAdministrator> LoginAdmin(string email,string password,string role)
        {
            try
            {
                Mapper mapper = Automapper.InitializeAutomapper();
                Console.WriteLine(role );
                Console.WriteLine(" ------ " + Roles.Admin.ToString());
                if (role == Roles.Admin.ToString())
                {
                Console.WriteLine("hello");
                    Console.WriteLine(email);

                    Administration? Admin = await _administrationRepository.GetAdministratorByEmail(email);
                    if(await _accountCredentialsRepository.IsValidCredential(Admin.AccountCredentialsId, password))
                    {
                        Console.WriteLine(Admin.PhoneNumber);
                        return mapper.Map<BLAdministrator>(Admin);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
