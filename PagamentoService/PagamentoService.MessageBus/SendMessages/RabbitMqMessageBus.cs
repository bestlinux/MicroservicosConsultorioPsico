using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PagamentoService.Application.Services.Events;
using PagamentoService.Domain.Common;
using PagamentoService.MessageBus.Base;
using RabbitMQ.Client;

namespace PagamentoService.MessageBus.SendMessages
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly string _uri;
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;
        private readonly ILogger<RabbitMqMessageBus> _logger;

        public RabbitMqMessageBus(IOptions<RabbitMqConfiguration> options, ILogger<RabbitMqMessageBus> logger)
        {
            _uri = options.Value.Uri;
            _hostName = options.Value.HostName;
            _port = options.Value.Port;
            _userName = options.Value.UserName;
            _password = options.Value.Password;
            _logger = logger;
        }

        public void SendMessage(BaseMessage message, string QueueName)
        {
            if (!CheckRabbitMqConnection())
                return;

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                    routingKey: QueueName,
                    basicProperties: properties,
                    body: body);
                _logger.LogInformation("SendMessage() mensagem de status de pagamento enviada com sucesso para o RabbitMQ");
            }
        }

        private void CreateRabbitMqConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(_uri),
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not create connection: {ex.Message}");
            }
        }

        private bool CheckRabbitMqConnection()
        {
            if (_connection != null)
                return true;

            CreateRabbitMqConnection();
            return _connection != null;
        }

    }
}
