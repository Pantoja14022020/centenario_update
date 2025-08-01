using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.SolicitudesInteligencia
{
    public class AsignacionFullViewModel
    {
        public Guid IdSolicitudInteligenciaAsig { get; set; }
        public Guid SolicitudInteligenciaId { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid PeritoAsignadoPIId { get; set; }
        public string Mensaje { get; set; }
        public string Respuesta { get; set; }
        public Boolean StatusAutorizacion { get; set; }
        public String StatusMensaje { get; set; }
        public string Fecha { get; set; }
        public DateTime FechaSys { get; set; }
        public string uDistrito { get; set; }
        public string uSubproc { get; set; }
        public string uAgencia { get; set; }
        public string uUsuario { get; set; }
        public string uPuesto { get; set; }
        public string uModulo { get; set; }

    }
}
