using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Ampliacion
{
    public class AmpDec
    {
        public Guid idAmpliacion { get; set; }
        public Guid HechoId { get; set; }
        public RHecho Hecho { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string Tipo { get; set; }
        public string ClasificacionPersona { get; set; }
        public string Manifestacion {get; set;}
        public string  Hechos {get; set;}
        public string TRepresentantes {get; set;}
        public string Edad { get; set; }
        public DateTime Fechasys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public string Numerooficio { get; set; }
        public string TipoEA { get; set; }
        public string HoraS { get; set; }
        public string FechaS { get; set; }
        public string Representante { get; set; }
        public string Iniciales { get; set; }
        public string Acompañantev { get; set; }
        public string ParentezcoV { get; set; }
        public string VNombre { get; set; }
        public string VPuesto { get; set; }
        public string Tidentificacion { get; set; }
        public string NoIdentificacion { get; set; }
        public string ClasificacionP { get; set; }
        public string TipoP { get; set; }
        public string DireccionP { get; set; }
        public string CURPA { get; set; }
        public Boolean EntrevistaInicial { get; set; }
    }
}
