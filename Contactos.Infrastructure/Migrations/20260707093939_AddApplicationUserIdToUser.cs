using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contactos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Users");
        }
    }
}
