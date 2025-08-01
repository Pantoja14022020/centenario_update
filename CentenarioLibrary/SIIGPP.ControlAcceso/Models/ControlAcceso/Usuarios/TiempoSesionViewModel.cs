using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios
{
    public class TiempodeSesionViewModel
    {
        public Guid idTiempo { get; set; } 
        public int CerrarSesionMinutos { get; set; }
        public int AvisoSesionMinutos { get; set; }
    }
}
