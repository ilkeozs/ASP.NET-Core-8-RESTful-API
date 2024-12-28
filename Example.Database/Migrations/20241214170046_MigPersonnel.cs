using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Database.Migrations
{
    /// <inheritdoc />
    public partial class MigPersonnel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personnel",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnel_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "dbo",
                        principalTable: "District",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_DistrictId",
                schema: "dbo",
                table: "Personnel",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personnel",
                schema: "dbo");
        }
    }
}
