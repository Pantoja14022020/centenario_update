using SIIGPP.JR.Models.RAsignacionEnvios;
using SIIGPP.JR.Models.RCitatorioRecordatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSesion
{
    public class POST_CrearSCAViewModel
    {
        //********************************
        // SESION
        public Guid ModuloServicioId { get; set; }
        public Guid EnvioId { get; set; }
        public int NoSesion { get; set; }

        public DateTime FechaHoraCita { get; set; }

        //********************************
        // CITATORIOS
        public List<POST_CrearCitatorioRecordatorioViewModel> Citatorios { get; set; }
        //********************************
        // ASIGNACIONES
        public List<POST_AsignacionEnvioViewModel> Asignaciones { get; set; }
    }

    public class POST_CrearSCAcCViewModel
    {
        //********************************
        // SESION
        public Guid ModuloServicioId { get; set; }
        public Guid EnvioId { get; set; }
        public int NoSesion { get; set; }
        public string Solicitantes { get; set; }
        public string Requeridos { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }

        public DateTime FechaHoraCita { get; set; }
        public string Capturista { get; set; }
        public DateTime FechaHora { get; set; }
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }

        //********************************
        // CITATORIOS
        public List<POST_CrearCitatorioRecordatorioViewModel> Citatorios { get; set; }
        //********************************
        // ASIGNACIONES
        public List<POST_AsignacionEnvioViewModel> Asignaciones { get; set; }
    }

    public class SesionRecordatorioIntermediaViewModel
    {
         //-------------------Sesion-----------------------
         public Guid EnvioId { get; set; }
         public Guid ModuloServicioId { get; set; }
         public string Solicitantes { get; set; }
         public string Requeridos { get; set; }
         public string uf_Distrito { get; set; }
         public string uf_DirSubProc { get; set; }
         public string uf_Agencia { get; set; }
         public string uf_Modulo { get; set; }
         public string uf_Nombre { get; set; }
         public string uf_Puesto { get; set; }
         public DateTime FechaHora { get; set; }
         public string Capturista { get; set; }
         //-------------------Intermedia-----------------------
         public Guid IdSC { get; set; }
         public Guid ConjuntoDerivacionesId { get; set; }
         //------------------Recordatorio-----------------------
         public string NoExpediente { get; set; }
         public DateTime FechaHoraCita { get; set; }
         public int Duracion { get; set; }
         public string LugarCita { get; set; }
         public string dirigidoa_Nombre { get; set; }
         public string dirigidoa_Direccion { get; set; }
         public string dirigidoa_Telefono { get; set; }
         public string solicitadoPor { get; set; }
         public string solicitadoPor_Telefono { get; set; }
         public string Textooficio { get; set; }
         public string un_Modulo { get; set; }
         public string un_Nombre { get; set; }
         public string un_Puesto { get; set; }
         public string StatusEntrega { get; set; }
         public int NoCitatorio { get; set; }
 
    }
}