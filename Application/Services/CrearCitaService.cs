using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CrearCitaService
    {
        readonly IUnitOfWork _unitOfWork;


        public CrearCitaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public CitaResponse CrearCita(CitaRequest request)
        {
            Cita cita = _unitOfWork.CitaRepository.FindFirstOrDefault(C => C.Id == request.Id);
            if (cita==null)
            {
                Cita NuevaCita = new Cita();
                NuevaCita.Medico = request.Medico;
                NuevaCita.Paciente = request.Paciente;
                NuevaCita.fecha = new DateTime();
                _unitOfWork.CitaRepository.Add(NuevaCita);
                _unitOfWork.Commit();
                _unitOfWork.Dispose();
                return new CitaResponse() { Message = $"Se Registro" };

            }
            else
            {
                return new CitaResponse() { Message = $"No  Registro Compa" };
            }
        }


    }

    public class CitaRequest
    {
        public int Id { get; set; }
        public DateTime fecha = new DateTime();
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }

        public CitaRequest( Medico medico , Paciente paciente)
        {
            this.Medico = medico;
            this.Paciente = paciente;
        }

    }

    public class CitaResponse
    {
        public string Message { get; set; }
    }
}
