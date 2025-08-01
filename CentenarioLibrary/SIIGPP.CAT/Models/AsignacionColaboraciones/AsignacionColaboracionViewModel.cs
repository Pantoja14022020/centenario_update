using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.AsignacionColaboraciones
{
    public class AsignacionColaboracionViewModel
    {
        public Guid IdAsignacionColaboraciones { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid SColaboracionMPId { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }

        public Guid CIdSColaboracionMP { get; set; }
        public Guid CRHechoId { get; set; }
        public Guid CAgenciaId { get; set; }
        public string CTexto { get; set; }
        public string CTipoColaboracion { get; set; }
        public string CNUC { get; set; }
        public string CUsuarioSolicita { get; set; }
        public string CAgenciaOrigen { get; set; }
        public string CAgenciaDestino { get; set; }
        public string CStatus { get; set; }
        public string CRespuesta { get; set; }
        public DateTime CFechaRespuesta { get; set; }
        public DateTime CFechaRechazo { get; set; }

        public string CUDistrito { get; set; }
        public string CUSubproc { get; set; }
        public string CUAgencia { get; set; }
        public string CUsuario { get; set; }
        public string CUPuesto { get; set; }
        public string CUModulo { get; set; }
        public DateTime CFechasys { get; set; }
    }
}
