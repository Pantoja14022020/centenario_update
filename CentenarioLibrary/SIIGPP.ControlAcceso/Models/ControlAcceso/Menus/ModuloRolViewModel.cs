using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Menus
{
    public class ModuloRolViewModel
    {
        public Guid IdModuloRol { get; set; }
        public Guid MenuPanelId { get; set;}
        public string Icono { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }

    }
}
