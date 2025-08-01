using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RActosInvestigacion
{
    public class ActualizarViewModel
    {
        public Guid IdRActosInvestigacion { get; set; }
        public string FechaSolicitud { get; set; }
        public string Status { get; set; }
        public string Servicios { get; set; }
        public string Especificaciones { get; set; }
        public Boolean Cdetenido { get; set; }
        public string Respuestas { get; set; }
        public string NUC { get; set; }
        public string Textofinal { get; set; }
        public DateTime FechaSys { get; set; }
        public string UDirSubPro { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public string UAgencia { get; set; }
    }
}
