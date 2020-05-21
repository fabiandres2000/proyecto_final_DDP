using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Examen : Entity<int>
    {
        public enum Tipo
        {
            Espirometría,volumen_pulmonar,difusión_de_gases,prueba_de_esfuerzo
        } 
    }
}
