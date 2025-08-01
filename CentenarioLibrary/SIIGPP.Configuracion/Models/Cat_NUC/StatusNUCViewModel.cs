using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_NUC
{
    public class StatusNUCViewModel
    {
        public Guid IdStatusNuc { get; set; }
        public string NombreStatus { get; set; }
        public string Etapa { get; set; }
    }
}
