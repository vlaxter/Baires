using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Baires.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CurrentRole = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberOfRecommendations = table.Column<int>(type: "int", nullable: true),
                    NumberOfConnections = table.Column<int>(type: "int", nullable: true),
                    PriorityIndex = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonId",
                table: "People",
                column: "PersonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
