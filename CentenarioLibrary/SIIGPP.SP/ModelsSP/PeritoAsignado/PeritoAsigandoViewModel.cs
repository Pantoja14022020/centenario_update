using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.SP.ModelsSP.PeritoAsignado
{
    public class PeritoAsigandoViewModel
    {
        public Guid IdPeritoAsignado { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Modulo { get; set; }
        public string Servicio { get; set; }
        public Guid RDiligenciasId { get; set; }
        public string Respuesta { get; set; }
        public int NumeroInterno { get; set; }
        public string Conclusion { get; set; }
        public string FechaRecibido { get; set; }
        public string FechaAceptado { get; set; }
        public string FechaFinalizado { get; set; }
        public string FechaEntregado { get; set; }
        public string uDistrito { get; set; }
        public string uSubproc { get; set; }
        public string uAgencia { get; set; }
        public string uUsuario { get; set; }
        public string uPuesto { get; set; }
        public string uModulo { get; set; }
        public DateTime Fechasysregistro { get; set; }
        public DateTime Fechasysfinalizado { get; set; }
        public string Status { get; set; }
        public string NUC { get; set; }
        public DateTime UltmimoStatus { get; set; }


        public Guid DrHechoId { get; set; }
        public string DFechaSolicitud { get; set; }
        public string DDirigidoa { get; set; }
        public string DEmitidoPor { get; set; }
        public string DDirSubPro { get; set; }
        public string DuDirSubPro { get; set; }
        public string DUPuesto { get; set; }
        public string DStatusRespuesta { get; set; }
        public string DServicio { get; set; }
        public string DEspecificaciones { get; set; }
        public string DPrioridad { get; set; }
        public Guid DASPId { get; set; }
        public string DModulo { get; set; }
        public string DAgencia { get; set; }
        public string DRespuestas { get; set; }
        public Boolean DConIndicio { get; set; }
        public string DNUC { get; set; }
        public string DTextofinal { get; set; }
        public Guid idagencia { get; set; }
        public string numerooficio { get; set; }
        public string NumeroControl { get; set; }


    }
}
