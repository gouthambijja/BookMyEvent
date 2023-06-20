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
        public BLForm BLForm { get; set; }

    }
}
