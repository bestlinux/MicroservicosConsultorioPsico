using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PagamentoService.Domain.Interfaces;
using PagamentoService.MessageBus.Base;
using PagamentoService.MessageBus.RecievedMessage;
using PagamentoService.MessageBus.SendMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.MessageBus.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureMessageBusApp(this IServiceCollection services,
                                                    IConfiguration configuration)
        {
            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMq"));
            services.AddTransient<IMessageBus, RabbitMqMessageBus>();

        }
    }
}
