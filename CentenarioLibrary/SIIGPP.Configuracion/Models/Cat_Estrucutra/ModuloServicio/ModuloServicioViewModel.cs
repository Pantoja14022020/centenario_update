using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Estrucutra.ModuloServicio
{
    public class ModuloServicioViewModel
    {
        public Guid IdModuloServicio { get; set; }
        public Guid DistritoId { get; set; }
        public Guid DSPId { get; set; }
        public Guid AgenciaId { get; set; } 
        public string  Agencia { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public Boolean ServicioInterno { get; set; }
        public bool Condicion { get; set; }
    }
    public class ModuloServicioErrorRepliViewModel
    {
        public Guid IdModuloServicio { get; set; }
        public Guid DistritoId { get; set; }
        public Guid DSPId { get; set; }
        public Guid AgenciaId { get; set; }
        public string Agencia { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public Boolean ServicioInterno { get; set; }
        public bool Condicion { get; set; }
        public string NombreDistrito { get; set; }
        public string NombreDSP { get; set; }
    }
}
