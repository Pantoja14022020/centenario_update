using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_INEGI
{
    public class Asentamiento
    {
        [Key]
        public Guid IdAsentamiento { get; set; }
        public string Nombre { get; set; }
        public int Clave { get; set; }
    }
}
