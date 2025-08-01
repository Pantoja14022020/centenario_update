using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Catalogos.Distrito
{
    public class ActualizarViewModel
    {
        public Guid IdDistrito { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public string Clave { get; set; }
        public Boolean StatusAsginacion { get; set; }
        public string Nombrejr { get; set; }
        public Guid distritoCnx { get; set; }
        public string UrlDistrito { get; set; }
    }
}
