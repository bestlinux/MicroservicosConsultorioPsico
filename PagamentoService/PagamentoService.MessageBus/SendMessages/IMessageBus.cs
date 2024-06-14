
using PagamentoService.Domain.Common;
using PagamentoService.MessageBus.Base;

namespace PagamentoService.MessageBus.SendMessages { 
    public interface IMessageBus  {
         void SendMessage(BaseMessage message, string QueueName);
    }
}