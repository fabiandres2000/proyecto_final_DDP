using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class EnfermedadTratamiento : Entity<int>
    {
        public Enfermedad enfermedad { get; set; }
        public Tratamiento tratamiento { get; set; }
    }
}
