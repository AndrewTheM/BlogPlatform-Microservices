using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPlatform.Verifications.DataAccess.Migrations
{
    public partial class MicroserviceExtractionAndChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerificationStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorVerifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromptText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VerificationStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorVerifications_VerificationStatuses_VerificationStatusId",
                        column: x => x.VerificationStatusId,
                        principalTable: "VerificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorVerifications_VerificationStatusId",
                table: "AuthorVerifications",
                column: "VerificationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationStatuses_StatusName",
                table: "VerificationStatuses",
                column: "StatusName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorVerifications");

            migrationBuilder.DropTable(
                name: "VerificationStatuses");
        }
    }
}
