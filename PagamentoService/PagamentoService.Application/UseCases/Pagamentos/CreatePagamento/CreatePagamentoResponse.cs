using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.UseCases.Pagamentos.CreatePagamento
{
    public class CreatePagamentoResponse
    {
        public int Id { get; set; }
        public int StatusPagamento { get; set; }

        public double? Valor { get; set; }

        public string? Observacao { get; set; }

        public int? Mes { get; set; }

        public int? PacienteId { get; set; }

        public int Ano { get; set; }
    }
}
