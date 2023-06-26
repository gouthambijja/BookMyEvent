using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.RequestModels
{
    public class UserTicketsWithDetails
    {
        public BLTicket ticket;
        public List<BLUserInputFormField> userDetails;
    }
}
