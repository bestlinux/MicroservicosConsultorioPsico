using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.UseCases.Pagamentos.GetAllPagamento
{
    public sealed record GetAllPagamentoRequest : IRequest<IReadOnlyCollection<GetAllPagamentoResponse>>;
}
