using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Nuc
{
    public class BuscarCarpetasDistritoFiltro
    {
        public Guid idDistrito { get; set; }
        public String nucg { get; set; }
        public String nombre { get; set; }
        public Guid servidor { get; set; }
        public String apellidoPaterno { get; set; }
        public String apellidoMaterno { get; set; }
        public Guid idAgencia { get; set; }
        public Guid idModulo { get; set; }
        public DateTime? fechaDesde { get; set; }
        public DateTime? fechaHasta { get; set; }
    }
}

