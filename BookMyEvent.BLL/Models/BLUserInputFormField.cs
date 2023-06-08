using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLUserInputFormField
    {
        public Guid UserInputFormFieldid { get; set; }

        public Guid RegistrationFormFieldId { get; set; }

        public Guid UserInputFormId { get; set; }

        public string Label { get; set; } = null!;

        public string? StringResponse { get; set; }

        public int? NumberResponse { get; set; }

        public DateTime? DateResponse { get; set; }
    }
}
