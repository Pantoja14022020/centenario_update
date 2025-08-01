using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;
using SIIGPP.Entidades.M_Panel.M_PanelControl;

namespace SIIGPP.Entidades.M_Configuracion.Cat_SpPi_Ligaciones
{
    public class SPPiligaciones
    {
        public Guid IdSPPiligaciones { get; set; }
        public Guid DSPId { get; set; }
        public DSP DSP { get; set; }
        public Guid PanelControlId { get; set; }
        public PanelControl PanelControl { get; set; }
        public Boolean Direccion { get; set; }
    }
}
