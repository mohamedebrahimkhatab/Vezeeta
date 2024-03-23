using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentRealTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentRealTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6411b294-1829-44c3-8be1-addc85b8ca40", "AQAAAAIAAYagAAAAEH0qx6YjQIo5LuDiv0hr7bNAcLpjrnCdgTyOzjgzQV5g6m8sKWAOG7NAdDUoiVh99g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentRealTime",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ca847d4-2578-4c3e-9a3e-763cd7b58962", "AQAAAAIAAYagAAAAEODdEloDoLq94IAIxLYLrdr/BP1mvzwMiNyUZfsd+jmq0TMkUJ4TNVC040IKdP68mA==" });
        }
    }
}
