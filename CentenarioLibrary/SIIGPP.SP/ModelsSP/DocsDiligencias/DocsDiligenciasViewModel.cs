using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.SP.ModelsSP.DocsDiligencias
{
    public class DocsDiligenciasViewModel
    {
        public Guid IdDocsDiligencia { get; set; }
        public Guid RDiligenciasId { get; set; }
        public string TipoDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public string FechaRegistro { get; set; }
        public string Puesto { get; set; }
        public string Ruta { get; set; }
        public string Usuario { get; set; }
        public string uDistrito { get; set; }
        public string uSubproc { get; set; }
        public string uAgencia { get; set; }
        public string uUsuario { get; set; }
        public string uPuesto { get; set; }
        public string uModulo { get; set; }
        public DateTime fechasysregistro { get; set; }
    }
}
