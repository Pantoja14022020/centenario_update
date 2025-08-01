using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.MedidasCautelares
{
    public class NoMedidasCautelaresViewModel
    {
        public Guid IdNoMedidasCautelares { get; set; }
        public Guid MedidasCautelaresId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
