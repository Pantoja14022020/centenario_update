using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.SolicitudesInteligencia
{
    public class ActualizarSolicitiudViewModel
    {
        public Guid IdSolicitudInteligencia { get; set; }
        public Boolean StatusAutorizacion { get; set; }
        public String StatusMensaje { get; set; }
        public string Respuesta { get; set; }
    }
}
