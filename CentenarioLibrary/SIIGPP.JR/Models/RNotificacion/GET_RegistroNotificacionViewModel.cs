using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RNotificacion
{
    public class GET_RegistroNotificacionViewModel
    {
        public Guid IdRegistroNotificaciones { get; set; }
        public Guid ExpedienteId { get; set; } 

        public string Asunto { get; set; }
        public string Texto { get; set; }
        public DateTime FechaHora { get; set; }

        public string Solicitates { get; set; }
        public string Reuqeridos { get; set; }

        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
    }
}
