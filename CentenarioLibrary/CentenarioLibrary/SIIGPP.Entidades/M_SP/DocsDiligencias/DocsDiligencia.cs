using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Diligencias;

namespace SIIGPP.Entidades.M_SP.DocsDiligencias
{
    public class DocsDiligencia
    {
        public Guid IdDocsDiligencia { get; set; }
        public Guid RDiligenciasId { get; set; }
        public RDiligencias RDiligencias { get; set; }
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
