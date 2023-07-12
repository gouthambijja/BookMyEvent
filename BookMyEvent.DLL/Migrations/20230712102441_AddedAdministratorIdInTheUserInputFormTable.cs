using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdministratorIdInTheUserInputFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdministratorId",
                table: "UserInputForm",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInputForm_AdministratorId",
                table: "UserInputForm",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ADMINISTRATORID_ADMINISTRATION",
                table: "UserInputForm",
                column: "AdministratorId",
                principalTable: "Administration",
                principalColumn: "AdministratorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ADMINISTRATORID_ADMINISTRATION",
                table: "UserInputForm");

            migrationBuilder.DropIndex(
                name: "IX_UserInputForm_AdministratorId",
                table: "UserInputForm");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "UserInputForm");
        }
    }
}
