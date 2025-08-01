using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.SistemaAudiencias
{
    public class PersonasNucviewModel
    {
        public Guid idRAP { get; set; }
        public Guid rAtencionId { get; set; }
        public Guid idPersona { get; set; }
        public Boolean statusAnonimo { get; set; }
        public string tipoPersona { get; set; }
        public string rfc { get; set; }
        public string razonSocial { get; set; }
        public string clasificacionPersona { get; set; }
        public bool pInicio { get; set; }
        public string nombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Alias { get; set; }
        public string FechaNacimiento { get; set; }
        public string EntidadFederativa { get; set; }
        public string CURP { get; set; }
        public Boolean? PoblacionAfro { get; set; }
        public string RangoEdad { get; set; }
        public Boolean? RangoEdadTF { get; set; }
        public Guid? PoliciaDetuvo { get; set; }
        public string Sexo { get; set; }
        public string DocIdentificacion { get; set; }
        public string Genero { get; set; }
        public Boolean? Registro { get; set; }
        public Boolean? VerR { get; set; }
        public Boolean? VerI { get; set; }
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
        public Boolean Relacion { get; set; }
        public int Edad { get; set; }
        public string Parentesco { get; set; }
        public string InstitutoPolicial { get; set; }
        public Boolean DatosFalsos { get; set; }
        public Boolean DatosProtegidos { get; set; }
        public string DocPoderNotarial { get; set; }
        public Boolean InicioDetenido { get; set; }
        public string CumpleRequisitoLey { get; set; }
        public string DecretoLibertad { get; set; }
        public string DispusoLibertad { get; set; }
        public string direccion { get; set; }

    }
}
