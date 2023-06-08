using System;
using System.Collections.Generic;

namespace db.Models;

public partial class FieldType
{
    public byte FieldTypeId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<RegistrationFormField> RegistrationFormFields { get; set; } = new List<RegistrationFormField>();
}
