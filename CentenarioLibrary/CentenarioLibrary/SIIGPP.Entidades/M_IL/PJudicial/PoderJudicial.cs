using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_IL.PJudicial
{
    public class PoderJudicial
    {
        public Guid IdSolicitud { get; set; }
        public string Estado { get; set; }
        public string NoOficio { get; set; }
        public string NUC { get; set; }
        public string TipoAudiencia { get; set; }
        public string SalaAsignada { get; set; }
        public string JuzgadoAsignado { get; set; }
        public string AsuntoRelacionado { get; set; }
    }
}
