using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios
{
    public class ClavesAgenciaViewModel
    {
        public Guid IdClave { get; set; } 
        public string NuevaClave { get; set; } 
        public string NombreModulo { get; set; } 
        public string NombreSubdireccionAgencia { get; set; } 
        public string RolAdecuado { get; set; }

    }
}
