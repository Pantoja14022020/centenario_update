using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Panel.Models
{
    public class ControlDistritoViewModel
    {
        public Guid IdControlDistrito { get; set; }
        public string Direccion { get; set; }
        public string NombreDistrito { get; set; }
        public Guid DisId { get; set; }
    }
}
