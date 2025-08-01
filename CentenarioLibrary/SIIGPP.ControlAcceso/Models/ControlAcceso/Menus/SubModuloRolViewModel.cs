using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Menus
{
    public class SubModuloRolViewModel
    {
        public Guid IdSubModuloRol { get; set; }
        public Guid ModuloRolId { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Ruta { get; set; }
        public int Orden { get; set; }

    }
}
