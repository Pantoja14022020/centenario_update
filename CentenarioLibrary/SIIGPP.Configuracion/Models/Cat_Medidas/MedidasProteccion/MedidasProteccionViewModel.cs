using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Medidas.MedidasProteccion
{
    public class MedidasProteccionViewModel
    {
        public Guid IdMedidasProteccionC { get; set; }
        public string Clave { get; set; }
        public string Clasificacion { get; set; }
        public string Descripcion { get; set; }
    }
}
