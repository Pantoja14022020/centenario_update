using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.MedAfiliacion
{
    public class ArchivosMediaAfiliacion
    {
        public Guid IdArchivosMediaAfiliacion { get; set; }
        public Guid MediaAfiliacionid { get; set; }
        public MediaAfiliacion MediaAfiliacion { get; set; }
        public string TipoDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public string Puesto { get; set; }
        public string Ruta { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
