using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaVecctor.Migrations
{
    /// <inheritdoc />
    public partial class NasaAsteroids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NasaAsteroids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diametro = table.Column<double>(type: "float", nullable: false),
                    Velocidad = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Planeta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NasaAsteroids", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NasaAsteroids");
        }
    }
}
