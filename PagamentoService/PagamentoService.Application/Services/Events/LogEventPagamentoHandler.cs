using MediatR;
using Microsoft.Extensions.Logging;
using PagamentoService.Application.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.Services.Events
{
    public class LogEventPagamentoHandler :
                 INotificationHandler<PagamentoActionNotification>,
                 INotificationHandler<ErrorNotification>
    {
        private readonly ILogger<LogEventPagamentoHandler> _logger;

        public LogEventPagamentoHandler(ILogger<LogEventPagamentoHandler> logger)
        {
            _logger = logger;
        }
    
        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogError($"ERROR : '{notification.Error} \n {notification.Stack}'");
            }, cancellationToken);
        }

        Task INotificationHandler<PagamentoActionNotification>.Handle(PagamentoActionNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation($"Pagamento para o paciente {notification.PacienteId} - foi {notification.Action.ToString().ToLower()} com sucesso !");
            }, cancellationToken);
        }
    }
}
