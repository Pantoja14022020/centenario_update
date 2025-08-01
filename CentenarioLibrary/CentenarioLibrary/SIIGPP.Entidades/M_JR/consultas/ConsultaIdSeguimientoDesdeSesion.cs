using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_JR.consultas
{
    //Nueva entidad para la consulta de expedientes con rezagoi, vease la consulta en EnvioController
    public class ConsultaIdSeguimientoDesdeSesion
    {
        [Key]
        public Guid IdSeguimientoCumplimiento { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public Guid IdAC { get; set; }
        public Guid IdAcuerdoReparatorio { get; set; }
        public DateTime Fecha { get; set; }

    }
}
