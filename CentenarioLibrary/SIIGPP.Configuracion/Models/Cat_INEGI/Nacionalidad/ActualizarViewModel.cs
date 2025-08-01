using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_INEGI.Nacionalidad
{
    public class ActualizarViewModel
    {
        public Guid IdNacionalidad { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}
