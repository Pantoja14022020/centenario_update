using System;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios
{
    public class ControlDistritoViewModel
    {
        public Guid IdControlDistrito { get; set; }
        public Guid DisId { get; set; }
        public string Direccion { get; set; }
        public string NombreDistrito { get; set; }
    }
}
