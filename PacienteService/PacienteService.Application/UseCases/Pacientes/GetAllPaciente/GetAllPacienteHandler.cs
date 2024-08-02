using AutoMapper;
using MediatR;
using PacienteService.Application.Services.Notifications;
using PacienteService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteService.Application.UseCases.Pacientes.GetAllPaciente
{
    public class GetAllPacienteHandler : IRequestHandler<GetAllPacienteRequest, IReadOnlyCollection<GetAllPacienteResponse>>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetAllPacienteHandler(IPacienteRepository pacienteRepository,
        IMapper mapper,
        IMediator mediator)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IReadOnlyCollection<GetAllPacienteResponse>> Handle(GetAllPacienteRequest request,
        CancellationToken cancellationToken)
        {
            try
            {
                var pacientes = await _pacienteRepository.GetAllAsync(cancellationToken);

                return pacientes.Select(_mapper.Map<GetAllPacienteResponse>).ToList();
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao buscar todos os pacientes",
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return null;
            }
           
        }
    }
}
