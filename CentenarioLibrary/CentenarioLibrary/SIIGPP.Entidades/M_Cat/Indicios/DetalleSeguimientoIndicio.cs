using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Indicios
{
    public class DetalleSeguimientoIndicio
    {

        public Guid IdDetalles { get; set; }

        public Guid IndiciosId { get; set; }
        public Indicios Indicios { get; set; }

        public DateTime FechaHora { get; set; }

        public string Fechasys { get; set; }

        public string OrigenLugar { get; set; }

        public string DestinoLugar { get; set; }

        public string QuienEntrega { get; set; }

        public string QuienRecibe { get; set; }
     
     

    }
}
