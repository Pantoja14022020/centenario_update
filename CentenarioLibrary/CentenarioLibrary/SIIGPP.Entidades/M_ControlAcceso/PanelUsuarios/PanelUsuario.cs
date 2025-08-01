using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using SIIGPP.Entidades.M_Panel.M_PanelControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.PanelUsuarios
{
   public class PanelUsuario
    {
        public Guid IdPanelUsuario { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuarios { get; set; }
        public Guid PanelControlId { get; set; }
        public PanelControl PanelControl { get; set; }
    }
}
