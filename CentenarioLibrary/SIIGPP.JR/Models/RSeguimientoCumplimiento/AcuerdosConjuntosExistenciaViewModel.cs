using System;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIIGPP.Entidades.M_Cat.DDerivacion;

namespace SIIGPP.JR.Models.RSeguimientoCumplimiento
{
    public class AcuerdosConjuntosExistenciaViewModel
    {
        

        public Guid IdAC { get; set; }
        public Guid AcuerdoReparatorioId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public Guid EnvioId { get; set; }
        public string SolicitadosC { get; set; }
        public string RequeridosC { get; set; }
        public string DelitosC { get; set; }
        public string NombreS { get; set; }
        public string DireccionS { get; set; }
        public string TelefonoS { get; set; }
        public string ClasificacionS { get; set; }
        public string NombreR { get; set; }
        public string DireccionR { get; set; }
        public string TelefonoR { get; set; }
        public string ClasificacionR { get; set; }
        public string NombreD { get; set; }
        public string NoOficio { get; set; }
        public string ResponsableJR { get; set; }
        public string NombreSubDirigido { get; set; }

        public string NombreCompleto { get; set; }
        public Guid PersonaId { get; set; }
        public string Tipo { get; set; }
        public string Clasificacion { get; set; }
        public Guid IdRSolicitanteRequerido { get; set; }
    }
    public class ConjuntsWithAgreementViewModel
    {
        //campos de conjuntos
        public Guid ConjuntoDerivacionesId { get; set; }
        public Guid EnvioId { get; set; }
        public string SolicitadosC { get; set; }
        public string RequeridosC { get; set; }
        public string DelitosC { get; set; }
        public string NombreS { get; set; }
        public string DireccionS { get; set; }
        public string TelefonoS { get; set; }
        public string ClasificacionS { get; set; }
        public string NombreR { get; set; }
        public string DireccionR { get; set; }
        public string TelefonoR { get; set; }
        public string ClasificacionR { get; set; }
        public string NombreD { get; set; }
        public string NoOficio { get; set; }
        public string ResponsableJR { get; set; }
        public string NombreSubDirigido { get; set; }
        //campos de acuerdo reparatorio
        public Guid IdAcuerdoReparatorio { get; set; }
        public string StatusConclusion { get; set; }
        public string StatusCumplimiento { get; set; }
        public string TipoPago { get; set; }
        public string MetodoUtilizado { get; set; }
        public Decimal MontoTotal { get; set; }
        public string ObjetoEspecie { get; set; }
        public int NoTotalParcialidades { get; set; }
        public int Periodo { get; set; }
        public string NUC { get; set; }
        public string money { get; set; }
        public string species { get; set; }
        public DateTime? FechaCelebracionAcuerdo { get; set; }
        public DateTime? FechaLimiteCumplimiento { get; set; }
        public string StatusRespuestaCoordinadorJuridico { get; set; }
        public string RespuestaCoordinadorJuridico { get; set; }
        public DateTime? fechaHoraRespuestaCoordinadorJuridico { get; set; }
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
        public DateTime? Fechasise { get; set; }
        public int nosise { get; set; }
        public string TipoDocumento { get; set; }
        public int? CountSessionsA { get; set; }
        public Guid? Anexo { get; set; }
        public bool? StatusAnexo { get; set; }
        public List<AcuerdoReparatorio> Attached { get; set; }
        public List<ConjuntoDerivaciones> Conjunt {  get; set; }
    }
}
