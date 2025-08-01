using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSolicitanteRequerido
{
    public class POST_CrearRViewModel
    {
        //DETALLE DE LOS SOLICITANTES ASIGNADOS
         
        public Guid code { get; set; }
        public string Tipo { get; set; }
        public string Clasificacion { get; set; }
  
    }
}
