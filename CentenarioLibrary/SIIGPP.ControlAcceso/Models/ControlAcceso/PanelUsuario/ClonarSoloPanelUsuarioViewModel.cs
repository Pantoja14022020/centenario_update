using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.PanelUsuario
{
    public class ClonarSoloPanelUsuarioViewModel
    {
        public Guid UsuarioId { get; set; } 
        public Guid IdDistrito { get; set; }
        public Guid PanelControlId { get; set; }
    }
}
