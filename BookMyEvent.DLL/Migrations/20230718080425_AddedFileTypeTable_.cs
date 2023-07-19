using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileTypeTable_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameTable(
                name: "FileType",
                newName: "FileTypes");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes");

            migrationBuilder.RenameTable(
                name: "FileTypes",
                newName: "FileType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileType",
                table: "FileType",
                column: "FileTypeId");
        }
    }
}
