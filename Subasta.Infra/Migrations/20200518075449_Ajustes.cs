using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Infra.Migrations
{
    public partial class Ajustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Usuarios_CompradorId",
                table: "Itens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFinal",
                table: "Leiloes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Usuarios_CompradorId",
                table: "Itens",
                column: "CompradorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Usuarios_CompradorId",
                table: "Itens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFinal",
                table: "Leiloes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Usuarios_CompradorId",
                table: "Itens",
                column: "CompradorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
