using db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Models
{
    public class FileType
    {
        public byte FileTypeId { get; set; }
        public string FileTypeName { get; set; }

        public virtual ICollection<RegistrationFormField> RegistrationFormFields { get; set; } = new List<RegistrationFormField>();

    }
}
