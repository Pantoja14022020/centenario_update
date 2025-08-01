using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Estructura
{
    public class Distrito
    {
        public Guid IdDistrito { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public string Clave { get; set; }
        public Boolean StatusAsginacion { get; set; }
        public string Nombrejr { get; set; }
        public string UrlDistrito { get; set; }
    }
}
