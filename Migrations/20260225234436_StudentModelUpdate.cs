using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptimalVisionAPI.Migrations
{
    /// <inheritdoc />
    public partial class StudentModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Student");
        }
    }
}
