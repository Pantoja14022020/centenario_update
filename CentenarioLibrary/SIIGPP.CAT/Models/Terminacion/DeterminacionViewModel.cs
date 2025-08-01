using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Terminacion
{
    public class DeterminacionViewModel
    {

        public Guid IdDeterminacionArchivo { get; set; }
        public Guid RHechoId { get; set; }
        public string Municipio { get; set; }
        public string Fecha { get; set; }
        public string MunicionEstado { get; set; }
        public string FechaIHecho { get; set; }
        public string MedioDenuncia { get; set; }
        public string ClasificacionPersona { get; set; }
        public string Delito { get; set; }
        public string Articulos { get; set; }
        public string Aifr { get; set; }
        public string Opcion1 { get; set; }
        public string Opcion2 { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UUAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string NumeroOficio { get; set; }
    }
}
