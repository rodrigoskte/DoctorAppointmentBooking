using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoctorAppointmentBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDBSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Code = table.Column<string>(type: "varchar(10)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DateTimeSchedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorSpecialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Specialty_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "Code", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "123", false, "Christiano Coccuza", "" },
                    { 2, "456", false, "Ida Fortini", "" },
                    { 3, "789", false, "Bárbara Martins", "" },
                    { 4, "001", false, "Ronu Muole", "" },
                    { 5, "002", false, "Mayfe Puesl", "" },
                    { 6, "003", true, "Deko Gapuobri", "" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "Email", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "r@rrr.com.br", false, "Rodrigo Carvalhomaru", "" },
                    { 2, "shutzing@enzo.com.br", false, "Enzo Shutzing", "" },
                    { 3, "cleber@bluedragon.com.br", false, "Cléber Bluedragon", "" },
                    { 4, "neville@bernard.com.br", false, "Neville Bernard", "" },
                    { 5, "wendell@kessner.com.br", false, "Wendell Kessner", "" },
                    { 6, "adare@gerbitz.com.br", false, "Adare Gerbitz", "" },
                    { 7, "sanders@cameron.com.br", false, "Sanders Cameron", "" },
                    { 8, "agata@wanner.com.br", false, "Agata Wanner", "" },
                    { 9, "senalda@ramirez.com.br", false, "Senalda Ramírez", "" }
                });

            migrationBuilder.InsertData(
                table: "Specialty",
                columns: new[] { "Id", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Nefrologista", false },
                    { 2, "Neurologista", false },
                    { 3, "Nutricionista", false },
                    { 4, "Gastro", false },
                    { 5, "Oftalmologista", false },
                    { 6, "Oncologista", false },
                    { 7, "Clinico Geral", false }
                });

            migrationBuilder.InsertData(
                table: "DoctorSpecialty",
                columns: new[] { "Id", "DoctorId", "IsDeleted", "SpecialtyId" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 2, false, 2 },
                    { 3, 3, false, 3 },
                    { 4, 4, false, 4 },
                    { 5, 5, false, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialty_DoctorId",
                table: "DoctorSpecialty",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialty_SpecialtyId",
                table: "DoctorSpecialty",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_DoctorId",
                table: "Schedule",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_PatientId",
                table: "Schedule",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialty");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
