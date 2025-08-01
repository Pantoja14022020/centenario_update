using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_SpPi_Ligaciones
{
    public class SpPiLigacionesViewModel
    {
        public Guid IdSPPiligaciones { get; set; }
        public Guid DSPId { get; set; }
        public Guid PanelControlId { get; set; }
        public Boolean Direccion { get; set; }
        public string Dspn { get; set; }
        public string Paneln { get; set; }
        public Guid DistritoId { get; set; }
    }
}
