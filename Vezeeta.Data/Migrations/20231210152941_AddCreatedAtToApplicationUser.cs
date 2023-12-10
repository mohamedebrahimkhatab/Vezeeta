using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2bd6cc11-459f-4424-9e1b-7dea6e692ead", "AQAAAAIAAYagAAAAEFT0myorcbYAmb9irBMNpHMTmoRR0PvlfjbarkFl2NXjz49MZiySZU4Xz0S8TDFSjg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9873649e-2dbf-45d5-9509-67d402a2cb62", "AQAAAAIAAYagAAAAEAyC1iCT+waTVWK7xT5gTnthQjj95VtgkABHfl4wkEt42Gx6aQp6LhC7X1bee0MKew==" });
        }
    }
}
