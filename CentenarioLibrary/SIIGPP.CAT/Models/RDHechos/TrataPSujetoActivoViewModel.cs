using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class TrataPSujetoActivoViewModel
    {
        public string Mes { get; set; }
        public int Mesi { get; set; }
        public string nuc { get; set; }
        public string Agencia { get; set; }
        public string MunicipioAgencia { get; set; }
        public Guid Rhechoid { get; set; }
        public Guid Ratencion { get; set; }
        public Guid PersonaId { get; set; }
        public string NombreImputado { get; set; }
        public string ApellidoMaImputado { get; set; }
        public string ApellidoPaImputado { get; set; }
        public string Alias { get; set; }
        public string FechaNacimiento { get; set; }
        public int EdadImputado { get; set; }
        public string Sexoimputado { get; set; }
        public string GeneroImputado { get; set; }
        public string EstadoCivilImputado { get; set; }
        public string NacionalidadImputado { get; set; }
        public string EscolaridadImputado { get; set; }
        public string OcupacionImputado { get; set; }
        public string PaisImputado { get; set; }
        public string EntidadImputado { get; set; }
        public string MunicipioImputado { get; set; }

    }
}
