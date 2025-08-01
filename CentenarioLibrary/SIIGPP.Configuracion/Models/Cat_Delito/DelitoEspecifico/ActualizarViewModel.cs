using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Delito.DelitoEspecifico
{
    public class ActualizarViewModel
    {
        public Guid IdDelitoEspecifico { get; set; }
        public string Nombre { get; set; }
        public Guid DelitoId { get; set; }
    }
}
