using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_JR.RCitatorioRecordatorio;
using SIIGPP.Entidades.M_JR.REnvio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_JR.RSesion
{
    public class Sesion
    {
        public Guid IdSesion { get; set; }
        public Guid ModuloServicioId { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
 
        public Guid EnvioId { get; set; }
        public Envio Envios { get; set; }
        public int NoSesion { get; set; }
        public DateTime? FechaHoraSys { get; set; }
        public string StatusSesion { get; set; }
        public string DescripcionSesion { get; set; }


        public string Asunto { get; set; } 
        public DateTime FechaHora { get; set; }

        public string Solicitates { get; set; }
        public string Reuqeridos { get; set; } 

        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
        public string Capturista { get; set; }

 
    }
}
