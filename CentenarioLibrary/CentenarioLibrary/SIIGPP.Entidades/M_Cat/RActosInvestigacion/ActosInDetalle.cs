using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.RActosInvestigacion
{
    public class ActosInDetalle
    {
        public Guid IdActosInDetetalle { get; set; }
        public Guid RActosInvestigacionId { get; set; }
        public RActoInvestigacion RActosInvestigacion { get; set; }
        public string Servicio { get; set; }
        public string ServicioNM { get; set; }
        public string Status { get; set; }
        public string TextoFinal { get; set; }
        public string FechaRecibido { get; set; }
        public string FechaAceptado { get; set; }
        public string FechaFinalizado { get; set; }
        public string FechaEntregado { get; set; }
        public DateTime UltmimoStatus { get; set; }
        public string Respuesta { get; set; }
        public string Conclusion { get; set; }
        public DateTime FechaSys { get; set; }
        public string UDirSubPro { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public string UAgencia { get; set; }
    }
}
