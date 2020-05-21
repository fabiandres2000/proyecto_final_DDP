using NUnit.Framework;
using System.Collections;
using Domain.Entity;
using Domain.Service;
using Application.Services;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Aplication.Test
{
    public class TestDetectarEnfermedad
    {
        EpsContext _context;
        UnitOfWork unitOfWork;
        PacienteRequest paciente;
        MedicoRequest medico;
        SintomaRequest sintoma1;
        SintomaRequest sintoma2;
        SintomaRequest sintoma3;
        SintomaRequest sintoma4;
        SintomaRequest sintoma5;
        SintomaRequest sintoma6;
        SintomaRequest sintoma7;
        EnfermedadRequest enfermedad1;
        EnfermedadRequest enfermedad2;
        EnfermedadRequest enfermedad3;
        EnfermedadSintomaRequest enfermedad1sintoma1;
        EnfermedadSintomaRequest enfermedad1sintoma6;
        EnfermedadSintomaRequest enfermedad1sintoma7;
        EnfermedadSintomaRequest enfermedad2sintoma1;
        EnfermedadSintomaRequest enfermedad2sintoma2;
        EnfermedadSintomaRequest enfermedad2sintoma3;
        EnfermedadSintomaRequest enfermedad2sintoma4;
        EnfermedadSintomaRequest enfermedad3sintoma4;
        EnfermedadSintomaRequest enfermedad3sintoma5;
        EnfermedadSintomaRequest enfermedad3sintoma6;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EpsContext>().UseInMemoryDatabase("EpsBD3").Options;
            _context = new EpsContext(options);
            unitOfWork = new UnitOfWork(_context);

            paciente = new PacienteRequest()
            {
                Identificacion = "1234",
                Nombres = "fabian andres",
                Apellidos = "quintero mendez",
                CorreoElectronico = "232@gmail.com",
                Direccion = "valledupar cesar",
                Edad = 24,
                Estrato = 2,
                Sexo = "M",
                Telefono = "3173345666"
            };
            CrearPacienteService service = new CrearPacienteService(unitOfWork);
            var response = service.CrearPaciente(paciente);

            medico = new MedicoRequest()
            {
                Identificacion = "123",
                Nombres = "fabian",
                Apellidos = "quintero",
                CorreoElectronico = "34563@gmail.com",
                Direccion = "valledupar",
                Edad = 23,
                Especializacion = "pulmonar",
                Estrato = 5,
                Sexo = "M",
                Telefono = "3123345666"
            };

            CrearMedicoService service2 = new CrearMedicoService(unitOfWork);
            var response2 = service2.CrearMedico(medico);


            sintoma1 = new SintomaRequest()
            {
                Codigo = "A1",
                Descripcion = "Tos"
            };

            sintoma2 = new SintomaRequest()
            {
                Codigo = "A2",
                Descripcion = "Fiebre"
            };

            sintoma3 = new SintomaRequest()
            {
                Codigo = "A3",
                Descripcion = "Secrecion Nasal"
            };

            sintoma4 = new SintomaRequest()
            {
                Codigo = "A4",
                Descripcion = "Tos Seca"
            };

            sintoma5 = new SintomaRequest()
            {
                Codigo = "A5",
                Descripcion = "Flemas"
            };

            sintoma6 = new SintomaRequest()
            {
                Codigo = "A6",
                Descripcion = "Dificultad Para Respirar"
            };

            sintoma7 = new SintomaRequest()
            {
                Codigo = "A7",
                Descripcion = "Dolor En El Pecho"
            };

            CrearSintomaService service3 = new CrearSintomaService(unitOfWork);
            var response3 = service3.CrearSitoma(sintoma1);
            var response4 = service3.CrearSitoma(sintoma2);
            var response5 = service3.CrearSitoma(sintoma3);
            var response6 = service3.CrearSitoma(sintoma4);
            var response7 = service3.CrearSitoma(sintoma5);
            var response8 = service3.CrearSitoma(sintoma6);
            var response9 = service3.CrearSitoma(sintoma7);

            enfermedad1 = new EnfermedadRequest()
            {
                Codigo = "A321",
                Nombre = "cancer-pulmonar",
                Gravedad = "GRAVE",
                Tipo = "PULMONAR",
            };

            enfermedad2 = new EnfermedadRequest()
            {
                Codigo = "A322",
                Nombre = "gripa",
                Gravedad = "LEVE",
                Tipo = "PULMONAR",
            };
            enfermedad3 = new EnfermedadRequest()
            {
                Codigo = "A323",
                Nombre = "coronavirus-F",
                Gravedad = "GRAVE",
                Tipo = "PULMONAR",
            };

            CrearEnfermedadService service4 = new CrearEnfermedadService(unitOfWork);
            var response10 = service4.CrearEnfermedad(enfermedad1);
            var response11 = service4.CrearEnfermedad(enfermedad2);
            var response12 = service4.CrearEnfermedad(enfermedad3);


          
            enfermedad1sintoma1 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A321",
                IDsintoma = "A1",
            };
            enfermedad1sintoma6 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A321",
                IDsintoma = "A6",
            };
            enfermedad1sintoma7 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A321",
                IDsintoma = "A7",
            };
            CrearEnfermedadSintomaService service5 = new CrearEnfermedadSintomaService(unitOfWork);
            var response13 = service5.CrearEnfermedadSitoma(enfermedad1sintoma1);
            var response14 = service5.CrearEnfermedadSitoma(enfermedad1sintoma6);
            var response15 = service5.CrearEnfermedadSitoma(enfermedad1sintoma7);

            enfermedad2sintoma1 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A322",
                IDsintoma = "A1",
            };
            enfermedad2sintoma2 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A322",
                IDsintoma = "A2",
            };
            enfermedad2sintoma3 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A322",
                IDsintoma = "A3",
            };
            enfermedad2sintoma4 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A322",
                IDsintoma = "A4",
            };
            CrearEnfermedadSintomaService service6 = new CrearEnfermedadSintomaService(unitOfWork);
            var response16 = service5.CrearEnfermedadSitoma(enfermedad2sintoma1);
            var response17 = service5.CrearEnfermedadSitoma(enfermedad2sintoma2);
            var response18 = service5.CrearEnfermedadSitoma(enfermedad2sintoma3);
            var response19 = service5.CrearEnfermedadSitoma(enfermedad2sintoma4);


            enfermedad3sintoma4 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A323",
                IDsintoma = "A4",
            };
            enfermedad3sintoma5 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A323",
                IDsintoma = "A5",
            };
            enfermedad3sintoma6 = new EnfermedadSintomaRequest()
            {
                IDenfermedad = "A323",
                IDsintoma = "A6",
            };
          
            CrearEnfermedadSintomaService service7 = new CrearEnfermedadSintomaService(unitOfWork);
            var response20 = service7.CrearEnfermedadSitoma(enfermedad2sintoma1);
            var response21 = service7.CrearEnfermedadSitoma(enfermedad2sintoma2);
            var response22 = service7.CrearEnfermedadSitoma(enfermedad2sintoma3);
          

        }

        [Test]

        public void DetectarCancerPulmonar()
        {        
            DetectarRequestapp request = new DetectarRequestapp();
            request.IdPaciente = "1234";
            List<string> des = new List<string>() {"Tos","Dificultad Para Respirar","Dolor En El Pecho"};
            request.Descipciones = des;
            DetectarEnfermedadServiceApp service8 = new DetectarEnfermedadServiceApp(unitOfWork);
            DetectarResponseapp responseapp = new DetectarResponseapp();
            responseapp = service8.detectar(request);
            Assert.AreEqual(responseapp.enfermedad.Nombre,"cancer-pulmonar");
            Console.WriteLine("su diagnostico es " + responseapp.diagnostico.Descripcion);
        }

        [Test]
        public void DetectarAGripa()
        {
            DetectarRequestapp request = new DetectarRequestapp();
            request.IdPaciente = "1234";
            List<string> des = new List<string>() {"Tos", "Fiebre", "Secrecion Nasal"};
            request.Descipciones = des;
            DetectarEnfermedadServiceApp service9 = new DetectarEnfermedadServiceApp(unitOfWork);
            DetectarResponseapp responseapp = new DetectarResponseapp();
            responseapp = service9.detectar(request);
            Assert.AreEqual(responseapp.enfermedad.Nombre, "gripa");
            Console.WriteLine("su diagnostico es "+ responseapp.diagnostico.Descripcion);
        }

        [Test]
        public void DetectarNinguna()
        {
            DetectarRequestapp request = new DetectarRequestapp();
            request.IdPaciente = "1234";
            List<string> des = new List<string>() { "Fiebre", "nada", "nada" };
            request.Descipciones = des;
            DetectarEnfermedadServiceApp service10 = new DetectarEnfermedadServiceApp(unitOfWork);
            DetectarResponseapp responseapp = new DetectarResponseapp();
            responseapp = service10.detectar(request);
            Assert.AreEqual(responseapp.Message, "sus sintomas no estan asociados a una enfermedad pulmonar");
        }
    }
}
