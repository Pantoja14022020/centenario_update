using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra
{
    public class ErrorReplicacion
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
    }
}
