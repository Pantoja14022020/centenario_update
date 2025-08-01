using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class EstadisticaViewModelMicrodato
    {
        public string nuc { get; set; }
        public string Agencia { get; set; }
        public string MunicipioAgencia { get; set; }
        public DateTime FechaInicioCarpeta { get; set; }
        public DateTime FechaHechos { get; set; }
        public string StatusCarpeta { get; set; }
        public string Rbreve { get; set; }
        public Guid Rhechoid { get; set; }
        public Guid Ratencion { get; set; }
        public Guid PersonaId { get; set; }
        public string EntidadFHechos { get; set; }
        public string MunicipioHechos { get; set; }
        public string NombreVictima { get; set; }
        public string ApellidoMaVictima { get; set; }
        public string ApellidoPaVictima { get; set; }
        public string FechaNacimiento { get; set; }
        public int EdadVictima {  get; set;  }
        public string RelacionVictima { get; set; }
        public string EscolaridadVictima { get; set; }
        public string OcupacionVictima { get; set; }
        public string NacionalidadVictima { get; set; }
        public string PaisVictima { get; set; }
        public string InicioDetenidi { get; set; }
        public string FormaComision { get; set; }
        public string ElementoParaComision { get; set; }


    }
}
