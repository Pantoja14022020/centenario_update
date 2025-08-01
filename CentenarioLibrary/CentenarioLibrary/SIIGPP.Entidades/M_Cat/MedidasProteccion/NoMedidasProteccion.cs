using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.MedidasProteccion
{
    public class NoMedidasProteccion
    {   
        public Guid IdNoMedidasProteccion { get; set; }
        public Guid MedidasproteccionId { get; set; }
        public Medidasproteccion Medidasproteccion { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
