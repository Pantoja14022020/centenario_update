using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Generales
{
    public class RangoEdad
    {
        public Guid IdRangoEdad { get; set; }
        public string Rango { get; set; } 
        public Boolean Activo { get; set; }
        public int OrdenEdad { get; set; }

    }
}
