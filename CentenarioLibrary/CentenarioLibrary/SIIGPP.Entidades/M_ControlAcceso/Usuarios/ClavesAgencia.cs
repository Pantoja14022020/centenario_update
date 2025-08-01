using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Panel.M_PanelControl;
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_ControlAcceso.Roles;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_ControlAcceso.Usuarios
{
    public class ClavesAgencia
    {
        public Guid IdClave { get; set; }
        public string NuevaClave { get; set; }
        public string NombreModulo { get; set; }
        public string NombreSubdireccionAgencia { get; set; }
        public string RolAdecuado { get; set; }

    }
}