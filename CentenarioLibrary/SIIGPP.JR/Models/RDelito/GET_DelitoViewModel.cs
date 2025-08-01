using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RDelito
{
    public class GET_DelitoViewModel
    {
        public Guid IdDelitoDerivado { get; set; }
        public Guid EnvioId { get; set; }
        public Guid RDHId { get; set; }
        public Guid RHechoId { get; set; }



        /********************************************************/
        public Guid DelitoId { get; set; }
        public string nombreDelito { get; set; }
        public string OfiNoOfic { get; set; }
        public Boolean altoImpacto { get; set; } 

        /********************************************************/
       
        public string TipoFuero { get; set; }
        public string TipoDeclaracion { get; set; }
        public string ResultadoDelito { get; set; }
        public string GraveNoGrave { get; set; }
        public string IntensionDelito { get; set; }
        public string ViolenciaSinViolencia { get; set; }
        public Boolean Equiparado { get; set; }
        public string Tipo { get; set; }
        public string Concurso { get; set; }
        public string ClasificaOrdenResult { get; set; }
        public Boolean ArmaFuego { get; set; }
        public Boolean ArmaBlanca { get; set; }
        public string ConAlgunaParteCuerpo { get; set; }
        public string ConotroElemento { get; set; }
        public string TipoRobado { get; set; }
        public decimal MontoRobado { get; set; }
        public string Hipotesis { get; set; }
        public string DelitoEspecifico { get; set; }

    }
    public class GET_idDelitoViewModel
    {
        public Guid IdDelitoDerivado { get; set; }
        public string nombreDelito { get; set; }
        public Guid IdDelito { get; set; }
    }

    public class GET_idDelitoDeViewModel
    {
        public Guid IdDelitoDerivado { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
    }
}
