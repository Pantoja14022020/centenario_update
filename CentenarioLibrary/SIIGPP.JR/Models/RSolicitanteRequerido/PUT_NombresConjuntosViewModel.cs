using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSolicitanteRequerido
{
    public class PUT_NombresConjuntosViewModel
    {
        //DETALLE DE LOS SOLICITANTES ASIGNADOS
         
        public Guid idConjuntoDerivaciones { get; set; }
        public string nombreS { get; set; }
        public string nombreR { get; set; }
        public string solRequ { get; set; }

    }
}
