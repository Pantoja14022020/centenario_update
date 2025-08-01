using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSesion
{
    public class POST_SesionViewModel
    {
        public Guid ModuloServicioId { get; set; }
        public Guid EnvioId { get; set; }
        public int NoSesion { get; set; }
        public Guid SesionId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public String Requeridos { get; set; }
        public String Solicitantes { get; set; }
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public string Capturista { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
