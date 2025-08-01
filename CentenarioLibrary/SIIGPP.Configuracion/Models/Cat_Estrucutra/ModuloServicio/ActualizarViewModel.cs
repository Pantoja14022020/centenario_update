using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Estrucutra.ModuloServicio
{
    public class ActualizarViewModel
    {
        public Guid IdModuloServicio { get; set; }
        public Guid AgenciaId { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public Boolean ServicioInterno { get; set; }
        public string distritoId { get; set; }
    }
}
