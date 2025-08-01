using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_PI.Detenciones
{
    public class Detencion
    {
        public Guid IdDetencion { get; set; }
        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string Nuc { get; set; }
        public string MpAsignado { get; set; }
        public string Mesa { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaSalida { get; set; }
        public string FechaTraslado { get; set; }
        public string NumUnicoRegNacional { get; set; }
        public string TipoDetencion { get; set; }
        public string Pertenecnias { get; set; }
        public DateTime FechaHReingreso { get; set; }
    
        public string Status { get; set; }
        public string AutoridadQO { get; set; }
        public string AutoridadED { get; set; }
        public string Delito { get; set; }
        public string Tdelito { get; set; }
        public string Modalidad { get; set; }
        public string MOperandi { get; set; }
        
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public DateTime FechaUltimoStatus { get; set; }
        
    }
}
