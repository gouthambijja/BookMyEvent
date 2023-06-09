using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface IEventServices
    {
        Task<BLEvent> Add(BLEvent bLEvent);
        Task<(bool, string)> Delete(Guid EventId);
        Task<BLEvent> Update(BLEvent bLEvent);
        Task<List<BLEvent>> GetAllActivePublishedEvents();
        //Task<bool> DeleteAll(Guid OrganiserId);

    }
}
