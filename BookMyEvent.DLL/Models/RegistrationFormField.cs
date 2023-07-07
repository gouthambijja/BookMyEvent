using System;
using System.Collections.Generic;

namespace db.Models;

public partial class RegistrationFormField
{
    public Guid RegistrationFormFieldId { get; set; }

    public Guid FormId { get; set; }

    public byte FieldTypeId { get; set; }

    public string Lable { get; set; } = null!;

    public string Validations { get; set; } = null!;

    public string? Options { get; set; }

    public bool? IsRequired { get; set; }

    public virtual FieldType FieldType { get; set; } = null!;

    public virtual Form Form { get; set; } = null!;

    public virtual ICollection<UserInputFormField> UserInputFormFields { get; set; } = new List<UserInputFormField>();
}
