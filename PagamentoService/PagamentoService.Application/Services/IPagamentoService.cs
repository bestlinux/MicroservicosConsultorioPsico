using PagamentoService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Application.Services
{
    public interface IPagamentoService
    {
        void Update(Pagamento pagamento);
    }
}
