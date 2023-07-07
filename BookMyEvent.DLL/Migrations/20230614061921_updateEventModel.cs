using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class updateEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImgBody",
                table: "Events",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImgBody",
                table: "Events");
        }
    }
}
