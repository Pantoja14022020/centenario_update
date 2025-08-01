using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SIIGPP.Entidades.M_Cat.Diligencias;
using SIIGPP.Entidades.M_Cat.Indicios;

namespace SIIGPP.Entidades.M_SP.DiligenciaIndicios
{
   public class DiligenciaIndicio
    {
        [Key]
        public Guid IdDiligenciaIndicio { get; set; }
        public Guid RDiligenciasId { get; set; }
        public RDiligencias RDiligencias { get; set; }
        public Guid IndiciosId { get; set; }
        public Indicios Indicios { get; set; }

    }
}
