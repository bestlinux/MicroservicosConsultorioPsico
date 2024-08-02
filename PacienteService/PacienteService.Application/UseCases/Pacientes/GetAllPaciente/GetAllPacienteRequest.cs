using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteService.Application.UseCases.Pacientes.GetAllPaciente
{
    public sealed record GetAllPacienteRequest : IRequest<IReadOnlyCollection<GetAllPacienteResponse>>;
}
