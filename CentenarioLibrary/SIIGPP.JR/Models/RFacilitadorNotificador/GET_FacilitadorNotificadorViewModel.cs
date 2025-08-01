 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RFacilitadorNotificador
{
    public class GET_FacilitadorNotificadorViewModel
    {
        public Guid IdFacilitadorNotificador { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid IdUsuario { get; set; }
        public Boolean StatusAsignado { get; set; }
        public Boolean StatusActivo { get; set; }
        public string NombreModulo { get; set; }
        public string NombreUsuario { get; set; }
        public string puesto { get; set; }
        public Guid IdAgencia { get; set; }
    }
    public class GET_FacilitadorNotificadorViewModel1
    {
        public Guid ModuloServicioId { get; set; }
        public Guid IdFacilitadorNotificador { get; set; }
        public string NombreUsuario { get; set; }
        public string puesto { get; set; }
    }


}
