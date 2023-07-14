using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdministratorIdInTheTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AdministratorId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AdministratorId",
                table: "Transactions",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Administrators",
                table: "Transactions",
                column: "AdministratorId",
                principalTable: "Administration",
                principalColumn: "AdministratorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Administrators",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AdministratorId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
