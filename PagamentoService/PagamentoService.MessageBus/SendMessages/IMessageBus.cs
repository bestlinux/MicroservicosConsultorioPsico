
using PagamentoService.Domain.Common;
using PagamentoService.MessageBus.Base;

namespace PagamentoService.MessageBus.SendMessages { 
    public interface IMessageBus  {
         Task SendMessage(BaseMessage message, string QueueName);
    }
}