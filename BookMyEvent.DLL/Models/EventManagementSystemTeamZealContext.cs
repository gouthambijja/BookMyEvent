using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace db.Models;

public partial class EventManagementSystemTeamZealContext : DbContext
{
    public EventManagementSystemTeamZealContext()
    {
    }

    public EventManagementSystemTeamZealContext(DbContextOptions<EventManagementSystemTeamZealContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountCredential> AccountCredentials { get; set; }

    public virtual DbSet<Administration> Administrations { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCategory> EventCategories { get; set; }

    public virtual DbSet<EventImage> EventImages { get; set; }

    public virtual DbSet<FieldType> FieldTypes { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<Organisation> Organisations { get; set; }

    public virtual DbSet<RegistrationFormField> RegistrationFormFields { get; set; }

    public virtual DbSet<RegistrationStatus> RegistrationStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInputForm> UserInputForms { get; set; }

    public virtual DbSet<UserInputFormField> UserInputFormFields { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountCredential>(entity =>
        {
            entity.HasKey(e => e.AccountCredentialsId).HasName("PK__AccountC__8537A84B61ED7E9E");

            entity.Property(e => e.AccountCredentialsId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Administration>(entity =>
        {
            entity.HasKey(e => e.AdministratorId).HasName("PK__Administ__ACDEFED343AA4E48");

            entity.ToTable("Administration");

            entity.HasIndex(e => e.GoogleId, "Unique_Administration_Google_Id")
                .IsUnique()
                .HasFilter("([GoogleId] IS NOT NULL)");

            entity.Property(e => e.AdministratorId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AdministratorAddress)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.AdministratorName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImageName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsAccepted).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AcceptedByNavigation).WithMany(p => p.InverseAcceptedByNavigation)
                .HasForeignKey(d => d.AcceptedBy)
                .HasConstraintName("FK__Administr__Accep__36B12243");

            entity.HasOne(d => d.AccountCredentials).WithMany(p => p.Administrations)
                .HasForeignKey(d => d.AccountCredentialsId)
                .HasConstraintName("FK_ADMINISTRATION_AccountCredentialsID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Administr__Creat__34C8D9D1");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.InverseDeletedByNavigation)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK__Administr__Delet__3A81B327");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Administrations)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORGANISATIONID");

            entity.HasOne(d => d.RejectedByNavigation).WithMany(p => p.InverseRejectedByNavigation)
                .HasForeignKey(d => d.RejectedBy)
                .HasConstraintName("FK__Administr__Rejec__38996AB5");

            entity.HasOne(d => d.Role).WithMany(p => p.Administrations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLEID");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C81054279AF9");

            entity.Property(e => e.EventId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AvailableSeats).HasComputedColumnSql("([Capacity])", false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EventEndingPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventStartingPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsOffline)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Location)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.RejectedOn).HasColumnType("datetime");
            entity.Property(e => e.RejectedReason).IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AcceptedByNavigation).WithMany(p => p.EventAcceptedByNavigations)
                .HasForeignKey(d => d.AcceptedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_ACCEPTEDBY");

            entity.HasOne(d => d.Category).WithMany(p => p.Events)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_CATEGORYID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EventCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_CREATEDBY");

            entity.HasOne(d => d.Form).WithMany(p => p.Events)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_FORMID");

            entity.HasOne(d => d.Img).WithMany(p => p.Events)
                .HasForeignKey(d => d.ImgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_IMGID");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_ORGANISATIONID");

            entity.HasOne(d => d.RegistrationStatus).WithMany(p => p.Events)
                .HasForeignKey(d => d.RegistrationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_REGISTRATIONSTATUSID");

            entity.HasOne(d => d.RejectedByNavigation).WithMany(p => p.EventRejectedByNavigations)
                .HasForeignKey(d => d.RejectedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_REJECTEDBY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.EventUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_EVENTS_UPDATEDBY");
        });

        modelBuilder.Entity<EventCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__EventCat__19093A0BB8F2E5D2");

            entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EventImage>(entity =>
        {
            entity.HasKey(e => e.ImgId).HasName("PK__EventIma__352F54F36BDC329E");

            entity.Property(e => e.ImgId).ValueGeneratedNever();
            entity.Property(e => e.ImgName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImgType)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FieldType>(entity =>
        {
            entity.HasKey(e => e.FieldTypeId).HasName("PK__FieldTyp__74418AE297847293");

            entity.Property(e => e.FieldTypeId).ValueGeneratedOnAdd();
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__Forms__FB05B7DD5A5ADCFC");

            entity.Property(e => e.FormId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FormName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Forms)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_createrId");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Forms)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orgId");
        });

        modelBuilder.Entity<Organisation>(entity =>
        {
            entity.HasKey(e => e.OrganisationId).HasName("PK__Organisa__722346DC26169D2A");

            entity.HasIndex(e => e.OrganisationName, "UQ__Organisa__1B62E33DBBDA62E6").IsUnique();

            entity.Property(e => e.OrganisationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Location)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.OrganisationDescription)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.OrganisationName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<RegistrationFormField>(entity =>
        {
            entity.HasKey(e => e.RegistrationFormFieldId).HasName("PK__Registra__6823EFDD4809FA0A");

            entity.Property(e => e.RegistrationFormFieldId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsRequired)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Lable)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Options)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Validations).IsUnicode(false);

            entity.HasOne(d => d.FieldType).WithMany(p => p.RegistrationFormFields)
                .HasForeignKey(d => d.FieldTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_fieldId");

            entity.HasOne(d => d.Form).WithMany(p => p.RegistrationFormFields)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_formId");
        });

        modelBuilder.Entity<RegistrationStatus>(entity =>
        {
            entity.HasKey(e => e.RegistrationStatusId).HasName("PK__Registra__17166AA51651A11A");

            entity.ToTable("RegistrationStatus");

            entity.Property(e => e.RegistrationStatusId).ValueGeneratedOnAdd();
            entity.Property(e => e.RegStatus)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A65D1B1F5");

            entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC607296FFC27");

            entity.Property(e => e.TicketId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Events");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Transactions");

            entity.HasOne(d => d.UserInputForm).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserInputFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInputFormId_UserInputForm");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B86BD076C");

            entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TransactionTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Event).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Events");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C7FDE0DA1");

            entity.HasIndex(e => e.GoogleId, "UQ__Users__A6FBF2FBFCCB1DBA").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534B14A2F5D").IsUnique();

            entity.HasIndex(e => e.GoogleId, "Unique_Users_Google_Id")
                .IsUnique()
                .HasFilter("([GoogleId] IS NOT NULL)");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImageName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserAddress).IsUnicode(false);

            entity.HasOne(d => d.AccountCredentials).WithMany(p => p.Users)
                .HasForeignKey(d => d.AccountCredentialsId)
                .HasConstraintName("FK_USERS_AccountCredentialsID");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_USERS_ADMINISTRATORID");
        });

        modelBuilder.Entity<UserInputForm>(entity =>
        {
            entity.HasKey(e => e.UserInputFormId).HasName("PK__UserInpu__A2369B98D4D521DB");

            entity.ToTable("UserInputForm");

            entity.Property(e => e.UserInputFormId).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithMany(p => p.UserInputForms)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTID_EVENTS");

            entity.HasOne(d => d.User).WithMany(p => p.UserInputForms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserID_Users");
        });

        modelBuilder.Entity<UserInputFormField>(entity =>
        {
            entity.HasKey(e => e.UserInputFormFieldid).HasName("PK__UserInpu__AC5D3622FE397643");

            entity.Property(e => e.UserInputFormFieldid).ValueGeneratedNever();
            entity.Property(e => e.DateResponse).HasColumnType("datetime");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StringResponse).IsUnicode(false);

            entity.HasOne(d => d.RegistrationFormField).WithMany(p => p.UserInputFormFields)
                .HasForeignKey(d => d.RegistrationFormFieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegistrationFormFieldId_RegistrationFormFields");

            entity.HasOne(d => d.UserInputForm).WithMany(p => p.UserInputFormFields)
                .HasForeignKey(d => d.UserInputFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInputFormIdd_UserInputForm");
        });

    }

}
