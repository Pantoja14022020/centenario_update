using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.SColaboracionesMP
{
    public class SColaboracionMPViewModel
    {
        public Guid IdSColaboracionMP { get; set; }
        public Guid RHechoId { get; set; }
        public Guid AgenciaId { get; set; }
        public string Texto { get; set; }
        public string TipoColaboracion { get; set; }
        public string NUC { get; set; }
        public string UsuarioSolicita { get; set; }
        public string AgenciaOrigen { get; set; }
        public string AgenciaDestino { get; set; }
        public string Status { get; set; }
        public string Respuesta { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public DateTime FechaRechazo { get; set; }

        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
