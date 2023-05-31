using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPlatform.Posts.DataAccess.Migrations
{
    public partial class AddedAudioPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioPath",
                table: "Posts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioPath",
                table: "Posts");
        }
    }
}
