using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class EstadisticaViewModelH
    {
        public Guid RHechoId { get; set; }
        public Guid RAtencionId { get; set; }
        public string nuc { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public string DistritoInicial { get; set; }
        public string StatusCarpeta { get; set; }
        public int Tipo { get; set; }
        public DateTime Fechah { get; set; }
    }
}
