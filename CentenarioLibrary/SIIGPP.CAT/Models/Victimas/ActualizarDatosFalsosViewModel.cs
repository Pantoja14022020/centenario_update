using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class ActualizarDatosFalsosViewModel
    {
        public Guid IdRAP { get; set; }
        public Guid IdPersona { get; set; }
        public Boolean DatosFalsos { get; set; }
    }
}
