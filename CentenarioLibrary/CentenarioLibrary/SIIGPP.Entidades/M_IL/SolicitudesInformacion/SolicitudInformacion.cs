using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_IL.SolicitudesInformacion
{
    public class SolicitudInformacion
    {
        public Guid IdSolicitudInfo { get; set; }
        public string CodOficio { get; set; }
        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }
        public string CausaPenal { get; set; }
        public string Nuc { get; set; }
        public string Imputado { get; set; }
        public string CURP { get; set; }
        public string Fnacimiento { get; set; }
        public string Dirigidoa { get; set; }
        public string Puesto { get; set; }
        public string Correo { get; set; }
        public int TipoDoc { get; set; }
        public string Sobr { get; set; }
        public string Unico { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
