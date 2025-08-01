using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_INEGI
{
    public class Religion
    {
        [Key]
        public Guid IdReligion { get; set; }
        public string Nombre { get; set; }
    }
}
