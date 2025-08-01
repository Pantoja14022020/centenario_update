using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Configuracion
{
   public class MedioNotificacion
    {
        [Key]
        public Guid IdMedioNotificacion { get; set; }
        public string Nombre { get; set; }
    }
}
