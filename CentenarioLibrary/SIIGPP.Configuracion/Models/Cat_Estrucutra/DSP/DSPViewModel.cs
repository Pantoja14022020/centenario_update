using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Estrucutra.DSP
{
    public class DSPViewModel
    {
        public Guid IdDSP { get; set; }
        public Guid DistritoId { get; set; }
        public string NombreDistrito { get; set; }
        public string NombreSubDir { get; set; }
        public string Responsable { get; set; }
        public string Telefono { get; set; }
        public Boolean StatusInicioCarpeta { get; set; }
        public string Clave { get; set; }
        public string Tipo { get; set; }
        public string NombreSub { get; set; }
        public bool StatusDSP { get; set; }
    }
}
