using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.Menu
{
    public class Seccion
    {
       public Guid IdSeccion { get; set; }
        public String Descripcion { get; set; }
        public Boolean Activo{ get; set; }
    }
}
