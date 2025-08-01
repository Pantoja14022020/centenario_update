using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_ControlAcceso.Roles;
using SIIGPP.Entidades.M_Panel.M_PanelControl;

namespace SIIGPP.Entidades.M_ControlAcceso.Menu
{
    public class SeccionesRolPanel
    {
        public Guid IdMenuPanel { get; set; }
        public Guid SeccionId { get; set; }
        public Seccion Seccion { get; set; }
        public Guid RolId { get; set; }
        public Rol Rol { get; set; }
        public Guid PanelControlId { get; set; }
        public PanelControl PanelControl { get; set; }
        public int Orden { get; set; }
        public Boolean Activo { get; set; }
    }
}
