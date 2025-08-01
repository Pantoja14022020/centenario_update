using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.ControlDistrito
{
    public class ControlDistrito
    {
        public Guid IdControlDistrito { get; set; }
        public Guid DisId {  get; set; }
        public string Direccion { get; set; }
        public string NombreDistrito { get; set; }
    }
}
