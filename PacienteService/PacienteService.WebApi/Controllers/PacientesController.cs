using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PacienteService.Application.UseCases.Pacientes.GetAllPaciente;

namespace PacienteService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly IMessageBus _messageBus;
        //private readonly string QueueName_Pagamento;
        private readonly ILogger<PacientesController> _logger;

        public PacientesController(IMediator mediator, ILogger<PacientesController> logger) //, IMessageBus messageBus, IOptions<RabbitMqConfiguration> rabbitMqOptions, ILogger<PagamentosController> logger)
        {
            _mediator = mediator;
            //_messageBus = messageBus;
            //QueueName_Pagamento = rabbitMqOptions.Value.QueueName_Pagamento;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetAllPacienteResponse>> GetAll([FromQuery] GetAllPacienteRequest listaPagamento, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(listaPagamento, cancellationToken);
            return Ok(result);
        }
    }
}
