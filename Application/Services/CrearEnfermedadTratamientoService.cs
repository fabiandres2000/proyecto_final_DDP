using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CrearEnfermedadTratamientoService
    {
        readonly IUnitOfWork _unitOfWork;


        public CrearEnfermedadTratamientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public EnfermedadTratamientoResponse CrearEnfermedadTratamiento(EnfermedadTratamientoRequest request)
        {
            request.tratamiento = _unitOfWork.TratamientoRepository.FindFirstOrDefault(p => p.Codigo == request.IDTratamiento);
            request.Enfermedad = _unitOfWork.EnfermedadRepository.FindFirstOrDefault(P => P.Codigo == request.IDenfermedad);
            if (request.tratamiento!=null && request.Enfermedad!=null) {
                EnfermedadTratamiento NuevoEnfermedadTratamiento = new EnfermedadTratamiento();

                NuevoEnfermedadTratamiento.enfermedad = request.Enfermedad;
                NuevoEnfermedadTratamiento.tratamiento = request.tratamiento;
                if (NuevoEnfermedadTratamiento.Guardar(NuevoEnfermedadTratamiento).Equals("se guardo todo cachon"))
                {
                    _unitOfWork.IEnfermedadTratamientoRepository.Add(NuevoEnfermedadTratamiento);
                    _unitOfWork.Commit();
                    return new EnfermedadTratamientoResponse() { Message = $"Se Registro" };
                }
                return new EnfermedadTratamientoResponse() { Message = $"Llene Todos los campos" };

            }
            else
            {
                return new EnfermedadTratamientoResponse() { Message = $"No Existe Fabian Cachon" };

            }

        }



    }

    public class EnfermedadTratamientoRequest
    {
        public int Id { get; set; }
        public string IDTratamiento{ get; set; }
        public string IDenfermedad { get; set; }
        public Tratamiento tratamiento { get; set; }
        public Enfermedad Enfermedad { get; set; }

    }

    public class EnfermedadTratamientoResponse
    {
        public string Message { get; set; }
    }
}
