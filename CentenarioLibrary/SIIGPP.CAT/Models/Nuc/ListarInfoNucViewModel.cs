using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Nuc
{
    public class ListarInfoNucViewModel
    {
        public Guid idNuc { get; set; }
        public Guid DistritoId { get; set; }
        public int DConsecutivo { get; set; }
        public string CveDistrito { get; set; }
        public string nucg { get; set; }
        public string DistritoActual { get; set; }
        public string SubdireccionActual { get; set; }
        public string AgenciaActual { get; set; }
        public string ModuloActual { get; set; }
        public string racg { get; set; }
        public Guid DistritoIdActual { get; set; }

    }
}
