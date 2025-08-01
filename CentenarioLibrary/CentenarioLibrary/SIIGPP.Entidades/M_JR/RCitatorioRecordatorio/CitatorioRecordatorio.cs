
using SIIGPP.Entidades.M_JR.RSesion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_JR.RCitatorioRecordatorio
{
    public class CitatorioRecordatorio
    {
        public Guid IdCitatorioRecordatorio { get; set; }
      
        public Guid SesionId { get; set; }
        public Sesion Sesion { get; set; }
        public string NoExpediente { get; set; }
        public DateTime FechaSys { get; set; }
        public  DateTime FechaHoraCita { get; set; }
        public int Duracion { get; set; }
        public string LugarCita  { get; set; } 
        public string  dirigidoa_Nombre { get; set; }
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

        
        public string un_Modulo { get; set; }
        public string un_Nombre { get; set; }
        public string un_Puesto { get; set; }

        public int NoCitatorio { get; set; }

        public Boolean StatusAsistencia { get; set; }
        public int ContadorSMSS { get; set; }
        public int ContadorSMSR { get; set; }
        public string StatusEntrega { get; set; }
        
    }
}
