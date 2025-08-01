using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Persona
{
    public class BuscarPersonaViewModel 
    {
        public Guid PersonaId { get; set; } 
        public string Genero { get; set; }
        public Boolean Registro { get; set; }
        public Boolean VerR { get; set; }
        public Boolean VerI { get; set; }
        public string EstadoCivil { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Correo { get; set; }
        public string Medionotificacion { get; set; }
        public string Nacionalidad { get; set; }
        public string Ocupacion { get; set; }
        public string NivelEstudio { get; set; }
        public string Lengua { get; set; }
        public string Religion { get; set; }
        public Boolean Discapacidad { get; set; }
        public string TipoDiscapacidad { get; set; }
        public string Numerornd { get; set; }
        public string InstitutoPolicial { get; set; }
    }
    public class BuscarPersona1ViewModel 
    {
        public Guid PersonaId { get; set; } 
        public string Genero { get; set; }
        public Boolean Registro { get; set; }
        public Boolean VerR { get; set; }
        public Boolean VerI { get; set; }
        public string EstadoCivil { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Correo { get; set; }
        public string Medionotificacion { get; set; }
        public string Nacionalidad { get; set; }
        public string Ocupacion { get; set; }
        public string NivelEstudio { get; set; }
        public string Lengua { get; set; }
        public string Religion { get; set; }
        public Boolean Discapacidad { get; set; }
        public string TipoDiscapacidad { get; set; }
        public string Numerornd { get; set; }
        public string InstitutoPolicial { get; set; }
        public string Clasificacion { get; set; }
    }
}
