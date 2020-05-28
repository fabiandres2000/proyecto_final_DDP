using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class ConsultarEnfermedadTratamientoService
    {
        readonly IUnitOfWork _unitOfWork;


        public ConsultarEnfermedadTratamientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public DetectarEnfermedadRequest GetTratamiento(string Codigo)
        {
            DetectarEnfermedadService detectarEnfermedad = new DetectarEnfermedadService();
            DetectarEnfermedadRequest enfermedadRequest = new DetectarEnfermedadRequest();
            var enfermedades = _unitOfWork.EnfermedadRepository.FindBy(includeProperties: "Tratamientos").ToList();
            enfermedadRequest.Enfermedades = enfermedades;
            //asociar tratamiento a cada enfermedad//////////////////////////////////////////////////////////////////////////////////////////
            foreach (var Item in enfermedadRequest.Enfermedades)
            {
                Console.WriteLine(Item.Nombre + " " + Item.Id);
                var enfermedadtratamiento = _unitOfWork.IEnfermedadTratamientoRepository.FindBy(p => p.enfermedad.Codigo == Item.Codigo, includeProperties: "tratamiento,enfermedad").ToList();
                Console.WriteLine("Tratamiento asociadas de " + Item.Nombre);
                foreach (var item2 in enfermedadtratamiento)
                {
                    //var sintoma = _unitOfWork.SintomaRepository.FindFirstOrDefault(p => p.Codigo.Equals(item2.Sintoma.Codigo));            
                    Console.WriteLine(item2.tratamiento.Descripcion);
                    Item.Tratamientos.Add(item2.tratamiento);
                }
                Console.WriteLine("---------------------------------------");
            }


            return enfermedadRequest; 
        }
    }

   
}
