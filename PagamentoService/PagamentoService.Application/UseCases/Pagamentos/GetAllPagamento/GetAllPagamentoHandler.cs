using AutoMapper;
using MediatR;
using PagamentoService.Application.Services.Notifications;
using PagamentoService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.UseCases.Pagamentos.GetAllPagamento
{
    public class GetAllPagamentoHandler : IRequestHandler<GetAllPagamentoRequest, IReadOnlyCollection<GetAllPagamentoResponse>>
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetAllPagamentoHandler(IPagamentoRepository pagamentoRepository,
        IMapper mapper,
        IMediator mediator)
        {
            _pagamentoRepository = pagamentoRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IReadOnlyCollection<GetAllPagamentoResponse>> Handle(GetAllPagamentoRequest request,
        CancellationToken cancellationToken)
        {
            try
            {
                var pagamentos = await _pagamentoRepository.LocalizaTodosPagamentosComPaciente();

                return pagamentos.Select(_mapper.Map<GetAllPagamentoResponse>).ToList();
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao buscar todos os pagamentos",
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return null;
            }

        }
    }
}
