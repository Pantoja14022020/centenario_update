using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.TipoViolencia
{
    public class TipoViolencia
    {
        [Key]
        public Guid IdTipoViolencia { get; set; }
        public string Tipo { get; set; }
      
    }
}
