using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLRegistrationFormFields
    {
        public Guid RegistrationFormFieldId { get; set; }

        public Guid FormId { get; set; }

        public byte FieldTypeId { get; set; }

        public byte FileTypeId { get; set; }

        public string Lable { get; set; } = null!;

        public string Validations { get; set; } = null!;

        public string? Options { get; set; }

        public bool? IsRequired { get; set; }
    }
}
