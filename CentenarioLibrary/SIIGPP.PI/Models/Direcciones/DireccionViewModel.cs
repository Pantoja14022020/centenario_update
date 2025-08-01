using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.Direcciones
{
    public class DireccionViewModel
    {
        public Guid IdDireccion { get; set; }
        public Guid PIPersonaVisitaId { get; set; }
        public string Calle { get; set; }
        public int NoExterior { get; set; }
        public int NoInterior { get; set; }
        public string Ecalle1 { get; set; }
        public string Ecalle2 { get; set; }
        public string Referencia { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public int Cp { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
