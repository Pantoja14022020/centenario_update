using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Configuracion
{
    public class DocIdentificacion

    {
        [Key]
        public Guid IdDocIdentificacion { get; set; }
        public string Nombre { get; set; }
    }
}
