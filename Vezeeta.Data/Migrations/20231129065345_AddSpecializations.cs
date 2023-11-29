using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vezeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecializations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Allergy and Immunology" },
                    { 2, "Anesthesiology" },
                    { 3, "Audiology" },
                    { 4, "Cardiology" },
                    { 5, "Cardiothoracic Surgery" },
                    { 6, "Cardiology and Vascular Disease" },
                    { 7, "Chest and Respiratory" },
                    { 8, "Colon and Rectal Surgery" },
                    { 9, "Dentistry" },
                    { 10, "Dermatology" },
                    { 11, "Diabetes and Endocrinology" },
                    { 12, "Dietitian and Nutrition" },
                    { 13, "Emergency Medicine" },
                    { 14, "Ear, Nose and Throat" },
                    { 15, "Family Medicine" },
                    { 16, "Forensic Pathology" },
                    { 17, "Gastroenterology and Endoscopy" },
                    { 18, "Genetics and Genomics" },
                    { 19, "General Practice" },
                    { 20, "General Surgery" },
                    { 21, "Geriatrics" },
                    { 22, "Hematology" },
                    { 23, "Hepatology" },
                    { 24, "Hospital Medicine" },
                    { 25, "Hospice and Palliative Medicine" },
                    { 26, "IVF and Infertility" },
                    { 27, "Internal Medicine" },
                    { 28, "Interventional Radiology" },
                    { 29, "Laboratories" },
                    { 30, "Neurology" },
                    { 31, "Neurosurgery" },
                    { 32, "Obesity and Laparoscopic Surgery" },
                    { 33, "Oncologic Surgery" },
                    { 34, "Oncology" },
                    { 35, "Ophthalmic Surgery" },
                    { 36, "Ophthalmology" },
                    { 37, "Orthopedic Surgery" },
                    { 38, "Osteopathy" },
                    { 39, "Otolaryngology" },
                    { 40, "Pain Management" },
                    { 41, "Pathology" },
                    { 42, "Pediatric Surgery" },
                    { 43, "Pediatrics" },
                    { 44, "Phoniatrics" },
                    { 45, "Physical Medicine and Rehabilitation" },
                    { 46, "Physiotherapy and Sports Injuries" },
                    { 47, "Plastic Surgery" },
                    { 48, "Preventive Medicine" },
                    { 49, "Psychiatry" },
                    { 50, "Radiology" },
                    { 51, "Rheumatology" },
                    { 52, "Sleep Medicine" },
                    { 53, "Spinal Surgery" },
                    { 54, "Thoracic Surgery" },
                    { 55, "Urology" },
                    { 56, "Vascular Surgery" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
