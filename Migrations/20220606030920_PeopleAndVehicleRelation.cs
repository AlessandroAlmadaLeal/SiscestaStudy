using Microsoft.EntityFrameworkCore.Migrations;

namespace Siscesta.Migrations
{
    public partial class PeopleAndVehicleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "Veiculos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_PessoaId",
                table: "Veiculos",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Pessoas_PessoaId",
                table: "Veiculos",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Pessoas_PessoaId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_PessoaId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Veiculos");
        }
    }
}
