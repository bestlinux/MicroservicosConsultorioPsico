using PagamentoService.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Domain.Entities
{
    public class Pagamento : Entity
    {

        //1 - PAGO
        //2 - ABERTO
        //3 - PROCESSANDO
        public int StatusPagamento { get; set; }

        public double? Valor { get; set; }

        public string? Observacao { get; set; }

        public int? Mes { get; set; }

        public int? PacienteId { get; set; }

        public int Ano { get; set; }

    }
}
