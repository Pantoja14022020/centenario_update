using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_FiscaliaOestados
{
    public class FiscaliaOestadosViewModel
    {
        public Guid IdFiscaliaOestado { get; set; }
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
    }
}
