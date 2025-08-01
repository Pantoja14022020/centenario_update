using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Indicios
{
    public class DetalleViewModel
    {

        public Guid IdDetalles { get; set; }

        public Guid IndiciosId { get; set; }

        public DateTime FechaHora { get; set; }

        public string Fechasys { get; set; }

        public string OrigenLugar { get; set; }

        public string DestinoLugar { get; set; }

        public string QuienEntrega { get; set; }

        public string QuienRecibe { get; set; }
        

    }
}
