using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Datos_Relacionados
{
    public class DatosRelacionadosViewModel
    {
        public Guid IdDatosRelacionados { get; set; }
        public Guid RHechoId { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
