using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyEvent.DLL.Migrations
{
    /// <inheritdoc />
    public partial class removedAvailableSeatsComputedColumnAndAddedDefaultValueAs1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvailableSeats",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValueSql: "((-1))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvailableSeats",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((-1))");
        }
    }
}
