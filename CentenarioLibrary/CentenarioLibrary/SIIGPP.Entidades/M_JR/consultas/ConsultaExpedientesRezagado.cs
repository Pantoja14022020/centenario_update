using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_JR.consultas
{
    //Nueva entidad para la consulta de expedientes con rezagoi, vease la consulta en EnvioController
    public class ConsultaExpedientesRezagado
    {
        [Key]
        public Guid IdExpediente { get; set; }
        public string NoExpediente { get; set; }
        public DateTime? FechaRegistroExpediente { get; set; }
        public DateTime? FechaDerivacion { get; set; }
        public Guid IdEnvio { get; set; }
        public string StatusGeneral { get; set; }
        public string AutoridadqueDeriva { get; set; }
        public string nucg { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public Guid? IdConjuntoDerivaciones { get; set; }
        public Guid? IdSesion { get; set; }
        public DateTime? fechahorasesion { get; set; }
        public string StatusSesion { get; set; }
        public int? NoSesion { get; set; }
        public Guid? IdAcuerdoReparatorio { get; set; }
        public DateTime? FechaCelebracionAcuerdo { get; set; }
        public string Nombre { get; set; }
        public string StatusPago { get; set; }
        public DateTime? fechaseguimiento { get; set; }
    }
}
