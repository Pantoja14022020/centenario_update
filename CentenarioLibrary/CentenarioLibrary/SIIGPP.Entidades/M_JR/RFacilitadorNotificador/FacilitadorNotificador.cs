using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

using System;
using System.Collections.Generic; 
using System.Text;

namespace SIIGPP.Entidades.M_JR.RFacilitadorNotificador
{
    public class FacilitadorNotificador
    {
 
        public Guid IdFacilitadorNotificador { get; set; }
        public Guid ModuloServicioId { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
        public Boolean StatusAsignado { get; set; }
        public Boolean StatusActivo { get; set; }


    }


}
