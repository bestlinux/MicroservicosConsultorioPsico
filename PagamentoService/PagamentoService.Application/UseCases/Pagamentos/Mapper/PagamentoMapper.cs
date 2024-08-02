using AutoMapper;
using PagamentoService.Application.UseCases.Pagamentos.CreatePagamento;
using PagamentoService.Application.UseCases.Pagamentos.GetAllPagamento;
using PagamentoService.Application.UseCases.Pagamentos.UpdatePagamento;
using PagamentoService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiConsultorio.Application.UseCases.Pagamentos.Mapper
{
    public class PagamentoMapper : Profile
    {
        public PagamentoMapper()
        {
            CreateMap<CreatePagamentoRequest, Pagamento>();
            CreateMap<Pagamento, CreatePagamentoResponse>();
            CreateMap<UpdatePagamentoRequest, Pagamento>();
            CreateMap<GetAllPagamentoRequest, Pagamento>();
            CreateMap<Pagamento, GetAllPagamentoResponse>();
        }
    }
}
