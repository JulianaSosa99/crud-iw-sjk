using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_api_iw_js.Migrations
{
    /// <inheritdoc />
    public partial class PRIMERAMIGRACION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objetivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objetivo_Tema_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Tema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recomendacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemaId = table.Column<int>(type: "int", nullable: false),
                    NivelRiesgo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recomendacion_Tema_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Tema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjetivoId = table.Column<int>(type: "int", nullable: false),
                    TemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hito_Objetivo_ObjetivoId",
                        column: x => x.ObjetivoId,
                        principalTable: "Objetivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hito_Tema_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Tema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Progreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    HitoId = table.Column<int>(type: "int", nullable: false),
                    Escala = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progreso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progreso_Hito_HitoId",
                        column: x => x.HitoId,
                        principalTable: "Hito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hito_ObjetivoId",
                table: "Hito",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Hito_TemaId",
                table: "Hito",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivo_TemaId",
                table: "Objetivo",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Progreso_HitoId",
                table: "Progreso",
                column: "HitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendacion_TemaId",
                table: "Recomendacion",
                column: "TemaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progreso");

            migrationBuilder.DropTable(
                name: "Recomendacion");

            migrationBuilder.DropTable(
                name: "Hito");

            migrationBuilder.DropTable(
                name: "Objetivo");

            migrationBuilder.DropTable(
                name: "Tema");
        }
    }
}
