using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using PagamentoService.Application.Services.Notifications;
using PagamentoService.Domain.Entities;
using PagamentoService.Domain.Interfaces;
using PagamentoService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.UseCases.Pagamentos.CreatePagamento
{
    public class CreatePagamentoHandler : IRequestHandler<CreatePagamentoRequest, CreatePagamentoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public CreatePagamentoHandler(
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
        
        public async Task<CreatePagamentoResponse> Handle(CreatePagamentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var pagamento = _mapper.Map<Pagamento>(request);

                await _pagamentoRepository.AddAsync(pagamento);

                await _unitOfWork.Commit(cancellationToken);

                await _mediator.Publish(new PagamentoActionNotification
                {
                    PacienteId = request.PacienteId,
                    Action = ActionNotification.Created
                }, cancellationToken);

                return _mapper.Map<CreatePagamentoResponse>(pagamento);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao registrar o pagamento",
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return null;
            }
        }
    }
}
