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

namespace PacienteService.Application.UseCases.Pacientes.UpdatePaciente
{
    public class UpdatePacienteHandler : IRequestHandler<UpdatePacienteRequest, UpdatePacienteResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPacienteRepository _pacienteRepository;
        //private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdatePacienteHandler(IUnitOfWork unitOfWork,
            IPacienteRepository pacienteRepository,
            IMapper mapper,
            //IAgendaRepository agendaRepository,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _pacienteRepository = pacienteRepository;
            //_agendaRepository = agendaRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<UpdatePacienteResponse> Handle(UpdatePacienteRequest request,
           CancellationToken cancellationToken)
        {
            try
            {
                var paciente = _mapper.Map<Paciente>(request);

                await _pacienteRepository.UpdateAsync(paciente);

                await _unitOfWork.Commit(cancellationToken);

                await _mediator.Publish(new PacienteActionNotification
                {
                    Nome = request.Nome,
                    Action = ActionNotification.Updated
                }, cancellationToken);

                //LOCALIZA REGISTRO DE ANIVERSARIOS
                //var aniversarios = _agendaRepository.LocalizaAniversarios(paciente.Id, 4).Result;

                //if (!aniversarios.Any())
                //{
                //    for (int i = 2024; i < 2084; i++)
                //    {
                //        DateTime inicioSessaoAniversario = new(i, paciente.DataNascimento.Month, paciente.DataNascimento.Day, 09, 00, 00);
                //        DateTime fimSessaoAniversario = new(i, paciente.DataNascimento.Month, paciente.DataNascimento.Day, 18, 00, 00);

                //        //REGISTRAR ANIVERSARIO NA AGENDA
                //        var agendaAniversario = new Agenda
                //        {

                //            InicioSessao = inicioSessaoAniversario,
                //            FimSessao = fimSessaoAniversario,
                //            PacienteId = paciente.Id,
                //            TipoConsulta = 4,
                //            StatusConsulta = 0,
                //            ValorSessao = 0,
                //            PacienteNome = paciente.Nome
                //        };

                //        await _agendaRepository.AddAsync(agendaAniversario);
                //        await _unitOfWork.Commit(cancellationToken);
                //    }
                //}

                return _mapper.Map<UpdatePacienteResponse>(paciente);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao atualizar o paciente",
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return null;
            }
        }
    }
}
