using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentTimes_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31b8e49d-7d80-4b84-a91e-02de6f7f302a", "AQAAAAIAAYagAAAAEKeZTRoE/1wqdAkOkmmtZEWwt96Rum03UGMuBX5+rvLn2QGqVKaEzLqMc5Zd4Uwlyg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTimes_AppointmentId",
                table: "AppointmentTimes",
                column: "AppointmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentTimes");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2572104f-12cc-4792-b4f3-3ae1be924e31", "AQAAAAIAAYagAAAAEJ8Se1hDvKHoz0lcusMMKOOsOpjVpaWqYZMbrZJp/Tf1TyUUVSKHSQRzK/DJ1TUjew==" });
        }
    }
}
