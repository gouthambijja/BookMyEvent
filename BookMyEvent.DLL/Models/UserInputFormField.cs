using BookMyEvent.DLL.Models;
using System;
using System.Collections.Generic;

namespace db.Models;

public partial class UserInputFormField
{
    public Guid UserInputFormFieldid { get; set; }

    public Guid RegistrationFormFieldId { get; set; }

    public Guid UserInputFormId { get; set; }

    public string Label { get; set; } = null!;

    public string? StringResponse { get; set; }

    public int? NumberResponse { get; set; }

    public DateTime? DateResponse { get; set; }

    public Byte[]? FileResponse { get; set; }

    public byte? FileTypeId { get; set; }

    public virtual RegistrationFormField RegistrationFormField { get; set; } = null!;

    public virtual UserInputForm UserInputForm { get; set; } = null!;
}
