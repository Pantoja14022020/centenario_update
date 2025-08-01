using System;
using System.Collections.Generic;
using System.Text;


namespace SIIGPP.Entidades.M_Administracion
{
    public class LogArchivos
    {
        public Guid IdAdminArchivos { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdArchivos { get; set; }
        public Guid RHechoId { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public string Ruta { get; set; }
        public string Fecha { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }

    }
}
