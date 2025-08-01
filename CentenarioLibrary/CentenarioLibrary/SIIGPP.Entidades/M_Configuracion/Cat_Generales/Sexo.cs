using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Generales
{
    public class Sexo
    {
        public Guid IdSexo { get; set; }
        public string Clave { get; set; } 
        public string Nombre { get; set; }
    }
}
