using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class GoogleIdNullableAndRemoveUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Unique_Users_Google_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UQ__Users__A6FBF2FBFCCB1DBA",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "Unique_Administration_Google_Id",
                table: "Administration");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

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
                name: "Unique_Administration_Google_Id",
                table: "Administration",
                column: "GoogleId",
                unique: true,
                filter: "([GoogleId] IS NOT NULL)");
        }
    }
}
