using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PagamentoService.Application.UseCases.Pagamentos.UpdatePagamento;
using PagamentoService.Domain.Entities;
using PagamentoService.Domain.Interfaces;
using PagamentoService.MessageBus.Base;
using PagamentoService.MessageBus.SendMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PagamentoService.MessageBus.RecievedMessage
{
    public class ReceivedPagamentoUpdateMessage : BackgroundService
    {
        private IModel _channel;
        private readonly IConnection _connection;
        private readonly string _uri;
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _queueName;
        private readonly ILogger<ReceivedPagamentoUpdateMessage> _logger;

        public IServiceProvider ServiceProvider { get; }

        public ReceivedPagamentoUpdateMessage(
        IOptions<RabbitMqConfiguration> rabbitMqOptions, IServiceProvider serviceProvider, ILogger<ReceivedPagamentoUpdateMessage> logger)
        {
            _uri = rabbitMqOptions.Value.Uri;
            _port = rabbitMqOptions.Value.Port;
            _hostName = rabbitMqOptions.Value.HostName;
            _queueName = rabbitMqOptions.Value.QueueName_Pagamento;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _logger = logger;
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_uri),
                HostName = _hostName,
                Port = _port,
                UserName = _userName,
                Password = _password,
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
                ServiceProvider = serviceProvider;
            }
            catch (BrokerUnreachableException ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao conectar no servidor do RabbitMQ");
            }          
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_channel != null)
            {
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += async (sender, eventArgs) =>
                {
                    var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                    var orderToPayment = JsonConvert.DeserializeObject<PagamentoUpdateMessage>(content);

                    var pagamento = new UpdatePagamentoRequest
                    {
                        StatusPagamento = 1,
                        Id = orderToPayment?.Id,
                        Valor = orderToPayment?.Valor,
                        Ano = orderToPayment?.Ano,
                        Mes = orderToPayment?.Mes,
                        Observacao = orderToPayment?.Observacao,
                        PacienteId = orderToPayment?.PacienteId
                    };
                    Thread.Sleep(10000);

                    _logger.LogInformation("ReceivedPagamentoUpdateMessage() mensagem de pagamento {Id} recebida com sucesso !", orderToPayment?.Id);

                    using (var scope = ServiceProvider.CreateScope())
                    {
                        var scopedService = scope.ServiceProvider.GetRequiredService<IMediator>();
                        await scopedService.Send(pagamento, stoppingToken);
                    }

                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                };
                _channel.BasicConsume(queue: _queueName, autoAck: false, consumer);
            }

            return Task.CompletedTask;
        }

    }
}
