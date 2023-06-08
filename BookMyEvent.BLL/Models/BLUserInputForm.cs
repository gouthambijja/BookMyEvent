using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLUserInputForm
    {
        public Guid UserInputFormId { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }
    }
}
