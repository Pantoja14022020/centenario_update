using SIIGPP.Entidades.M_JR.RExpediente;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_JR.RNotificacion
{
    public class RegistroNotificacion
    {
        public Guid IdRegistroNotificaciones { get; set; }
        public Guid ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

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
