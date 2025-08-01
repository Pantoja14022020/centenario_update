using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Roles
{
    public class CrearViewModel
    {
 
        public Guid panelcontrolId { get; set; } 
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}
