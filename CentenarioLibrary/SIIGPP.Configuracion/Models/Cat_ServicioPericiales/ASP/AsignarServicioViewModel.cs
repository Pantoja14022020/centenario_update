using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_ServicioPericiales.ASP
{
    public class AsignarServicioViewModel
    {
        public Guid IdASP { get; set; } 
        public Guid AgenciaId { get; set; }
        public string NombreAgencia { get; set; }
        public Guid DSPId { get; set; }
        public string NombreDirSub { get; set; }
        public Guid ServicioPericialId { get; set; }
        public string NombreServicio { get; set; }
        public string NumeroDistrito { get; set; }
        public string NombreDistrito { get; set; }
        public string EnMateriaDe { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
    }
}
