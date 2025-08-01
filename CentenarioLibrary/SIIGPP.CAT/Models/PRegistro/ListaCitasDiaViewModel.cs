using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PRegistro
{
    public class ListaCitasDiaViewModel
    {
        public Guid IdAgencia { get; set; }
        public TimeSpan hora { get; set; }
        public DateTime fecha { get; set; }
        public int espacios { get; set; }

    }
}
