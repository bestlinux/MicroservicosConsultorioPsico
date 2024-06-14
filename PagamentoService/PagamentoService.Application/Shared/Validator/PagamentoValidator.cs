using FluentValidation;
using PagamentoService.Application.UseCases.Pagamentos.CreatePagamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.Shared.Validator
{
    public class PagamentoCreateValidator : AbstractValidator<CreatePagamentoRequest>
    {
        public PagamentoCreateValidator()
        {
            RuleFor(x => x.Ano).NotEmpty();
            RuleFor(x => x.StatusPagamento).NotEmpty();
            RuleFor(x => x.Mes).NotEmpty();
            RuleFor(x => x.Valor).NotEmpty();
        }
    }
}
