using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.Informes
{
    public class InformeViewModel
    {
        public Guid IdInforme { get; set; }
        public Guid PeritoAsignadoPIId { get; set; }
        public int TipoInforme { get; set; }
        public string TextoInforme { get; set; }
        public string Fecha { get; set; }
        public DateTime FechaSys { get; set; }
        public string uDistrito { get; set; }
        public string uSubproc { get; set; }
        public string uAgencia { get; set; }
        public string uUsuario { get; set; }
        public string uPuesto { get; set; }
        public string uModulo { get; set; }
        public string NoOficio { get; set; }

        public string PersonaSolicita {get; set;}
        public string Nuc { get; set; }
    }
}
