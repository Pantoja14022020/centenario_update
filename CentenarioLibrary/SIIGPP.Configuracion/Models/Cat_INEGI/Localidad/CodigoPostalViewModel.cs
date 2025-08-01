using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_INEGI.Localidad
{
    public class CodigoPostalViewModel
    {
        
        
        public int CP { get; set; }
        public int idMunicipio { get; set; }
        public int idEstado { get; set; }

    }
    public class DatosCodigoPostalViewModel
    {


        public int CP { get; set; }
        public int idMunicipio { get; set; }
        public string municipio { get; set; }
        public int idEstado { get; set; }
        public string estado { get; set; }
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; }

    }
}
