using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_api_iw_js.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFinalLimpio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objetivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemaObjetivo",
                columns: table => new
                {
                    ObjetivosId = table.Column<int>(type: "int", nullable: false),
                    TemasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemaObjetivo", x => new { x.ObjetivosId, x.TemasId });
                    table.ForeignKey(
                        name: "FK_TemaObjetivo_Objetivo_ObjetivosId",
                        column: x => x.ObjetivosId,
                        principalTable: "Objetivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemaObjetivo_Tema_TemasId",
                        column: x => x.TemasId,
                        principalTable: "Tema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioObjetivo",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ObjetivoId = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioObjetivo", x => new { x.UsuarioId, x.ObjetivoId });
                    table.ForeignKey(
                        name: "FK_UsuarioObjetivo_Objetivo_ObjetivoId",
                        column: x => x.ObjetivoId,
                        principalTable: "Objetivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioObjetivo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subtema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecursoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HitoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subtema_Hito_HitoId",
                        column: x => x.HitoId,
                        principalTable: "Hito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ObjetivoId = table.Column<int>(type: "int", nullable: false),
                    HitoId = table.Column<int>(type: "int", nullable: false),
                    Escala = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Progreso_UsuarioObjetivo_UsuarioId_ObjetivoId",
                        columns: x => new { x.UsuarioId, x.ObjetivoId },
                        principalTable: "UsuarioObjetivo",
                        principalColumns: new[] { "UsuarioId", "ObjetivoId" },
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Progreso_HitoId",
                table: "Progreso",
                column: "HitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Progreso_UsuarioId_ObjetivoId",
                table: "Progreso",
                columns: new[] { "UsuarioId", "ObjetivoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Subtema_HitoId",
                table: "Subtema",
                column: "HitoId");

            migrationBuilder.CreateIndex(
                name: "IX_TemaObjetivo_TemasId",
                table: "TemaObjetivo",
                column: "TemasId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioObjetivo_ObjetivoId",
                table: "UsuarioObjetivo",
                column: "ObjetivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progreso");

            migrationBuilder.DropTable(
                name: "Subtema");

            migrationBuilder.DropTable(
                name: "TemaObjetivo");

            migrationBuilder.DropTable(
                name: "UsuarioObjetivo");

            migrationBuilder.DropTable(
                name: "Hito");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Objetivo");

            migrationBuilder.DropTable(
                name: "Tema");
        }
    }
}
