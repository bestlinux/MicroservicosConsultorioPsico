using AutoMapper;
using MediatR;
using PacienteService.Application.Services.Notifications;
using PacienteService.Domain.Entities;
using PacienteService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteService.Application.UseCases.Pacientes.GetByIdPaciente
{
    public class GetByIdPacienteHandler : IRequestHandler<GetByIdPacienteRequest, GetByIdPacienteResponse>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetByIdPacienteHandler(IPacienteRepository pacienteRepository,
        IMapper mapper,
        IMediator mediator)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<GetByIdPacienteResponse> Handle(GetByIdPacienteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var pacienteId = _mapper.Map<Paciente>(request);

                var paciente = await _pacienteRepository.GetByIdAsync(pacienteId.Id);

                return _mapper.Map<GetByIdPacienteResponse>(paciente);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao buscar o pacientes de id " + request.Id,
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return null;
            }
        }
    }
}
