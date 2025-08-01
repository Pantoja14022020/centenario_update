using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogOficio
    {
        public Guid IdAdminOficios{ get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdOficios { get; set; }
        public Guid RHechoId { get; set; }
        public string Texto { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string NumeroOficio { get; set; }
        public string TipoOficio {get; set;}

    }
}
