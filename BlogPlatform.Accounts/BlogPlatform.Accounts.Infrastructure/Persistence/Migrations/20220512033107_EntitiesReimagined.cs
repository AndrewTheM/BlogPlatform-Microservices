using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPlatform.Verifications.DataAccess.Migrations
{
    public partial class EntitiesReimagined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorVerifications");

            migrationBuilder.DropTable(
                name: "VerificationStatuses");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name_MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location_State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AvatarPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PreferredLanguage = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFeedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Result = table.Column<byte>(type: "tinyint", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFeedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName_MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullName_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Annotation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorApplications_ApplicationFeedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "ApplicationFeedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorApplications_FeedbackId",
                table: "AuthorApplications",
                column: "FeedbackId",
                unique: true,
                filter: "[FeedbackId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AuthorApplications");

            migrationBuilder.DropTable(
                name: "ApplicationFeedbacks");

            migrationBuilder.CreateTable(
                name: "VerificationStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    VerificationStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    PromptText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
    }
}
