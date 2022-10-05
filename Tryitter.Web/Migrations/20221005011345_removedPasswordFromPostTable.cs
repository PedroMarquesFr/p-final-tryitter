using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tryitter.Web.Migrations
{
    public partial class removedPasswordFromPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
