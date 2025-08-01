using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RExpediente;
using System;
using System.Collections.Generic;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIIGPP.Entidades.M_JR.RAcuerdoReparatorio
{
    public class AcuerdoReparatorio
    {
        public Guid IdAcuerdoReparatorio { get; set; }
        public Guid EnvioId { get; set; }
        public Envio Envio { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreSolicitante { get; set; }
        public string NombreRequerdio { get; set; }
        public string Delitos { get; set; }
        public string NUC { get; set; }
        public string NoExpediente { get; set; }
        public string StatusConclusion  { get; set; } 
        public string StatusCumplimiento { get; set; }
        public string TipoPago { get; set; }
        public string MetodoUtilizado { get; set; }
        public decimal MontoTotal { get; set; }
        public string ObjectoEspecie { get; set; }
        public int NoTotalParcialidades { get; set; }
        public int Periodo { get; set; }
        public string MoneyChain { get; set; }
        public string SpeciesChain { get; set; }
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
        public Guid? Anexo { get; set; }
        public bool? StatusAnexo { get; set; }
        [NotMapped]
        public List<SeguimientoCumplimiento> Followup { get; set; }
    }
}
