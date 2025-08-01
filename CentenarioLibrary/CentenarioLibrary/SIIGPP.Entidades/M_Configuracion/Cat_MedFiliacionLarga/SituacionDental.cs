using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga
{
    public class SituacionDental
    {
        [Key]
        public Guid IdSituacionDental { get; set; }
        public string Dato { get; set; }
    }
}
