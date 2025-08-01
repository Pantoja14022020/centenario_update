using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_ControlAcceso.UAlmacenamiento
{
    public class Almacenamiento
    {
        public Guid IdAlmacenamiento { get; set; }
        public Boolean StatusActivo { get; set; }
        public Boolean StatusLLeno { get; set; }
        public decimal EspacioDsiponible { get; set; }
        public decimal EspacioTotal { get; set; }
        public decimal EspacioUtilizado { get; set; }
        public decimal Porcentaje { get; set; }


    }
}
