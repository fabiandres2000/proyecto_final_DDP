﻿using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class DetectarEnfermedadServiceApp
    {
        readonly IUnitOfWork _unitOfWork;



        public DetectarEnfermedadServiceApp(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public DetectarResponseapp detectar(DetectarRequestapp request)
        {
            DetectarEnfermedadService detectarEnfermedad = new DetectarEnfermedadService();
            DetectarEnfermedadRequest enfermedadRequest = new DetectarEnfermedadRequest();
            var enfermedades = _unitOfWork.EnfermedadRepository.GetAll().ToList();
            enfermedadRequest.Enfermedades = enfermedades;
            //asociar sintomas a cada enfermedad//////////////////////////////////////////////////////////////////////////////////////////
            foreach (var Item in enfermedades)
            {
                Console.WriteLine(Item.Nombre+" "+Item.Id);
                var enfermedadsintoma = _unitOfWork.IEnfermedadSintoma.FindBy(p=> p.Enfermedad==Item,includeProperties: "Sintoma,Enfermedad").ToList();  
                Console.WriteLine("sintomas asociadas de " + Item.Nombre);
                foreach (var item2 in enfermedadsintoma) {
                    var sintoma = _unitOfWork.SintomaRepository.FindFirstOrDefault(p => p.Codigo == item2.Sintoma.Codigo);
                    Console.WriteLine(sintoma.Descripcion);
                    Item.Sintomas.Add(sintoma);
                }
                Console.WriteLine("---------------------------------------");
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            enfermedadRequest.Paciente = _unitOfWork.IPacienteRepository.FindFirstOrDefault(p=> p.Identificacion == request.IdPaciente);
            //////////////////buscar sintomas////////////////////////////////////////////////////////////////////////////////////////////
            foreach(var item3 in request.Descipciones)
            {
                var sintomapaciente = _unitOfWork.SintomaRepository.FindFirstOrDefault(p => p.Descripcion==(item3));
                if (sintomapaciente!=null) {
                    enfermedadRequest.Sintomas.Add(sintomapaciente);
                    Console.WriteLine(sintomapaciente.Descripcion);
                }
            } 
            Console.WriteLine("numero de sistomas del paciente : "+enfermedadRequest.Sintomas.Count());
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////calcilar probabilidad enfermedad////////////////////////////////////////////////////////////////////////////////////////
            if (enfermedadRequest.Sintomas.Count()>1)
            {             
                var deteccion =detectarEnfermedad.CalcularProbabilidad(enfermedadRequest);
                return new DetectarResponseapp() { Message = $"se le manda tratamiento", enfermedad = deteccion.Enfermedad, diagnostico = deteccion.Diagnostico};
            }
            else
            {
                return new DetectarResponseapp() { Message = $"sus sintomas no estan asociados a una enfermedad pulmonar"};
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*
            if (deteccion.Enfermedad.Gravedad == "ALTA" || deteccion.Enfermedad.Gravedad == "MEDIA")
            {
                CrearCitaService crearCitaService = new CrearCitaService(_unitOfWork);
                CitaRequest citaRequest = new CitaRequest(enfermedadRequest.Paciente.Medico, enfermedadRequest.Paciente);
                crearCitaService.CrearCita(citaRequest);
                ConsultarMedicoService consultarMedico = new ConsultarMedicoService(_unitOfWork);
                var medicoPaciente=consultarMedico.GetId(enfermedadRequest.Paciente.Medico.Id);
                medicoPaciente.Diagnosticos.Add(deteccion.
                Diagnostico);
                _unitOfWork.IMedicoRepository.Edit(medicoPaciente);

                //llamar diagnitco service
                CrearDiagnosticoService crearDiagnosticoService = new CrearDiagnosticoService(_unitOfWork);
                DiagnosticoRequest diagnosticoRequest = new DiagnosticoRequest(deteccion.Diagnostico.Descripcion,deteccion.Diagnostico.Enfermedad,deteccion.Diagnostico.Paciente,deteccion.Diagnostico.Medico,deteccion.Diagnostico.Estado);

                crearDiagnosticoService.CrearDiagnostico(diagnosticoRequest);
                _unitOfWork.Commit();
                _unitOfWork.Dispose();
                return new DetectarResponse() { Message = $"se registro cita" };
            }
            else
            {

                return new DetectarResponse() { Message = $"se le manda tratamiento" };
                //var consultar = _unitOfWork.IEnfermedadTratamientoRepository.FindBy(c => c.enfermedad == deteccion.Enfermedad);
          
                //var consulta = _unitOfWork.TratamientoRepository.FindBy()
            }*/

        }


    }

    public class DetectarRequestapp
    {
        public string IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
        public List<string> Descipciones { get; set; }
       
    }

    public class DetectarResponseapp
    {
        public string Message { get; set; }
        public Enfermedad enfermedad { get; set; }
        public Diagnostico diagnostico { get; set;}
    }
}
