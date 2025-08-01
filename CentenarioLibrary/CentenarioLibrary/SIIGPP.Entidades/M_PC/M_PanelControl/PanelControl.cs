
using SIIGPP.Entidades.M_ControlAcceso.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Panel.M_PanelControl
{
    public class PanelControl
    {
        public Guid Id { get; set; }
        public Guid Clave { get; set; }
    
        public  string Nombre { get; set; }
        public string Abrev { get; set; }
        public string Icono { get; set; }
        public Boolean Status { get; set; }
        public string Ruta { get; set; }
        public Boolean Activo { get; set; }
         
    
    }
}
