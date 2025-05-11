using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TiendaOnline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colores", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Tallas",
                columns: table => new
                {
                    TallaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TallaNombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tallas", x => x.TallaId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    TallaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Tallas_TallaId",
                        column: x => x.TallaId,
                        principalTable: "Tallas",
                        principalColumn: "TallaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Colores",
                columns: new[] { "ColorId", "ColorNombre" },
                values: new object[,]
                {
                    { 1, "Amarillo" },
                    { 2, "Azul" },
                    { 3, "Azul Marino" },
                    { 4, "Beige" },
                    { 5, "Blanco" },
                    { 6, "Caqui" },
                    { 7, "Celeste" },
                    { 8, "Coral" },
                    { 9, "Dorado" },
                    { 10, "Fucsia" },
                    { 11, "Gris" },
                    { 12, "Marrón" },
                    { 13, "Morado" },
                    { 14, "Naranja" },
                    { 15, "Negro" },
                    { 16, "Plateado" },
                    { 17, "Rosa" },
                    { 18, "Rojo" },
                    { 19, "Turquesa" },
                    { 20, "Verde" }
                });

            migrationBuilder.InsertData(
                table: "Tallas",
                columns: new[] { "TallaId", "TallaNombre" },
                values: new object[,]
                {
                    { 1, "0-3 meses" },
                    { 2, "3-6 meses" },
                    { 3, "6-9 meses" },
                    { 4, "9-12 meses" },
                    { 5, "12-18 meses" },
                    { 6, "2 años" },
                    { 7, "3 años" },
                    { 8, "4 años" },
                    { 9, "5 años" },
                    { 10, "6 años" },
                    { 11, "8 años" },
                    { 12, "10 años" },
                    { 13, "12 años" },
                    { 14, "14 años" },
                    { 15, "S" },
                    { 16, "M" },
                    { 17, "L" },
                    { 18, "XL" },
                    { 19, "XXL" },
                    { 20, "3XL" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ColorId",
                table: "Productos",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TallaId",
                table: "Productos",
                column: "TallaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Tallas");
        }
    }
}
