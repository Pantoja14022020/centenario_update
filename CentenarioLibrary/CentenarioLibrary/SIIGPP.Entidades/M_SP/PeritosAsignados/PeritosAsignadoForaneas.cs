using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Cat.Diligencias;

namespace SIIGPP.Entidades.M_SP.PeritosAsignados
{
    public class PeritosAsignadoForaneas
    {
        public Guid IdPeritosAsignadoForaneas { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid RDiligenciasForaneasId { get; set; }
        public RDiligenciasForaneas RDiligenciasForaneas { get; set; }
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
        public string NumeroControl { get; set; }
    }
}
