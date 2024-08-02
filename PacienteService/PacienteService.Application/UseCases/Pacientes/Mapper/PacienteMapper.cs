using AutoMapper;
using PacienteService.Application.UseCases.Pacientes.CreatePaciente;
using PacienteService.Application.UseCases.Pacientes.GetAllPaciente;
using PacienteService.Application.UseCases.Pacientes.GetByIdPaciente;
using PacienteService.Application.UseCases.Pacientes.UpdatePaciente;
using PacienteService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteService.Application.UseCases.Pacientes.Mapper
{
    public sealed class PacienteMapper : Profile
    {
        public PacienteMapper() 
        {
            CreateMap<CreatePacienteRequest, Paciente>();
            CreateMap<Paciente, CreatePacienteResponse>();
            CreateMap<UpdatePacienteRequest, Paciente>();
            CreateMap<Paciente, UpdatePacienteResponse>();
            CreateMap<GetAllPacienteRequest, Paciente>();
            CreateMap<Paciente, GetAllPacienteResponse>();
            CreateMap<GetByIdPacienteRequest, Paciente>();
            CreateMap<Paciente, GetByIdPacienteResponse>();
        }
    }
}
