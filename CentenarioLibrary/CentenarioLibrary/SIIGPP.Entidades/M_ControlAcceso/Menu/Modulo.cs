using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.Menu
{
    public class Modulo
    {
       public Guid IdModulo { get; set; }
       public String Descripcion { get; set; }
       public String Icono { get; set; }
       public String Ruta { get; set; }
       public Boolean Activo{ get; set; }
    }
}
