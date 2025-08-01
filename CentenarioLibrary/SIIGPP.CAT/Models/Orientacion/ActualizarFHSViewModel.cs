using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ActualizarFHSViewModel
    {
        public Guid IdRHecho { get; set; }
        public Guid ratencionId { get; set; }

        public DateTime fechaHoraSuceso { get; set; }
    }
}
