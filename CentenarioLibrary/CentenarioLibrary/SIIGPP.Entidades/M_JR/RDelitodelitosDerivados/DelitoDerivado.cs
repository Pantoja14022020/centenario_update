
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.Entidades.M_JR.REnvio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_JR.RDelitodelitosDerivados
{
   public class DelitoDerivado
    {
        public Guid IdDelitoDerivado { get; set; }
        public Guid EnvioId { get; set; }
        public Envio Envio { get; set; }
        public Guid RDHId { get; set; }
        public RDH RDH { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
    }
}
