using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSesion
{
    public class POST_CrearSCViewModel
    {
        //********************************
        // SESION
        public Guid ModuloServicioId { get; set; }
        public Guid EnvioId { get; set; }
        public int NoSesion { get; set; }
        //********************************
        // CITATORIOS 
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

        public string StatusEntrega { get; set; }
        //Conjuntos
        public string conjuntoId { get; set; }


    }
}
