using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
     public class Cita : Entity<int>
    {
        public DateTime fecha = new DateTime();
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }  
    }
}
