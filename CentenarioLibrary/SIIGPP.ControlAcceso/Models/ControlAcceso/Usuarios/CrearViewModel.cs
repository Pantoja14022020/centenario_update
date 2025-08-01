using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios
{
    public class CrearViewModel
    {

   
        public Guid rolId { get; set; }  
        public Guid moduloServicioId { get; set; } 
        public string usuario { get; set; }
        public string nombre { get; set; } 
        public string direccion { get; set; }
        public string telefono { get; set; } 
        public string email { get; set; } 
        public string puesto { get; set; }  
        public string password { get; set; }
        public bool condicion { get; set; }
        public bool Titular { get; set; }
        public string ResponsableCuenta { get; set; }
    }
}
