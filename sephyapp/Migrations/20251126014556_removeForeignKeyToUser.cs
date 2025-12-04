using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sephyapp.Migrations
{
    /// <inheritdoc />
    public partial class removeForeignKeyToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SephyProfiles_AspNetUsers_UserId",
                table: "SephyProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SephyProfiles_UserId",
                table: "SephyProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SephyProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SephyProfiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SephyProfiles_UserId",
                table: "SephyProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SephyProfiles_AspNetUsers_UserId",
                table: "SephyProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
