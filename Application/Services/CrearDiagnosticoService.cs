using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CrearDiagnosticoService
    {
        readonly IUnitOfWork _unitOfWork;


        public CrearDiagnosticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public DiagnosticoResponse CrearDiagnostico(DiagnosticoRequest request)
        {
            Diagnostico diagnostico = new Diagnostico();
            diagnostico.Estado = request.Estado;
            diagnostico.Descripcion = request.Descripcion;
            diagnostico.Enfermedad = request.Enfermedad;
            diagnostico.Fecha = DateTime.Now;
            diagnostico.Medico = request.Medico;
            diagnostico.Paciente = request.Paciente;
            diagnostico.Guardar(diagnostico);
            _unitOfWork.DiagnosticoRepository.Add(diagnostico);
            _unitOfWork.Commit();
            _unitOfWork.Dispose();
            return new DiagnosticoResponse() { Message = $"Se Registro CorrectaMente" };

        }
    }

    public class DiagnosticoRequest
    {
        public string Descripcion { get; set; }
        public Enfermedad Enfermedad { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime Fecha;
        public string Estado { get; set; }
        //public string RecomendacionMedica { get; set; }

        public DiagnosticoRequest(string descripcion, Enfermedad enfermedad, Paciente paciente, Medico medico,string estado){

            this.Descripcion = descripcion;
            this.Enfermedad = enfermedad;
            this.Paciente = paciente;
            this.Medico = medico;
            this.Estado = estado;
        }

    }

    public class DiagnosticoResponse
    {
        public string Message { get; set; }
    }

}
