using SIIGPP.Entidades.M_PI.Detenciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_PI.SubirArchivos
{
    public class SubirArchivo
    {
        public Guid IdSubirArchivo { get; set; }
        public Guid DetencionId { get; set; }
        public Detencion Detencion { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public string Ruta { get; set; }
        public DateTime Fecha { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
