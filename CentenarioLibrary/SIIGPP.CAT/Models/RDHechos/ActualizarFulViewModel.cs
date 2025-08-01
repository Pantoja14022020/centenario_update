using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class ActualizarFulViewModel
    {
        public Guid IdRDH { get; set; }
        /********************************************************/
        public string Hipotesis { get; set; }

        public Guid DelitoId { get; set; }

        /********************************************************/
        public string TipoFuero { get; set; }
        public string TipoDeclaracion { get; set; }
        public string ResultadoDelito { get; set; }
        public string GraveNoGrave { get; set; }
        public string IntensionDelito { get; set; }
        public string ViolenciaSinViolencia { get; set; }
        public string Tipo { get; set; }
        public string Concurso { get; set; }
        public string ClasificaOrdenResult { get; set; }
        public Boolean ArmaFuego { get; set; }
        public Boolean ArmaBlanca { get; set; }
        public string Observaciones { get; set; }
        public string ConAlgunaParteCuerpo { get; set; }
        public string ConotroElemento { get; set; }
        public string TipoRobado { get; set; }
        public decimal MontoRobado { get; set; }
        public string DelitoEspecifico { get; set; }
        public Guid PersonaId { get; set; }
        public string TipoViolencia { get; set; }
        public string SubtipoSexual { get; set; }
        public string TipoInfoDigital { get; set; }
        public string MedioDigital { get; set; }
        public string InstrumentosComision { get; set; }
        public string GradoDelito { get; set; }

    }

    public class InsertInstrumentosViewModel
    {
        public Guid IdRDH { get; set; }
        public string InstrumentosComision { get; set; }
    }
}
