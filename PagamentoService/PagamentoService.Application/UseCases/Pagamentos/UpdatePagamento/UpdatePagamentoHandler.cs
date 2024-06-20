using AutoMapper;
using MediatR;
using PagamentoService.Application.Services.Notifications;
using PagamentoService.Application.UseCases.Pagamentos.CreatePagamento;
using PagamentoService.Domain.Entities;
using PagamentoService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.UseCases.Pagamentos.UpdatePagamento
{
    public class UpdatePagamentoHandler : IRequestHandler<UpdatePagamentoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdatePagamentoHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        IPagamentoRepository pagamentoRepository)
        {

            _mapper = mapper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task Handle(UpdatePagamentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var pagamento = _mapper.Map<Pagamento>(request);

                await _pagamentoRepository.UpdateAsync(pagamento);

                await _unitOfWork.Commit(cancellationToken);

                await _mediator.Publish(new PagamentoActionNotification
                {
                    PacienteId = request.PacienteId,
                    Action = ActionNotification.Updated
                }, cancellationToken);

            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao registrar o pagamento",
                    Stack = ex.StackTrace,
                }, cancellationToken);
            }
        }
    }
}
