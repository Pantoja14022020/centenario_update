using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DenunciaEnLinea
{
    public class ListarMedidasPViewModel
    {
        public Guid IdMedidasProteccionC { get; set; }
        public string Clave { get; set; }
        public string Clasificacion { get; set; }
        public string Descripcion { get; set; }
    }
}
