using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogHistorialCarpeta
    {
        public Guid IdAdminHistorialcarpetas { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdHistorialcarpetas { get; set; }
        public Guid RHechoId { get; set; }
        public string Detalle { get; set; }
        public string DetalleEtapa { get; set; }
        public string Modulo { get; set; }
        public string Agencia { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
