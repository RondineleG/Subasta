using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leiloes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFinal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leiloes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Login = table.Column<string>(maxLength: 20, nullable: false),
                    Senha = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    ValorInicial = table.Column<decimal>(nullable: false),
                    ValorFinal = table.Column<decimal>(nullable: false),
                    LeilaoId = table.Column<Guid>(nullable: false),
                    CompradorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itens_Usuarios_CompradorId",
                        column: x => x.CompradorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itens_Leiloes_LeilaoId",
                        column: x => x.LeilaoId,
                        principalTable: "Leiloes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lances_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lances_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_CompradorId",
                table: "Itens",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_LeilaoId",
                table: "Itens",
                column: "LeilaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Lances_ItemId",
                table: "Lances",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Lances_UsuarioId",
                table: "Lances",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lances");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Leiloes");
        }
    }
}
