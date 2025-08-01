using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Menus
{
    public class SeccionRolPanelViewModel
    {
        public Guid IdMenuPanel { get; set; }
        public Guid SeccionId { get; set; }
        public Guid RolId { get; set; }
        public Guid PanelControlId { get; set; } 
        public string Descripcion { get; set; }
    }
}
