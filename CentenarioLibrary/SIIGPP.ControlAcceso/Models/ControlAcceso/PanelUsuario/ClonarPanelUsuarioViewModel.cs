using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.PanelUsuario
{
    public class ClonarPanelUsuarioViewModel
    {
        public Guid UsuarioId { get; set; } 
        public int Caso { get; set; }
        public Guid IdDistrito { get; set; }
        public Guid IdDistritoO { get; set; }
        public Guid IdDistritoD { get; set; }
    }
}
