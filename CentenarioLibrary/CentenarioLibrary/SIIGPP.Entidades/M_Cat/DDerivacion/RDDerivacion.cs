using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.DDerivacion
{
   public class RDDerivacion
    {
        [Key]
        public Guid idRDDerivacion { get; set; }
        public Guid rHechoId { get; set; }
        public RHecho RHecho { get; set; }
        public Guid idDDerivacion { get; set; }
        public DependeciasDerivacion DependeciasDerivacion { get; set; }
        public string Espesificaciones { get; set; }
        public string FechaDerivacion { get; set; }
        public DateTime? FechaSys { get; set; }

        public string uDistrito { get; set; }
        public string uDirSubPro { get; set; }
        public string uAgencia { get; set; }
        public string uNombre { get; set; }
        public string UPuesto { get; set; }

    }
}
