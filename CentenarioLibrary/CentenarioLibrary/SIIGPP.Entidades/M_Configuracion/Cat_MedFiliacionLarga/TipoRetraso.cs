using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga
{
    public class TipoRetraso
    {
        [Key]
        public Guid IdTipoDeRetraso { get; set; }
        public string Dato { get; set; }
    }
}
