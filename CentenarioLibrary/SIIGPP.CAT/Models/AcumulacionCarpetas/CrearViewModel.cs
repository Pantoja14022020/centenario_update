using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.AcumulacionCarpetas
{
    public class CrearViewModel
    {
        public Guid RHechoId { get; set; }
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
