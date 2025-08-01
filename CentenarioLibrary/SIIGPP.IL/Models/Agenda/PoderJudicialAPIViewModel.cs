using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.IL.Models.Agenda
{
    public class PoderJudicialAPIViewModel
    {
        public string Causa { get; set; }
        public string Juzgado { get; set; }
        public string TipoAudi { get; set; }
        public DateTime FechaAudi { get; set; }
        public string SalaDescrip { get; set; }
        public string Tipo { get; set; }
        public string NUC { get; set; }
        public Guid ClavePGJ { get; set; }

    }
}
