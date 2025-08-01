using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DenunciaEnLinea
{
    public class AgenciaDELViewModel
    {
        public Guid IdAgencia { get; set; }
        public Guid DSPId { get; set; }
        public string NombreDirSub { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Clave { get; set; }
    }

    public class AgenciaDELRViewModel
    {
        public Guid IdAgencia { get; set; }
        public string Nombre { get; set; }
    }
}
