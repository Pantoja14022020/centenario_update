using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DenunciaEnLinea
{
    public class ActualizarFHSDELViewModel
    {
        public Guid IdRHecho { get; set; }
        public Guid ratencionId { get; set; }

        public DateTime fechaHoraSuceso { get; set; }
    }
}
