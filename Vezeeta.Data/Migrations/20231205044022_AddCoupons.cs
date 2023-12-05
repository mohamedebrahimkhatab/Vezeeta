using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoupons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfRequests = table.Column<int>(type: "int", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2572104f-12cc-4792-b4f3-3ae1be924e31", "AQAAAAIAAYagAAAAEJ8Se1hDvKHoz0lcusMMKOOsOpjVpaWqYZMbrZJp/Tf1TyUUVSKHSQRzK/DJ1TUjew==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba40c8d2-5bff-4711-9559-ee5a4e3f0ba8", "AQAAAAIAAYagAAAAEOjroekVwwqWUY5R0JggUWcCORYXrTfI+FXd0BorWyPKrER3GLeE4KYcMyrH4YuYCw==" });
        }
    }
}
