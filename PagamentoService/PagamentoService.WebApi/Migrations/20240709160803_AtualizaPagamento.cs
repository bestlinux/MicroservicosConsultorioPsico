using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PagamentoService.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteDiaVencimento",
                table: "Pagamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PacienteNome",
                table: "Pagamentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteTipoPagamento",
                table: "Pagamentos",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PacienteDiaVencimento",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "PacienteNome",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "PacienteTipoPagamento",
                table: "Pagamentos");
        }
    }
}
