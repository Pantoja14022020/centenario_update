using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DiligenciasForaneas
{
    public class ActualizarViewModel
    {
        public Guid IdRDiligenciasForaneas { get; set; }
        public string StatusRespuesta { get; set; }
        public string Respuestas { get; set; }
        public string Respuesta { get; set; }
        public Guid AgenciaEnvia { get; set; }
    }
}
