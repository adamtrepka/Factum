using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factum.Modules.Ledger.Infrastructure.EF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ledger");

            migrationBuilder.CreateTable(
                name: "Blockchain",
                schema: "ledger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousBlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PreviousBlockHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EntriesRootHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Confirmation = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blockchain", x => x.Id);
                    table.UniqueConstraint("AK_Blockchain_BusinessId", x => x.BusinessId);
                    table.ForeignKey(
                        name: "FK_Blockchain_Blockchain_PreviousBlockId",
                        column: x => x.PreviousBlockId,
                        principalSchema: "ledger",
                        principalTable: "Blockchain",
                        principalColumn: "BusinessId");
                });

            migrationBuilder.CreateTable(
                name: "Inbox",
                schema: "ledger",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outbox",
                schema: "ledger",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                schema: "ledger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Blockchain_BlockId",
                        column: x => x.BlockId,
                        principalSchema: "ledger",
                        principalTable: "Blockchain",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blockchain_BusinessId",
                schema: "ledger",
                table: "Blockchain",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blockchain_PreviousBlockId",
                schema: "ledger",
                table: "Blockchain",
                column: "PreviousBlockId",
                unique: true,
                filter: "[PreviousBlockId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_BlockId",
                schema: "ledger",
                table: "Entries",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_BusinessId",
                schema: "ledger",
                table: "Entries",
                column: "BusinessId",
                unique: true,
                filter: "[BusinessId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries",
                schema: "ledger");

            migrationBuilder.DropTable(
                name: "Inbox",
                schema: "ledger");

            migrationBuilder.DropTable(
                name: "Outbox",
                schema: "ledger");

            migrationBuilder.DropTable(
                name: "Blockchain",
                schema: "ledger");
        }
    }
}
