using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PRegistro
{
    public class RacFromPRegistroViewModel
    {
        public Guid IdPreRegistro { get; set; }
        public Guid IdModulo { get; set; }
        public Guid IdDistrito { get; set; }
        public Guid IdAgencia { get; set; }
        public string UNombre { get; set; }
        public string UPuesto { get; set; }
        public string dirSubProcuInicial { get; set; }
        public string agencia { get; set; }
        public string distritoinicial { get; set; }
        public string modulo { get; set; }
        public Guid idPersona { get; set; }


    }
}
