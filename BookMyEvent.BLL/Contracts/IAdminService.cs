using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface IAdminService
    {
        Task<BLAdministrator> AddSecondaryAdministrator(BLAdministrator secondaryAdmin);
        Task<BLAdministrator> DeleteSecondaryAdministrator(Guid AdminId);
        Task<BLAdministrator> UpdateAdministrator(BLAdministrator secondaryAdmin);
        Task<bool> ChangeAdminPassword(string Password);
        Task<List<BLAdministrator>> GetAllAdmins();
        Task<bool> BlockAdmin(Guid AdminId);
    }
}
