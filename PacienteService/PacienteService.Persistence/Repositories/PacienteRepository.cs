using Microsoft.EntityFrameworkCore;
using PacienteService.Domain.Entities;
using PacienteService.Domain.Interfaces;
using PacienteService.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PacienteService.Persistence.Repositories
{
    public class PacienteRepository(AppDbContext context) : Repository<Paciente>(context), IPacienteRepository
    {
        public async Task<IEnumerable<Paciente>> LocalizaAniversariantes(int Mes)
        {
            return await _db.Pacientes.AsNoTracking()
                 .Where(b => b.DataNascimento.Month == Mes)
                 .OrderBy(b => b.Nome)
                 .ToListAsync();
        }
    }
}
