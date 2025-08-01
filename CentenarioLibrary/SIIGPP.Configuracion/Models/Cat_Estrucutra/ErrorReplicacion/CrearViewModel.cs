using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Estrucutra.ErrorReplicacion
{
    public class CrearViewModel
    {
        public Guid RegistroErrorId { get; set; }
        public Guid DistritoId { get; set; }
        public string NombreDistrito { get; set; }
        public string Modulo { get; set; }
        public string Proceso { get; set; }
        public Boolean Status { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
