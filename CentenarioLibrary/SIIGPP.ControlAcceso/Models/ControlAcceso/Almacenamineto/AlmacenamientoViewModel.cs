using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Almacenamineto
{
    public class AlmacenamientoViewModel
    {
        public Guid IdAlmacenamiento { get; set; }
        public string Nombre { get; set; }
        public string RutaFisica { get; set; }
        public Boolean StatusActivo { get; set; }
        public Boolean StatusLLeno { get; set; }
        public decimal EspacioDsiponible { get; set; }
        public decimal EspacioTotal { get; set; }
        public decimal EspacioUtilizado { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
