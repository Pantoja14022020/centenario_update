using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.OAprhensionBitacoras
{
    public class OAprhensionBitacoraViewModel
    {
        public Guid IdOAprhensionBitacora { get; set; }
        public Guid OrdenAprensionId { get; set; }
        public string Texto { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime FechaSys { get; set; }
    }
}
