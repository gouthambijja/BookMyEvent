using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileResponseInUserInputFormFieldsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileResponse",
                table: "UserInputFormFields",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileResponse",
                table: "UserInputFormFields");
        }
    }
}
