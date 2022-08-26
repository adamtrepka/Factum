using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factum.Modules.Ledger.Core.EF.Migrations
{
    public partial class AddConfirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Confirmation",
                schema: "ledger",
                table: "Blockchain",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmation",
                schema: "ledger",
                table: "Blockchain");
        }
    }
}
