using System;
using System.Collections.Generic;
using System.Linq;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using System.Threading.Tasks;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using System.ComponentModel.DataAnnotations;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;

namespace SIIGPP.JR.Models.RAcuerdoReparatorio
{
    public class GET_AcuerdoReparatorioViewModel
    {
        public Guid IdAcuerdoReparatorio { get; set; }
        public string TipoDocumento { get; set; }
        public Guid EnvioId { get; set; } 
        public string NombreSolicitante { get; set; }
        public string NombreRequerdio { get; set; }        
        public string Delitos { get; set; }
        public string NUC { get; set; }
        public string NoExpediente { get; set; }
        public string StatusConclusion { get; set; } 
        public string StatusCumplimiento { get; set; }
        public string TipoPago { get; set; }        
        public string MetodoUtilizado { get; set; }
        public Decimal MontoTotal { get; set; }
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
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public int nosise { get; set; }
        public DateTime? fechasise { get; set; }
        public string AutoridadqueDeriva { get; set; }
        public string uqe_Distrito { get; set; }
        public string uqe_DirSubProc { get; set; }
        public string uqe_Agencia { get; set; }
        public string uqe_Modulo { get; set; }
        public string uqe_Nombre { get; set; }
        public string uqe_Puesto { get; set; }
        public string StatusGeneral { get; set; }         
        public string RespuestaExpediente { get; set; }       
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }    
        
        public Guid? anexo { get; set; }
    }

    public class GET_ARJRViewModel
    {
        public Guid IdAcuerdoReparatorio { get; set; }
        public Guid EnvioId { get; set; }
        public string NombreSolicitante { get; set; }
        public string NombreRequerdio { get; set; }
        public string Delitos { get; set; }
        public string NUC {  get; set; }
        public string NoExpediente { get; set; }
        public string StatusConclusion { get; set; }
        public string StatusCumplimiento { get; set; }
        public string TipoPago { get; set; }
        public string MetodoUtilizado { get; set; }
        public Decimal MontoTotal { get; set; }
        public string ObjetoEspecie { get; set; }
        public int NoTotalParcialidades {  get; set; }
        public int Periodo {  get; set; }
        public DateTime? FechaCelebracionAcuerdo { get; set; }
        public DateTime? FechaLimiteCumplimiento { get; set; }
        public string StatusRespuestaCoordinadorJuridico { get; set; }
        public string RespuestaCoordinadorJuridico { get; set; }
        public DateTime? FechaHoraRespuestaCoordinadorJuridico { get; set; }
        public string StatusRespuestaAMP { get; set; }
        public string RespuestaAMP { get; set; }
        public DateTime? FechaRespuestaAMP { get; set; }
        public string TextoAR { get; set; }
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public int nosise { get; set; }
        public DateTime? fechasise { get; set; }
        public string TipoDocumento { get; set; }
        public string MoneyChain { get; set; }
        public string SpeciesChain { get; set; }
        public Guid? Anexo { get; set; }
        public bool? StatusAnexo { get; set; }        
    }
    public class GET_ARConjuntoViewModel
    {        
        //campos de entidad acuerdo reparatorio
        public Guid IdAcuerdoReparatorio { get; set; }
        public string TipoDocumento { get; set; }
        public Guid EnvioId { get; set; }
        public string NombreSolicitante { get; set; }
        public string NombreRequerdio { get; set; }
        public string Delitos { get; set; }
        public string NUC { get; set; }
        public string NoExpediente { get; set; }
        public string StatusConclusion { get; set; }
        public string StatusCumplimiento { get; set; }
        public string TipoPago { get; set; }
        public string MetodoUtilizado { get; set; }
        public Decimal MontoTotal { get; set; }
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
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public int nosise { get; set; }
        public DateTime? fechasise { get; set; }
        public string AutoridadqueDeriva { get; set; }
        public string uqe_Distrito { get; set; }
        public string uqe_DirSubProc { get; set; }
        public string uqe_Agencia { get; set; }
        public string uqe_Modulo { get; set; }
        public string uqe_Nombre { get; set; }
        public string uqe_Puesto { get; set; }
        public string StatusGeneral { get; set; }
        public string RespuestaExpediente { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }

        //campos de entidad acuerdo_conjuntos
        [Key]
        public Guid IdAC { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public List<AcuerdoReparatorio> Attached { get; set; }

    }

    public class GET_AcuerdoxDistrito
    {
        public Guid IdAcuerdoReparatorio { get; set; }
        public string TipoDocumento { get; set; }
        public Guid EnvioId { get; set; }
        public string NombreSolicitante { get; set; }
        public string NombreRequerdio { get; set; }
        public string Delitos { get; set; }
        public string NUC { get; set; }
        public string NoExpediente { get; set; }
        public string StatusConclusion { get; set; }
        public string StatusCumplimiento { get; set; }
        public string TipoPago { get; set; }
        public string MetodoUtilizado { get; set; }
        public Decimal MontoTotal { get; set; }
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
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public int nosise { get; set; }
        public DateTime? fechasise { get; set; }
        public string  AutoridadqueDeriva { get; set; }
        public string uqe_Distrito { get; set; }
        public string uqe_DirSubProc { get; set; }
        public string uqe_Agencia { get; set; }
        public string uqe_Modulo { get; set; } 
        public string uqe_Nombre { get; set; }
        public string uqe_Puesto { get; set; }
        public string StatusGeneral {  get; set; } 
        public string RespuestaExpediente {  get; set; } 
        public DateTime FechaRegistro {  get; set; } 
        public DateTime? FechaCierre {  get; set; } 
    }
}
