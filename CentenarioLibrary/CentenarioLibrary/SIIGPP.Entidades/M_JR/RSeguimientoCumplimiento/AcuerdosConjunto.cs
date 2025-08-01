using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_JR.REnvio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento
{
   public  class AcuerdosConjunto
    {
        public Guid IdAC { get; set; }
        public Guid AcuerdoReparatorioId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public AcuerdoReparatorio AcuerdoReparatorio { get; set; }
        public ConjuntoDerivaciones ConjuntoDerivaciones { get; set; }
        [NotMapped]
        public int? CountSessionsA {  get; set; }
        [NotMapped]        
        public List<AcuerdoReparatorio> Attached {  get; set; }
        [NotMapped]
        public List<ConjuntoDerivaciones> Conjunt {  get; set; }
    }

    public class ACARRaw
    {
        //campos de entidad acuerdo reparatorio
        public Guid IdAcuerdoReparatorio { get; set; }
        public Guid EnvioId { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreSolicitante { get; set; }
        public string NombreRequerdio { get; set; }
        public string Delitos { get; set; }
        public string NUC { get; set; }
        public string NoExpediente { get; set; }
        public string StatusConclusion { get; set; }
        public string StatusCumplimiento { get; set; }
        public string TipoPago { get; set; }
        public string MetodoUtilizado { get; set; }
        public decimal MontoTotal { get; set; }
        public string ObjectoEspecie { get; set; }
        public int NoTotalParcialidades { get; set; }
        public int Periodo { get; set; }
        public DateTime? FechaCelebracionAcuerdo { get; set; }
        public DateTime? FechaLimiteCumplimiento { get; set; }
        public string StatusRespuestaCoordinadorJuridico { get; set; }
        public string RespuestaCoordinadorJuridico { get; set; }
        public DateTime? FechaHoraRespuestaCoordinadorJuridico { get; set; }
        public string StatusRespuestaAMP { get; set; }
        public string RespuestaAMP { get; set; }
        public DateTime? FechaRespuestaAMP { get; set; }
        public string TextoAR { get; set; }
        public int Sise { get; set; }
        public DateTime? Fechasise { get; set; }
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }

        //campos de entidad acuerdos_conjuntos
        [Key]
        public Guid IdAC { get; set; }
        public Guid AcuerdoReparatorioId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        [NotMapped]
        public List<AcuerdoReparatorio> Attached { get; set; }

    }
}
