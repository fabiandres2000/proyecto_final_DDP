using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;

namespace Domain.Factory
{
    public class FactoryCrearPersona // : IGenericFactory<Persona>
    {
      
        public Persona CreateEntity(int type, IPersonaRequest request)
        {
                Tipousuario Tipo = (Tipousuario)type;
                switch (Tipo)
                {
                    case Tipousuario.Administrador:
                        return new Administrador();
                    case Tipousuario.medico:
                        return new Medico(request.Especializacion);
                    case Tipousuario.Paciente:
                        return new Paciente(request.TipoAfiliacion, request.Medico);
                    default:
                        throw new ArgumentOutOfRangeException(message: "Tipo de usuaro No Válido.", innerException: null);
                }
        }

        public enum Tipousuario
        {
            Paciente = 1,
            medico = 2,
            Administrador = 3
        }
    }
}
