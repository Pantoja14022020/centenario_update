using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.ImpProceso
{
    public class CrearViewModel
    {
        public Guid idConImpProceso { get; set; }
        public Guid PersonaId { get; set; }

        public Guid RHechoId { get; set; }
        public string ConduccionImputadoProceso { get; set; }

        public DateTime FechaHoraCitacion { get; set; }
        public DateTime FechahoraComparecencia { get; set; }
        public string OrdeAprehension { get; set; }
        public string FechaHoraAudienciaOrdenAprehencion { get; set; }
        public string AutoridadEjecutora { get; set; }
        public string ResultadoOrdenAprehension { get; set; }
        public DateTime FechaHoraEjecucionOrdenAprehecion { get; set; }
        public DateTime FechaHoraCancelacionOrdenAprehecion { get; set; }
        public Boolean OrdenReaprehesion { get; set; }
        public string PlazoResolverSituacionJuridica { get; set; }
        public DateTime FechaSys { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
    }
}
