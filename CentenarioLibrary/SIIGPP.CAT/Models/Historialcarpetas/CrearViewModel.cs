using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Historialcarpetas
{
    public class CrearViewModel
    {
     
        public Guid RHechoId { get; set; }
        public string Detalle { get; set; }
        public string DetalleEtapa { get; set; }
        public string Agencia { get; set; }
        public string Modulo { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
