using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DenunciaEnLinea
{
    public class AsignarServicioDELViewModel
    {
        public Guid IdASP { get; set; } 
        public Guid AgenciaId { get; set; }
        public string NombreAgencia { get; set; }
        public Guid DSPId { get; set; }
        public string NombreDirSub { get; set; }
        public Guid ServicioPericialId { get; set; }
        public string NombreServicio { get; set; }
    }
}
