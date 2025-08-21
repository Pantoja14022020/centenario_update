using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.FEDC.Models.NoAcionPenal
{
    public class CrearViewModel
    {
     
        public Guid RHechoId { get; set; }
        public string NumeroOficio { get; set; }
        public string Delitos { get; set; }
        public string Victimas { get; set; }
        public string Imputados { get; set; }
        public string Cosumacion { get; set; }
        public string AusenciaVOluntad { get; set; }
        public string CausasAtipicidad { get; set; }
        public string FalteElementos { get; set; }
        public string EfectosCodigo { get; set; }
        public string Sobreseimiento { get; set; }
        public string HechocncDelito { get; set; }
        public string Antecedente { get; set; }
        public string Articulo25 { get; set; }
        public DateTime FechaSys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
    }
}
