using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.InformacionJuridico.PresentacionesYC
{
    public class PresentacionYCViewModel
    {
        public Guid IdPresentacionesYC { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Recepcion { get; set; }
        public string Oficio { get; set; }
        public string CausaPenal { get; set; }
        public string Dependencia { get; set; }
        public string PerAPresentar { get; set; }
        public string FdePresentacion { get; set; }
        public string Hora { get; set; }
        public string Asignado { get; set; }
        public string CumplidaOInf { get; set; }
        public string Status { get; set; }
        public string Estado { get; set; }
        public string Respuesta { get; set; }
        public DateTime FAsignacion { get; set; }
        public DateTime FFinalizacion { get; set; }
        public DateTime FUltmimoStatus { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public DateTime FechasComparescencia { get; set; }
        public DateTime FechaCumplimiento { get; set; }
        public string FechaRecepcion { get; set; }
    }
}
