using PagamentoService.Domain.Entities;
using PagamentoService.Domain.Interfaces;
using PagamentoService.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Persistence.Repositories
{
    public class PagamentoRepository : Repository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosComPaciente()
        {
            return null;
        }

        public async Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosPorPacienteMesAno(int idPaciente, int Mes, int Ano)
        {
            return null;
        }

        public async Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosPorPacienteAno(int idPaciente, int Ano)
        {
            return null;
        }

        public async Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosPendentesMesAno(int Mes, int Ano)
        {
            return null;
        }
    }
}
