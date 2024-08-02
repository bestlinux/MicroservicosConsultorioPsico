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

namespace PacienteService.Application.UseCases.Pacientes.CreatePaciente
{
    public class CreatePacienteHandler : IRequestHandler<CreatePacienteRequest, CreatePacienteResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPacienteRepository _pacienteRepository;
        //private readonly IProntuarioRepository _prontuarioRepository;
        //private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreatePacienteHandler(IUnitOfWork unitOfWork,
        IPacienteRepository pacienteRepository,
        //IProntuarioRepository prontuarioRepository,
       // IAgendaRepository agendaRepository,
        IMapper mapper,
        IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _pacienteRepository = pacienteRepository;
            //_prontuarioRepository = prontuarioRepository;
            //_agendaRepository = agendaRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<CreatePacienteResponse> Handle(CreatePacienteRequest request,
         CancellationToken cancellationToken)
        {
            try
            {
                var paciente = _mapper.Map<Paciente>(request);

                await _pacienteRepository.AddAsync(paciente);

                await _unitOfWork.Commit(cancellationToken);

                await _mediator.Publish(new PacienteActionNotification
                {
                    Nome = request.Nome,
                    Action = ActionNotification.Created
                }, cancellationToken);

                //PRONTUARIO
                //var pronturarioQueixa = new Prontuario
                //{
                //    Pagina = "Queixa",
                //    PacienteId = paciente.Id,
                //    Conteudo = String.Empty
                //};

                //await _prontuarioRepository.AddAsync(pronturarioQueixa);
                await _unitOfWork.Commit(cancellationToken);

                //await _mediator.Publish(new ProntuarioActionNotification
                //{
                //    Pagina = "Queixa",
                //    Action = ActionNotification.Created
                //}, cancellationToken);

                //var pronturarioEntrevista = new Prontuario
                //{
                //    Pagina = "Entrevista",
                //    PacienteId = paciente.Id,
                //    Conteudo = String.Empty
                //};

                //await _prontuarioRepository.AddAsync(pronturarioEntrevista);
                await _unitOfWork.Commit(cancellationToken);

                //await _mediator.Publish(new ProntuarioActionNotification
                //{
                //    Pagina = "Entrevista",
                //    Action = ActionNotification.Created
                //}, cancellationToken);

                //var pronturarioSessoes = new Prontuario
                //{
                //    Pagina = "Sessões",
                //    PacienteId = paciente.Id,
                //    Conteudo = String.Empty
                //};

                //await _prontuarioRepository.AddAsync(pronturarioSessoes);
                await _unitOfWork.Commit(cancellationToken);

                //await _mediator.Publish(new ProntuarioActionNotification
                //{
                //    Pagina = "Sessões",
                //    Action = ActionNotification.Created
                //}, cancellationToken);

                for (int i = 2024; i < 2084; i++)
                {
                    DateTime inicioSessaoAniversario = new(i, paciente.DataNascimento.Month, paciente.DataNascimento.Day, 09, 00, 00);
                    DateTime fimSessaoAniversario = new(i, paciente.DataNascimento.Month, paciente.DataNascimento.Day, 18, 00, 00);

                    //REGISTRAR ANIVERSARIO NA AGENDA
                    //var agendaAniversario = new Agenda
                    //{

                    //    InicioSessao = inicioSessaoAniversario,
                    //    FimSessao = fimSessaoAniversario,
                    //    PacienteId = paciente.Id,
                    //    TipoConsulta = 4,
                    //    StatusConsulta = 0,
                    //    ValorSessao = 0,
                    //    PacienteNome = paciente.Nome
                    //};

                    //await _agendaRepository.AddAsync(agendaAniversario);
                    await _unitOfWork.Commit(cancellationToken);
                }               
               

                return _mapper.Map<CreatePacienteResponse>(paciente);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Ocorreu um erro ao criar o paciente",
                    Stack = ex.StackTrace,
                }, cancellationToken);
                return null;
            }
        }

    }
}
