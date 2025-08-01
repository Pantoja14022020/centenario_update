using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.NoEjerciciosApenal
{
    public class NoEjerciciosApenalViewModel
    {
        public Guid IdNoEjercicioApenal { get; set; }
        public Guid RHechoId { get; set; }
        public Boolean Op1 { get; set; }
        public Boolean Op2 { get; set; }
        public Boolean Op3 { get; set; }
        public Boolean Op4 { get; set; }
        public Boolean Op5 { get; set; }
        public DateTime FechaEntrevista { get; set; }
        public DateTime FechaHecho { get; set; }
        public string Delito { get; set; }
        public string Denunciante { get; set; }
        public string ArticulosCp { get; set; }
        public Boolean OficioQuerella { get; set; }
        public string ArticuloLegislador { get; set; }
        public Boolean TipoQuerella { get; set; }
        public DateTime TFechaQuerella { get; set; }
        public DateTime FFechaLimiteQI { get; set; }
        public DateTime FFechaInterposicionMp { get; set; }
        public string PunibilidadMinimo { get; set; }
        public string PunibilidadMaximo { get; set; }
        public Boolean TipoPrescripcion { get; set; }
        public Boolean Tipopena { get; set; }
        public string Ttipodelito { get; set; }
        public DateTime TAccionPenalFecha { get; set; }
        public string TUltimoActoInvestigacion { get; set; }
        public DateTime TOperacionAritmeticaFecha { get; set; }
        public int DiasTranscurridos { get; set; }
        public int MesesTranscurridos { get; set; }
        public int AnosTranscurridos { get; set; }
        public DateTime FechaUltimoActo { get; set; }
        public DateTime FechaEjercerAcion { get; set; }
        public DateTime FechaResolucionconsulta { get; set; }
        public Boolean Art123 { get; set; }
        public Boolean PerdonOfendido { get; set; }
        public int PONumeroFirmas { get; set; }
        public string PONombreFirmas { get; set; }
        public DateTime POFechaPerdon { get; set; }
        public Boolean POViolenciaMuMeOG { get; set; }
        public string POVReparacionDaño { get; set; }
        public DateTime POVFITratamiento { get; set; }
        public DateTime POVFFTratamiento { get; set; }
        public Boolean ExcluyenteDelito { get; set; }
        public string EDHipotesis { get; set; }
        public string EDRazonar { get; set; }
        public Boolean NoConstituyeDelito { get; set; }
        public string NCDElementospenal { get; set; }
        public string NCDRazonar { get; set; }
        public Boolean ExentoResponsabilidadPenal { get; set; }
        public string ERPCausaInculpabilidad { get; set; }
        public string ERPRazonar { get; set; }
        public Boolean ImputadoFallecio { get; set; }
        public string IFImputadoNombre { get; set; }
        public string IFFechaFallecimiento { get; set; }
        public string NumeroOficio { get; set; }

        public DateTime FechaSys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
    }
}
