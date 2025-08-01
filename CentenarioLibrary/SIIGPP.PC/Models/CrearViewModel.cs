using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Panel.Models
{
    public class CrearViewModel
    {
        public string Nombre { get; set; }
        public string Abrev { get; set; }
        public string Icono { get; set; }
        public Boolean Status { get; set; }
        public string Ruta { get; set; }
        public Guid Clave { get; set; }
    }
}
