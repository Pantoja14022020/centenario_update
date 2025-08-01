using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Delito.Delito
{
    public class ActualizarViewModel
    {
        public Guid IdDelito { get; set; }
        public string Nombre { get; set; }
        public Boolean SuceptibleMASC { get; set; }
        public Boolean TipoMontoRobo { get; set; }
        public Boolean AltoImpacto { get; set; }
        public string OfiNoOfi { get; set; }

    }
}
