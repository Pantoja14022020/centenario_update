using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Ampliacion
{
    public class AmpDecViewModel
    {
        public Guid idAmpliacion { get; set; }
        public Guid HechoId { get; set; }
        public Guid PersonaId { get; set; }
        public string NombrePersona { get; set; }
        public string Tipo { get; set; }
        public string ClasificacionPersona { get; set; }
        public string Manifestacion { get; set; }
        public string Hechos { get; set; }
        public string TRepresentantes { get; set; }
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


        public string Alias { get; set; }
        public string Curp { get; set; }
        public string DocIdentificacion { get; set; }
        public string Nacionalidad { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string Medionotificacion { get; set; }
        public string Ocupacion { get; set; }
        public string Nivelestudio { get; set; }
        public string Religion { get; set; }
        public string Lengua { get; set; }
        public string Estadocivil { get; set; }
        public string Tipodiscapacidad { get; set; }

        public string ClasificacionP { get; set; }
        public string TipoP { get; set; }
        public string DireccionP { get; set; }
        public string CURPA { get; set; }
        public Boolean EntrevistaInicial { get; set; }
        public Boolean DatosProtegidos { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string DocPoderNotarial { get; set; }
    }
}
