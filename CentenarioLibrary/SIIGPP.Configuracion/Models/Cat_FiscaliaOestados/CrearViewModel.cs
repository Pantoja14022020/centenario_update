using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_FiscaliaOestados
{
    public class CrearViewModel
    {
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
    }
}
