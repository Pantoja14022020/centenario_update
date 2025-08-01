using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.PanelUsuario
{
    public class PanelUsuarioViewModel
    {
        public Guid IdPanelUsuario { get; set; }
        public Guid UsuarioId { get; set; } 
        public Guid PanelControlId { get; set; }
        public  string NombrePanel { get; set; }
    }
}
