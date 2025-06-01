using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_api_iw_js.Migrations
{
    /// <inheritdoc />
    public partial class AddCalificationToHito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValorObtenido",
                table: "Progreso",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Calificacion",
                table: "Hito",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorObtenido",
                table: "Progreso");

            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Hito");
        }
    }
}
