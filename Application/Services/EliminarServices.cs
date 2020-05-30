using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class EliminarServices
    {
        readonly IUnitOfWork _unitOfWork;


        public EliminarServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public EliminarResponse DeleteEnfermedad(int id)
        {

            Enfermedad enfermedad = _unitOfWork.EnfermedadRepository.Find(id);
            var enfermedadSintoma = _unitOfWork.IEnfermedadSintoma.FindBy(p => p.Enfermedad.Id == id, includeProperties: "Sintoma,Enfermedad").ToList();
            var enfermedadTratamiento = _unitOfWork.IEnfermedadTratamientoRepository.FindBy(p => p.enfermedad.Id == id, includeProperties: "tratamiento,enfermedad").ToList();
            if (enfermedad == null)
            {
                return new EliminarResponse() { Message = $"No Existe" };
            }
            else
            {
                _unitOfWork.EnfermedadRepository.Delete(enfermedad);
                if (enfermedadSintoma != null)
                {
                    _unitOfWork.IEnfermedadSintoma.DeleteRange(enfermedadSintoma);
                }
                if (enfermedadTratamiento != null)
                {
                    _unitOfWork.IEnfermedadTratamientoRepository.DeleteRange(enfermedadTratamiento);
                }

                _unitOfWork.Commit();
                return new EliminarResponse() { Message = $"Se Elimio" };
            }
        }
        public EliminarResponse DeleteSintoma(int id)
        {

            Sintoma sintoma = _unitOfWork.SintomaRepository.Find(id);
            var enfermedadSintoma = _unitOfWork.IEnfermedadSintoma.FindBy(p => p.Enfermedad.Id == id, includeProperties: "Sintoma,Enfermedad").ToList();
            if (sintoma == null)
            {
                return new EliminarResponse() { Message = $"No Existe" };
            }
            else
            {
                _unitOfWork.SintomaRepository.Delete(sintoma);
                if (enfermedadSintoma != null)
                {
                    _unitOfWork.IEnfermedadSintoma.DeleteRange(enfermedadSintoma);
                }
                _unitOfWork.Commit();
                return new EliminarResponse() { Message = $"Se Elimio" };
            }
        }

        public EliminarResponse ElminarEnfermedadSintoma(int id)
        {
            EnfermedadSintoma enfermedadSintoma = _unitOfWork.IEnfermedadSintoma.Find(id);
            if (enfermedadSintoma == null)
            {
                return new EliminarResponse() { Message = $"No Existe" };
            }
            else
            {
                _unitOfWork.IEnfermedadSintoma.Delete(enfermedadSintoma);
                _unitOfWork.Commit();
                return new EliminarResponse() { Message = $"Se Elimio" };
            }

        }

        public EliminarResponse ElminarEnfermedadTratamiento(int id)
        {
            EnfermedadTratamiento enfermedadTratamiento = _unitOfWork.IEnfermedadTratamientoRepository.Find(id);
            if (enfermedadTratamiento == null)
            {
                return new EliminarResponse() { Message = $"No Existe" };
            }
            else
            {
                _unitOfWork.IEnfermedadTratamientoRepository.Delete(enfermedadTratamiento);
                _unitOfWork.Commit();
                return new EliminarResponse() { Message = $"Se Elimio" };
            }

        }

        public EliminarResponse DeleteTratamiento(int id)
        {

            Tratamiento tratamiento = _unitOfWork.TratamientoRepository.Find(id);
            var enfermedadTratamiento = _unitOfWork.IEnfermedadTratamientoRepository.FindBy(p => p.enfermedad.Id == id, includeProperties: "tratamiento,enfermedad").ToList();

            if (tratamiento == null)
            {
                return new EliminarResponse() { Message = $"No Existe" };
            }
            else
            {
                _unitOfWork.TratamientoRepository.Delete(tratamiento);
                if (enfermedadTratamiento != null)
                {
                    _unitOfWork.IEnfermedadTratamientoRepository.DeleteRange(enfermedadTratamiento);
                }
                _unitOfWork.Commit();
                return new EliminarResponse() { Message = $"Se Elimio" };
            }
        }


        public EliminarResponse DeletePaciente(int id)
        {

            Paciente paciente = _unitOfWork.IPacienteRepository.Find(id);

            if (paciente == null)
            {
                return new EliminarResponse() { Message = $"No Existe" };
            }
            else
            {
                _unitOfWork.IPacienteRepository.Delete(paciente);
                _unitOfWork.Commit();
                return new EliminarResponse() { Message = $"Se Elimio" };
            }
        }

        public EliminarResponse DeleteMedico(int id)
        {

            Medico medico = _unitOfWork.IMedicoRepository.Find(id);

            if (medico == null)
            {
                return new EliminarResponse() { Message = $"No Existe" };
            }
            else
            {
                _unitOfWork.IMedicoRepository.Delete(medico);
                _unitOfWork.Commit();
                return new EliminarResponse() { Message = $"Se Elimio" };
            }
        }





    }

    public class EliminarResponse
    {
        public string Message { get; set; }
    }
}
