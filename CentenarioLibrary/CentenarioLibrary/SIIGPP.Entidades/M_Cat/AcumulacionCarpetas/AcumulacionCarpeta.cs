using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_Cat.AcumulacionCarpetas
{
    public class AcumulacionCarpeta
    {
        public Guid IdAcumulacionCarpeta { get; set; }
        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }
        public string NUCFusion { get; set; }
        public Guid RhechoIdFusion { get; set; }
        public DateTime FechaSys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
    }
}
