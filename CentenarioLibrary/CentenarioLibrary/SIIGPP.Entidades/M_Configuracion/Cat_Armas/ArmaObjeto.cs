using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Armas
{
    public class ArmaObjeto
    {
        public Guid IdArmaObjeto { get; set; }
        public string nombreAO { get; set; }
        public Guid ClasificacionArmaId { get; set; }
        public ClasificacionArma ClasificacionArma { get; set; }
        
    }
}
