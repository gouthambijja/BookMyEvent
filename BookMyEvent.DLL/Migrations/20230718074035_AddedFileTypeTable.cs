using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "FileTypeId",
                table: "RegistrationFormFields",
                type: "tinyint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileType",
                columns: table => new
                {
                    FileTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    FileTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileType", x => x.FileTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormFields_FileTypeId",
                table: "RegistrationFormFields",
                column: "FileTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileTypeId_FileType",
                table: "RegistrationFormFields",
                column: "FileTypeId",
                principalTable: "FileType",
                principalColumn: "FileTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTypeId_FileType",
                table: "RegistrationFormFields");

            migrationBuilder.DropTable(
                name: "FileType");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationFormFields_FileTypeId",
                table: "RegistrationFormFields");

            migrationBuilder.DropColumn(
                name: "FileTypeId",
                table: "RegistrationFormFields");
        }
    }
}
