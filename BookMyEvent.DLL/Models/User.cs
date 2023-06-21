using System;
using System.Collections.Generic;

namespace db.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string? GoogleId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public Guid? AccountCredentialsId { get; set; }

    public string? UserAddress { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool? IsActive { get; set; }

    public byte[]? ImgBody { get; set; }

    public string? ImageName { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual AccountCredential? AccountCredentials { get; set; }

    public virtual Administration? DeletedByNavigation { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<UserInputForm> UserInputForms { get; set; } = new List<UserInputForm>();
}
