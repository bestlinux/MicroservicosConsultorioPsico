using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteService.Application.Services.Notifications
{
    public class PacienteActionNotification : INotification
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public ActionNotification Action { get; set; }
    }
}
