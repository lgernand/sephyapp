using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sephyapp.Migrations
{
    /// <inheritdoc />
    public partial class EditProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SephyProfiles",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "AccountType",
                table: "SephyProfiles",
                newName: "Bio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "SephyProfiles",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "SephyProfiles",
                newName: "AccountType");
        }
    }
}
