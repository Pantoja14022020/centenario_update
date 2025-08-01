using SIIGPP.Entidades.M_Cat.DDerivacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Estructura
{
   public class DependeciasDerivacion
    {

        public Guid IdDDerivacion { get; set; }
        public Guid DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public string Nombre { get; set; }
        public  string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; } 
        
        

    }
}
