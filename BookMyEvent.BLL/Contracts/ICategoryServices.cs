using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface ICategoryServices
    {
        Task<List<BLEventCategory>> GetAllEventCategories();
        Task<BLEventCategory> AddEventCategory(BLEventCategory category);
        Task<BLEventCategory> UpdateEventCategory(BLEventCategory category);
    }
}
