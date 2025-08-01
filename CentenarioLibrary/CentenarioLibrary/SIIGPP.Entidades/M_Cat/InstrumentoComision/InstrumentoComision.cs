using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.InstrumentoComision
{
   public class InstrumentoComision
    {
        [Key]
        public Guid IdInstrumentoComision { get; set; }
        public string NombreInstrumento { get; set; }

    }
}
