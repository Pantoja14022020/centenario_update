using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RExpediente;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_JR.RRegistro
{
    public class RegistroConclusion
    {
        public Guid IdRegistroConclusion { get; set; }

        public Guid EnvioId { get; set; }
        public Envio Envio { get; set; }
        public string Conclusion { get; set; }
        public string Asunto { get; set; }
        public string Texto { get; set; }
        public DateTime FechaHora { get; set; }

        public string Solicitates { get; set; }
        public string Reuqeridos { get; set; }
        public string StatusGeneral { get; set; }

        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }



    }
}
