using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountCredentials",
                columns: table => new
                {
                    AccountCredentialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AccountC__8537A84B61ED7E9E", x => x.AccountCredentialsId);
                });

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    CategoryId = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventCat__19093A0BB8F2E5D2", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "FieldTypes",
                columns: table => new
                {
                    FieldTypeId = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FieldTyp__74418AE297847293", x => x.FieldTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    OrganisationName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    OrganisationDescription = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: false),
                    Location = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organisa__722346DC26169D2A", x => x.OrganisationId);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationStatus",
                columns: table => new
                {
                    RegistrationStatusId = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegStatus = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Registra__17166AA51651A11A", x => x.RegistrationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE1A65D1B1F5", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Administration",
                columns: table => new
                {
                    AdministratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    AdministratorName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    GoogleId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    AdministratorAddress = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    AccountCredentialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    RoleId = table.Column<byte>(type: "tinyint", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    ImgBody = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcceptedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RejectedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Administ__ACDEFED343AA4E48", x => x.AdministratorId);
                    table.ForeignKey(
                        name: "FK_ADMINISTRATION_AccountCredentialsID",
                        column: x => x.AccountCredentialsId,
                        principalTable: "AccountCredentials",
                        principalColumn: "AccountCredentialsId");
                    table.ForeignKey(
                        name: "FK_ORGANISATIONID",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "OrganisationId");
                    table.ForeignKey(
                        name: "FK_ROLEID",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK__Administr__Accep__36B12243",
                        column: x => x.AcceptedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK__Administr__Creat__34C8D9D1",
                        column: x => x.CreatedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK__Administr__Delet__3A81B327",
                        column: x => x.DeletedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK__Administr__Rejec__38996AB5",
                        column: x => x.RejectedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FormName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Forms__FB05B7DD5A5ADCFC", x => x.FormId);
                    table.ForeignKey(
                        name: "FK_createrId",
                        column: x => x.CreatedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK_orgId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "OrganisationId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    GoogleId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    AccountCredentialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserAddress = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    ImgBody = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C7FDE0DA1", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_USERS_ADMINISTRATORID",
                        column: x => x.DeletedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK_USERS_AccountCredentialsID",
                        column: x => x.AccountCredentialsId,
                        principalTable: "AccountCredentials",
                        principalColumn: "AccountCredentialsId");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    EventName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CategoryId = table.Column<byte>(type: "tinyint", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false, computedColumnSql: "([Capacity])", stored: false),
                    Description = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: false),
                    Location = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsOffline = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    MaxNoOfTicketsPerTransaction = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RejectedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RejectedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    RejectedReason = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    EventStartingPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    EventEndingPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationStatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcceptedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Events__7944C81054279AF9", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_EVENTS_ACCEPTEDBY",
                        column: x => x.AcceptedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK_EVENTS_CATEGORYID",
                        column: x => x.CategoryId,
                        principalTable: "EventCategories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_EVENTS_CREATEDBY",
                        column: x => x.CreatedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK_EVENTS_FORMID",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "FormId");
                    table.ForeignKey(
                        name: "FK_EVENTS_ORGANISATIONID",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "OrganisationId");
                    table.ForeignKey(
                        name: "FK_EVENTS_REGISTRATIONSTATUSID",
                        column: x => x.RegistrationStatusId,
                        principalTable: "RegistrationStatus",
                        principalColumn: "RegistrationStatusId");
                    table.ForeignKey(
                        name: "FK_EVENTS_REJECTEDBY",
                        column: x => x.RejectedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK_EVENTS_UPDATEDBY",
                        column: x => x.UpdatedBy,
                        principalTable: "Administration",
                        principalColumn: "AdministratorId");
                });

            migrationBuilder.CreateTable(
                name: "RegistrationFormFields",
                columns: table => new
                {
                    RegistrationFormFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    Lable = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Validations = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Options = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Registra__6823EFDD4809FA0A", x => x.RegistrationFormFieldId);
                    table.ForeignKey(
                        name: "FK_fieldId",
                        column: x => x.FieldTypeId,
                        principalTable: "FieldTypes",
                        principalColumn: "FieldTypeId");
                    table.ForeignKey(
                        name: "FK_formId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "FormId");
                });

            migrationBuilder.CreateTable(
                name: "EventImages",
                columns: table => new
                {
                    ImgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImgBody = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImgType = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ImgName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventIma__352F54F36BDC329E", x => x.ImgId);
                    table.ForeignKey(
                        name: "FK_Img_EventID_Events",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    NoOfTickets = table.Column<int>(type: "int", nullable: false),
                    TransactionTime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__55433A6B86BD076C", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Events",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                    table.ForeignKey(
                        name: "FK_Transactions_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserInputForm",
                columns: table => new
                {
                    UserInputFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserInpu__A2369B98D4D521DB", x => x.UserInputFormId);
                    table.ForeignKey(
                        name: "FK_EVENTID_EVENTS",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                    table.ForeignKey(
                        name: "FK_UserID_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserInputFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tickets__712CC607296FFC27", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Events",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                    table.ForeignKey(
                        name: "FK_Tickets_Transactions",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId");
                    table.ForeignKey(
                        name: "FK_UserInputFormId_UserInputForm",
                        column: x => x.UserInputFormId,
                        principalTable: "UserInputForm",
                        principalColumn: "UserInputFormId");
                });

            migrationBuilder.CreateTable(
                name: "UserInputFormFields",
                columns: table => new
                {
                    UserInputFormFieldid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationFormFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserInputFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    StringResponse = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    NumberResponse = table.Column<int>(type: "int", nullable: true),
                    DateResponse = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserInpu__AC5D3622FE397643", x => x.UserInputFormFieldid);
                    table.ForeignKey(
                        name: "FK_RegistrationFormFieldId_RegistrationFormFields",
                        column: x => x.RegistrationFormFieldId,
                        principalTable: "RegistrationFormFields",
                        principalColumn: "RegistrationFormFieldId");
                    table.ForeignKey(
                        name: "FK_UserInputFormIdd_UserInputForm",
                        column: x => x.UserInputFormId,
                        principalTable: "UserInputForm",
                        principalColumn: "UserInputFormId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administration_AcceptedBy",
                table: "Administration",
                column: "AcceptedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Administration_AccountCredentialsId",
                table: "Administration",
                column: "AccountCredentialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Administration_CreatedBy",
                table: "Administration",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Administration_DeletedBy",
                table: "Administration",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Administration_OrganisationId",
                table: "Administration",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Administration_RejectedBy",
                table: "Administration",
                column: "RejectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Administration_RoleId",
                table: "Administration",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "Unique_Administration_Google_Id",
                table: "Administration",
                column: "GoogleId",
                unique: true,
                filter: "([GoogleId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_EventImages_EventId",
                table: "EventImages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AcceptedBy",
                table: "Events",
                column: "AcceptedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatedBy",
                table: "Events",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Events_FormId",
                table: "Events",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganisationId",
                table: "Events",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RegistrationStatusId",
                table: "Events",
                column: "RegistrationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RejectedBy",
                table: "Events",
                column: "RejectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UpdatedBy",
                table: "Events",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_CreatedBy",
                table: "Forms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_OrganisationId",
                table: "Forms",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "UQ__Organisa__1B62E33DBBDA62E6",
                table: "Organisations",
                column: "OrganisationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormFields_FieldTypeId",
                table: "RegistrationFormFields",
                column: "FieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormFields_FormId",
                table: "RegistrationFormFields",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TransactionId",
                table: "Tickets",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserInputFormId",
                table: "Tickets",
                column: "UserInputFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EventId",
                table: "Transactions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInputForm_EventId",
                table: "UserInputForm",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInputForm_UserId",
                table: "UserInputForm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInputFormFields_RegistrationFormFieldId",
                table: "UserInputFormFields",
                column: "RegistrationFormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInputFormFields_UserInputFormId",
                table: "UserInputFormFields",
                column: "UserInputFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountCredentialsId",
                table: "Users",
                column: "AccountCredentialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeletedBy",
                table: "Users",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "Unique_Users_Google_Id",
                table: "Users",
                column: "GoogleId",
                unique: true,
                filter: "([GoogleId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A6FBF2FBFCCB1DBA",
                table: "Users",
                column: "GoogleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534B14A2F5D",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventImages");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "UserInputFormFields");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "RegistrationFormFields");

            migrationBuilder.DropTable(
                name: "UserInputForm");

            migrationBuilder.DropTable(
                name: "FieldTypes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "RegistrationStatus");

            migrationBuilder.DropTable(
                name: "Administration");

            migrationBuilder.DropTable(
                name: "AccountCredentials");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
