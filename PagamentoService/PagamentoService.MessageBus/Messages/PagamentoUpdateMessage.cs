using PagamentoService.Domain.Common;
using PagamentoService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.MessageBus.Base
{
    public class PagamentoUpdateMessage : BaseMessage
    {
        //1 - PAGO
        //2 - ABERTO

        public int Id { get; set; }
        public int StatusPagamento { get; set; }

        public double? Valor { get; set; }

        public string? Observacao { get; set; }

        public int? Mes { get; set; }

        public int? PacienteId { get; set; }

        public int Ano { get; set; }
    }
}
