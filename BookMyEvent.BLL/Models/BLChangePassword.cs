using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLChangePassword
    {
        public Guid AdminId { get; set; }
        public string Password { get; set; }    
    }
}
