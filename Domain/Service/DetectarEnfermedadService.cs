using System;
using System.Collections.Generic;
using System.Text;
using Domain.Base;
using System.Linq;

namespace Domain.Entity
{
    public class DetectarEnfermedadRequest
    {
        public Paciente Paciente { get; set; }
        public List<Sintoma> Sintomas = new List<Sintoma>();
        public List<Enfermedad> Enfermedades { get; set; }
    }

    public class DetectarEnfermedadResponse
    {
        public DetectarEnfermedadResponse(Enfermedad enfermedad,Diagnostico diagnostico)
        {
            Enfermedad = enfermedad;
            Diagnostico = diagnostico;

        }
        public DetectarEnfermedadResponse()
        {
            Enfermedad = null;
            Diagnostico = null;
        }

        public Enfermedad Enfermedad { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public bool EnfermedadDetectada => Enfermedad != null;
    }



    public class DetectarEnfermedadService : IDetectarEnfermedadService
    {
        private double Probabilidad { get; set; }
        DetectarEnfermedadRequest _request;
        Diagnostico _diagnostico = new Diagnostico();
        public DetectarEnfermedadResponse CalcularProbabilidad(DetectarEnfermedadRequest request)
        {
            int temp = 0;
            int suma = 0;
            _request = request;
            List<double> probabilidades = new List<double>();
            List<double> probabilidades2 = new List<double>();
            var pacientes = request.Paciente;
            if (pacientes != null)
            {
                foreach (var itemEnfermedad in request.Enfermedades)
                {
                    suma = 0;
                    temp = 0;
                    foreach (var itemSintomas in itemEnfermedad.Sintomas)
                    {
                        foreach (var itemSintomaPaciente in request.Sintomas)
                        {
                            if (itemSintomas.Descripcion.Equals(itemSintomaPaciente.Descripcion))
                            {
                                suma += 1;
                            }
                        }
                        temp += 1;
                    }
                    probabilidades.Add(suma);
                    double num = (double) suma/ temp;
                    double pro = (double)num*100;
                    probabilidades2.Add(pro);
                }

                var probabilidad = (double)probabilidades2.Max();
                Enfermedad enfermedad = Detectar(probabilidades);
               
                if (enfermedad != null)
                {
                    _diagnostico.Descripcion = ($"usted tiene {probabilidad}% de tener la enfermedad pulmonar de {enfermedad.Nombre}");
                    _diagnostico.Enfermedad = enfermedad;
                    _diagnostico.Fecha = new DateTime();
                    _diagnostico.Paciente = request.Paciente;
                    _diagnostico.Medico = request.Paciente.Medico;
                    _diagnostico.Estado = "pendiente";
                }
                else
                {
                    _diagnostico = null;
                }
                return new DetectarEnfermedadResponse(enfermedad, _diagnostico);
            }
            else
            {
                return new DetectarEnfermedadResponse();
            }
        }

        private Enfermedad Detectar(List<double> probabilidades)
        {
            int temporal = 0;
            double maximo = probabilidades.Max();
            foreach (var item in probabilidades)
            {
                if (item != maximo)
                {
                    temporal += 1;
                }
                else
                {
                    break;
                }
            }

            Enfermedad enfermedad = _request.Enfermedades.ElementAt(temporal);
            return enfermedad;
        }
    }
}
