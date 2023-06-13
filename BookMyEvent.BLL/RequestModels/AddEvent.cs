using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.RequestModels
{
    public class AddEvent
    {
        public BLEvent EventDetails { get; set; }
        public List<BLRegistrationFormFields> RegistrationFormFields { get; set; }
        public BLForm EventForm { get; set; }

    }
}
