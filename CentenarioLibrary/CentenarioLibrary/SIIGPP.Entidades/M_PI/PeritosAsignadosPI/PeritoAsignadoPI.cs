using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Cat.RActosInvestigacion;

namespace SIIGPP.Entidades.M_PI.PeritosAsignadosPI
{
    public class PeritoAsignadoPI
    {
        public Guid IdPeritoAsignadoPI { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
        public Guid ModuloServicioId { get; set; }
        public RActoInvestigacion RActoInvestigacion { get; set; }
        public Guid RActoInvestigacionId { get; set; }
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
        public string Motivo { get; set; }
        public DateTime FechaCambio { get; set; }
        public DateTime Fechasysregistro { get; set; }
        public DateTime Fechasysfinalizado { get; set; }
        public DateTime UltmimoStatus { get; set; }

    }
}
