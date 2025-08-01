using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RCitatorioRecordatorio
{
    public class GET_CitatorioRecordatorioViewModel
    {
        public Guid IdRCitatoriosRecordatorios { get; set; }
        public Guid SesionId { get; set; }
        public Guid EnvioId { get; set; }
        public int NoSesion { get; set; }
        public string NoExpediente { get; set; }
        public DateTime FechaSys { get; set; }
        public DateTime fechaExpediente { get; set; }
        public DateTime fechaDerivacion { get; set; }
        public DateTime FechaHoraCita { get; set; }
        public int Duracion { get; set; }
        public string LugarCita { get; set; }
        public string dirigidoa_Nombre { get; set; }
        public string dirigidoa_Direccion { get; set; }
        public string dirigidoa_Telefono { get; set; }
        public string solicitadoPor { get; set; }
        public string solicitadoPor_Telefono { get; set; }
        public string Textooficio { get; set; }

        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public string un_Nombre { get; set; }
        public string un_Puesto { get; set; }
        public int NoCitatorio { get; set; }
        public int ContadorSMSS { get; set; }
        public int ContadorSMSR { get; set; }
        public Boolean StatusAsistencia { get; set; }
        public string StatusEntrega { get; set; }

        public string StatusSesion { get; set; }


    }

    public class GET_CitatorioRecordatorioSViewModel
    {
        public Guid IdRCitatoriosRecordatorios { get; set; }
        public Guid SesionId { get; set; }
        public Guid EnvioId { get; set; }
        public int NoSesion { get; set; }
        public string NoExpediente { get; set; }
        public DateTime FechaSys { get; set; }
        public DateTime fechaExpediente { get; set; }
        public DateTime fechaDerivacion { get; set; }
        public DateTime FechaHoraCita { get; set; }
        public int Duracion { get; set; }
        public string LugarCita { get; set; }
        public string dirigidoa_Nombre { get; set; }
        public string dirigidoa_Direccion { get; set; }
        public string dirigidoa_Telefono { get; set; }
        public string solicitadoPor { get; set; }
        public string solicitadoPor_Telefono { get; set; }
        public string Textooficio { get; set; }

        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public string un_Nombre { get; set; }
        public string un_Puesto { get; set; }
        public int NoCitatorio { get; set; }
        public int ContadorSMSS { get; set; }
        public int ContadorSMSR { get; set; }
        public Boolean StatusAsistencia { get; set; }
        public string StatusEntrega { get; set; }

        public string StatusSesion { get; set; }

        //----------------------------------------- Sesion

        public Guid IdSesion { get; set; }
        public Guid ModuloServicioId { get; set; }
        public DateTime? FechaHoraSys { get; set; }
        public string DescripcionSesion { get; set; }
        public string Asunto { get; set; }
        public DateTime FechaHora { get; set; }

        public string Solicitates { get; set; }
        public string Reuqeridos { get; set; }



    }

    public class GET_usuarioCitatorio
    {        
        public string uf_Nombre { get; set; }

    }

}
