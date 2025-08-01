using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_INEGI
{
    public class Lengua
    {
        [Key]
        public Guid IdLengua { get; set; }
        public int Clave { get; set; }
        public string Nombre { get; set; }

    }
}
