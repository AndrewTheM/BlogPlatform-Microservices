using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPlatform.Verifications.DataAccess.Migrations
{
    public partial class UniqueUserPerAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts");
        }
    }
}
