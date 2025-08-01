using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Estrucutra.ErrorReplicacion
{
    public class ActualizarViewModel
    {
        public Guid IdReplicacion { get; set; }
        public Guid RegistroErrorId { get; set; }
        public Guid DistritoId { get; set; }
        public string NombreDistrito { get; set; }
        public string Modulo { get; set; }
        public string Proceso { get; set; }
        public Boolean Status { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        [NotMapped]
        public Boolean ActualizaRegistro { get; set; }
    }
}
