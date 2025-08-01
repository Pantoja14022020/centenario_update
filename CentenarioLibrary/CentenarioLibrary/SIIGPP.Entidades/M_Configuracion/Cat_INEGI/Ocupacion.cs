using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_INEGI
{
    public class Ocupacion
    {
        [Key]
        public Guid IdOcupacion { get; set; }
        public string Nombre{ get; set; }
    }
}
