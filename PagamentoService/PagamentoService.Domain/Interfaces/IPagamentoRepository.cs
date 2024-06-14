using PagamentoService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Domain.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosComPaciente();

        Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosPorPacienteMesAno(int idPaciente, int Mes, int Ano);

        Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosPorPacienteAno(int idPaciente, int Ano);

        Task<IEnumerable<Pagamento>> LocalizaTodosPagamentosPendentesMesAno(int Mes, int Ano);
    }
}
