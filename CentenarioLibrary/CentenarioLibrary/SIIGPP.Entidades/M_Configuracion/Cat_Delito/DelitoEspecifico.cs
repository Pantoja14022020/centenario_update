using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Delito
{
    public class DelitoEspecifico
    {
        public Guid IdDelitoEspecifico {get; set;}
        public string Nombre { get; set; }
        public Guid DelitoId { get; set; }
        public Delito Delito { get; set; }

    }
}
