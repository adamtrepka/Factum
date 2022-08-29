using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "chronicle");

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "chronicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SagaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sagas",
                schema: "chronicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SagaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sagas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Type_SagaId",
                schema: "chronicle",
                table: "Logs",
                columns: new[] { "Type", "SagaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Sagas_Type_SagaId",
                schema: "chronicle",
                table: "Sagas",
                columns: new[] { "Type", "SagaId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs",
                schema: "chronicle");

            migrationBuilder.DropTable(
                name: "Sagas",
                schema: "chronicle");
        }
    }
}
