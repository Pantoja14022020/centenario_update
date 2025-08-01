using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSesion
{
    public class GET_SesionConjuntoViewModel
    {
        public Guid IdSC { get; set; }
        public Guid SesionId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid EnvioId { get; set; }
        public int NoSesion { get; set; }
        public DateTime? FechaHoraSys { get; set; }
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
        public DateTime FechaHoraCita { get; set; }


    }
}
