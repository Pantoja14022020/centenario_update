using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSeguimientoCumplimiento
{
    public class GET_AgendaViewModel
    {
        //Seguimiento
        public Guid IdSeguimientoCumplimiento { get; set; }
        public Guid AcuerdoReparatorioId { get; set; } 
        public int NoParcialidad { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaProrroga { get; set; }
        public decimal CantidadAPagar { get; set; }
        public string TipoPago { get; set; }
        public string ObjectoEspecie { get; set; }
        public String StatusPago { get; set; }

        public string Titulo { get; set; }
        public string Dirigidoa { get; set; }
        public string Direccion { get; set; }
        public string Solicitantes { get; set; }
        public string Requeridos { get; set; }



        public DateTime? FechaHoraCita { get; set; }
        public string Texto { get; set; }

        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }

        //Acuerdos 
        public Guid EnvioId { get; set; } 
        public string NombreSolicitante { get; set; }
        public string NombreRequerdio { get; set; }
        public string Delitos { get; set; }
        public string NUC { get; set; }
        public string NoExpediente { get; set; }
        public string StatusConclusion { get; set; }
        public string StatusCumplimiento { get; set; }
        public string TipoPagoA { get; set; }
        public string MetodoUtilizado { get; set; }
        public decimal MontoTotal { get; set; }
        public string ObjectoEspecieA { get; set; }
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



         

    }
}
