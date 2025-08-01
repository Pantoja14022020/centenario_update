using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class VictimasViewModelM
    {
        public Guid idRAP { get; set; }
        public Guid RAtencionId { get; set; }
        public Guid PersonaId { get; set; }
        public string ClasificacionPersona { get; set; }
        //PERSONA
        public Boolean StatusAnonimo { get; set; }
        public string TipoPersona { get; set; }
        public string NombreC { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Alias { get; set; }
        public string FechaNacimiento { get; set; }
        public string EntidadFederativa { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string DocIdentificacion { get; set; }
        public string CURP { get; set; }
        public string Sexo { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Correo { get; set; }
        public string Medionotificacion { get; set; }
        public string Nacionalidad { get; set; }
        public string Ocupacion { get; set; }
        public string Lengua { get; set; }
        public string Religion { get; set; }
        public Boolean Discapacidad { get; set; }
        public string TipoDiscapacidad { get; set; }
        public string Parentesco { get; set; }
        public string NivelEstudio { get; set; }
        public Boolean Relacion { get; set; }

    }
}

 