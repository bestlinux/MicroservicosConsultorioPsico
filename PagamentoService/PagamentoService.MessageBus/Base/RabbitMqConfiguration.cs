namespace PagamentoService.MessageBus.Base
{
    public class RabbitMqConfiguration
    {
        public string Uri { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName_Pagamento { get; set; }
    }
}
