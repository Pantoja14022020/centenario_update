using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_INEGI.Municipio
{
    public class MunicipioViewModel
    {
        public int IdMunicipio { get; set; }
        public int EstadoId { get; set; }
        public string Nombre { get; set; }
        public  string NombreEstado { get; set; }
        public int Numero_Mpio { get; set; }

    }
}
