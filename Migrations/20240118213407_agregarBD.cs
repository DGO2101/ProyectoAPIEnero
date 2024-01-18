using System;
using MagicVill.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVill.Migrations
{
    public partial class agregarBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "villa",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tarifa = table.Column<double>(type: "float", nullable: false),
                    ocupantes = table.Column<int>(type: "int", nullable: false),
                    metrosCudrados = table.Column<int>(type: "int", nullable: false),
                    imagenURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amenidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villa", x => x.id);
                });
            //en el video no se crean todas estas lineas de codigo 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villa");
            //tampoco se crean estas
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Vill>().HasData(
        //        new Vill()
        //        {
        //            id = 1,
        //            name = "villa real",
        //            detalle = "detalle de la villa",
        //            imagenURL = "",
        //            ocupantes = 6,
        //            metrosCudrados = 50,
        //            tarifa = 49,
        //            amenidad = "",
        //            FechaCreacion = DateTime.Now,
        //            fechaActualizacion = DateTime.Now,
        //        });
        //}
    }
}
