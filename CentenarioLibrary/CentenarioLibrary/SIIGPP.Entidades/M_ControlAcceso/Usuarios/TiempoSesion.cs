using SIIGPP.Entidades.M_Configuracion.Cat_Estructura; 
using SIIGPP.Entidades.M_Panel.M_PanelControl; 
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_ControlAcceso.Roles;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_ControlAcceso.Usuarios
{
  public  class TiempoSesion
    {
        public Guid IdTiempo { get; set; }
        public int CerrarSesionMinutos { get; set; }
        public int AvisoSesionMinutos { get; set; }

    }
}
