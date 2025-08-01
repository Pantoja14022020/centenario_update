using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_JR.consultas
{
    public class ConsultaRequeridoCat
    {
        [Key]
        public Guid IdPersona { get; set; }
        public string Clasificacion { get; set; }
        public string Tipo { get; set; }
        public string PersonaRepresentar { get; set; }
        public string RepresentanteJr { get; set; }


    }
}
