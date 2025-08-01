using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.PeritoAsignadoPI
{
    public class PeritoAsignadoPIFullViewModel
    {
        public Guid IdPeritoAsignadoPI { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid RActosInvestigacionId { get; set; }
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
        public DateTime UltmimoStatus { get; set; }
        public string StatusAC { get; set; }
        public string Modulo { get; set; }


        public Guid DIdRActosInvestigacion { get; set; }
        public Guid DRHechoId { get; set; }
        public string DFechaSolicitud { get; set; }
        public string DStatus { get; set; }
        public string DServicios { get; set; }
        public string DEspecificaciones { get; set; }
        public Boolean DCdetenido { get; set; }
        public string DRespuestas { get; set; }
        public string DNUC { get; set; }
        public string DTextofinal { get; set; }
        public DateTime DFechaSys { get; set; }
        public string DUDirSubPro { get; set; }
        public string DUUsuario { get; set; }
        public string DUPuesto { get; set; }
        public string DUModulo { get; set; }
        public string DUAgencia { get; set; }
        public string NumerOficio { get; set; }
    }
}
