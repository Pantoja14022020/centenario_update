using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSesion
{
    public class PUT_SesionViewModel
    {
        public Guid IdSesion { get; set; } 
        public string StatusSesion { get; set; }
        public string DescripcionSesion { get; set; }
        public string Asunto { get; set; }
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
