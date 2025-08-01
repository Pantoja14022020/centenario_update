using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using SIIGPP.Entidades.M_Panel.M_PanelControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.Roles
{
    public class Rol
    {
        public Guid IdRol { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
   
        public ICollection<Usuario> usuarios { get; set; }

    }
}
