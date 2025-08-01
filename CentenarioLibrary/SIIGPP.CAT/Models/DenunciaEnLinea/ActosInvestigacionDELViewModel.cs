using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DenunciaEnLinea
{

    public class ActosInvestigacionDELViewModel
    {
        public Guid IdActonvestigacion { get; set; }

        public string Nombre { get; set; }

        public string Nomenclatura { get; set; }

        public string Descripcion { get; set; }
        public Boolean RAutorizacion { get; set; }
    }
}

 