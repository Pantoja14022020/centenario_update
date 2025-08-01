using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.Menu
{
    public class SubModuloRol
    {
       public Guid IdSubModuloRol { get; set; }
       public Guid ModuloId { get; set; }
       public Modulo Modulo { get; set; }
       public Guid ModuloRolId { get; set; }
       public ModuloRol ModuloRol { get; set; }
       public int Orden { get; set; }
       public Boolean Activo{ get; set; }
    }
}
