using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Entidades.M_Configuracion.Cat_FiscaliaOestados
{
    public class FiscaliaOestado
    {
        public Guid IdFiscaliaOestado { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public int MunicipioId { get; set; }
        public Municipio Municipio { get; set; }        
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
    }
}
