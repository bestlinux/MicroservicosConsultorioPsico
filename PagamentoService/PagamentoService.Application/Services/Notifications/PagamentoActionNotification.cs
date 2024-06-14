using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.Services.Notifications
{
    public class PagamentoActionNotification : INotification
    {
        public int? PacienteId { get; set; }
        public ActionNotification Action { get; set; }
    }
}
