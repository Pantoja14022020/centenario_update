using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.ImpProceso
{
    public class CondImpProceso
    {
        public Guid idConImpProceso { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }

        public Guid RHechoId { get; set; }
        public RHecho  RHecho { get; set; }
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
        public string  PlazoResolverSituacionJuridica { get; set; }
        public DateTime FechaSys { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
    }
}
