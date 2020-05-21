using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CrearEnfermedadSintomaService
    {
        readonly IUnitOfWork _unitOfWork;


        public CrearEnfermedadSintomaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public EnfermedadSintomaResponse CrearEnfermedadSitoma(EnfermedadSintomaRequest request)
        {
            //buscar enfermedad  y sintoma en los repositorios
            request.Sintoma  = _unitOfWork.SintomaRepository.FindFirstOrDefault(P => P.Codigo == request.IDsintoma);
            request.Enfermedad = _unitOfWork.EnfermedadRepository.FindFirstOrDefault(P => P.Codigo == request.IDenfermedad);
            ///////////////////////////////////////////////////////////
             
            EnfermedadSintoma NuevoEnfermedadSintoma = new EnfermedadSintoma();
                NuevoEnfermedadSintoma.Enfermedad = request.Enfermedad;
                NuevoEnfermedadSintoma.Sintoma = request.Sintoma;
                if (NuevoEnfermedadSintoma.Guardar(NuevoEnfermedadSintoma).Equals("se guardo todo cachon")) {  
                    _unitOfWork.IEnfermedadSintoma.Add(NuevoEnfermedadSintoma);
                    _unitOfWork.Commit();
                   
                    return new EnfermedadSintomaResponse() { Message = $"Se Registro" };
                }
                return new EnfermedadSintomaResponse() { Message = $"Llene Todos los campos" };
        }

    }

    public class EnfermedadSintomaResponse
    {
        public string Message { get; set; }
    }
    public class EnfermedadSintomaRequest
    {
        public string IDsintoma { get; set; }
        public string IDenfermedad { get; set; }
        public Sintoma Sintoma { get; set; }
        public Enfermedad Enfermedad { get; set; }

        public EnfermedadSintomaRequest()
        {
            Sintoma = null;
            Enfermedad = null;
        }
    }
}
