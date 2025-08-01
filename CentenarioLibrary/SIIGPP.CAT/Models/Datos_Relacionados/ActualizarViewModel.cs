using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Datos_Relacionados
{
    public class ActualizarViewModel
    {
        public Guid IdDatosRelacionados { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}
