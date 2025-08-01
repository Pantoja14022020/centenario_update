using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class DatosGeneralesEstadistica
    {

        public Guid Distrito { get; set; }

        public Boolean Distritoact { get; set; }

        public Guid Dsp { get; set; }

        public Boolean Dspact { get; set; }

        public Guid Agencia { get; set; }

        public Boolean Agenciaact { get; set; }

        public DateTime Fechadesde { get; set; }

        public DateTime Fechahasta { get; set; }
    }
}
