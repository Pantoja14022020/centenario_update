using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Terminacion
{
    public class RIncompetenciaViewModel
    {
        public Guid IdInconpentencia { get; set; }
        public Guid RHechoId { get; set; }
        public string TextoFinal { get; set; }
        public string Fecha { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UUAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string RBreve { get; set; }
        public string Dependencia { get; set; }
        public string Articulos { get; set; }
        public string NumeroOficio { get; set; }
    }
}
