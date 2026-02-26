using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptimalVisionAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferredAcademicIntake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HappyToTravelFirst = table.Column<bool>(type: "bit", nullable: false),
                    YearOfLastAcademicStudies = table.Column<int>(type: "int", nullable: false),
                    QualificationObtained = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grades = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfCompletion = table.Column<int>(type: "int", nullable: false),
                    Sponsor = table.Column<int>(type: "int", nullable: false),
                    AvailableDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnyAgent = table.Column<bool>(type: "bit", nullable: false),
                    CanYouStopAgent = table.Column<bool>(type: "bit", nullable: false),
                    AnyVisaRefusal = table.Column<bool>(type: "bit", nullable: false),
                    AnyBan = table.Column<bool>(type: "bit", nullable: false),
                    AvailabilityOfMaintenanceFunds = table.Column<bool>(type: "bit", nullable: false),
                    ReadyToProceedNow = table.Column<bool>(type: "bit", nullable: false),
                    TotalArriveAbroadBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AreFundsAvailableNow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TryYourLuckWithChosenCountryOrNot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateApplied = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentCategoryId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentCountryOfPreference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCountryOfPreference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCountryOfPreference_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCountryOfPreference_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentQualifiedCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQualifiedCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQualifiedCourse_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentVisaBanCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentVisaBanCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentVisaBanCountries_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentVisaBanCountries_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentVisaRefusalCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentVisaRefusalCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentVisaRefusalCountries_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentVisaRefusalCountries_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCountryOfPreference_CountryId",
                table: "StudentCountryOfPreference",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCountryOfPreference_StudentId",
                table: "StudentCountryOfPreference",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQualifiedCourse_StudentId",
                table: "StudentQualifiedCourse",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisaBanCountries_CountryId",
                table: "StudentVisaBanCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisaBanCountries_StudentId",
                table: "StudentVisaBanCountries",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisaRefusalCountries_CountryId",
                table: "StudentVisaRefusalCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisaRefusalCountries_StudentId",
                table: "StudentVisaRefusalCountries",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "DocumentCategory");

            migrationBuilder.DropTable(
                name: "StudentCountryOfPreference");

            migrationBuilder.DropTable(
                name: "StudentDocument");

            migrationBuilder.DropTable(
                name: "StudentQualifiedCourse");

            migrationBuilder.DropTable(
                name: "StudentVisaBanCountries");

            migrationBuilder.DropTable(
                name: "StudentVisaRefusalCountries");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
