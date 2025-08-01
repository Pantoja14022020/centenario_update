using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.Menu
{
    public class ModuloRol
    {
        public Guid IdModuloRol { get; set; }
        public Guid ModuloId { get; set; }
        public Modulo Modulo { get; set; }
        public Guid MenuPanelId { get; set; }
        public SeccionesRolPanel SeccionRolPanel { get; set; }
        public int Orden { get; set; }
        public Boolean Activo{ get; set; }
    }
}
