using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PagamentoService.Application.Shared.Validator;
using PagamentoService.Application.UseCases.Pagamentos.CreatePagamento;
using PagamentoService.Domain.Entities;
using PagamentoService.MessageBus.Base;
using PagamentoService.MessageBus.SendMessages;

namespace PagamentoService.WebApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageBus _messageBus;
        private readonly string QueueName_Pagamento;
        private readonly ILogger<PagamentosController> _logger;

        public PagamentosController(IMediator mediator, IMessageBus messageBus, IOptions<RabbitMqConfiguration> rabbitMqOptions, ILogger<PagamentosController> logger)
        {
            _mediator = mediator;
            _messageBus = messageBus;
            QueueName_Pagamento = rabbitMqOptions.Value.QueueName_Pagamento;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatePagamentoResponse>> Create(CreatePagamentoRequest request,
                                                        CancellationToken cancellationToken)
        {
            var validator = new PagamentoCreateValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _logger.LogInformation("Create() criando pagamento no banco de dados ");

            var result = await _mediator.Send(request, cancellationToken);

            // envia mensagem para atualizar o status do pagamento
            PagamentoUpdateMessage message = new()
            {
                Id = result.Id,
                StatusPagamento = result.StatusPagamento,
                Valor = result.Valor,
                PacienteId = result.PacienteId,
                Observacao = result.Observacao,
                Ano = result.Ano,
                Mes = result.Mes
            };

            _logger.LogInformation("Create() enviando mensagem para o Bus para atualizar status de pagamento ");

            _messageBus.SendMessage(message, QueueName_Pagamento);
            return Ok(result);
        }
    }
}
