using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSeguimientoCumplimiento
{
    public class GET_RegistroViewModel
    {
       
     
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


    }
}
