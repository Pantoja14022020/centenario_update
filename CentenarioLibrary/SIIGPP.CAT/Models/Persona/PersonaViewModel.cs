using SIIGPP.Entidades.M_Cat.Direcciones;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Persona
{
    public class PersonaViewModel
    {
        public Guid IdPersona { get; set; }
        public Boolean StatusAnonimo { get; set; }
        public string TipoPersona { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCompleto { get; set; }
        public string Alias { get; set; }
        public Boolean StatusAlias { get; set; }
        public string FechaNacimiento { get; set; }
        public string EntidadFederativa { get; set; }
        public string DocIdentificacion { get; set; }
        public string CURP { get; set; }
        //Integraciones nuevas
        public Boolean? PoblacionAfro { get; set; }
        public string RangoEdad { get; set; }
        public Boolean? RangoEdadTF { get; set; }
        public Guid? PoliciaDetuvo { get; set; }
        //----------------------------------------------------------
        public string Sexo { get; set; }
        public string Genero { get; set; }
        public Boolean? Registro { get; set; }
        public Boolean? VerR { get; set; }
        public Boolean? VerI { get; set; }
        public string EstadoCivil { get; set; }
        public string Telefonos { get; set; }
        public string Correo { get; set; }
        public string Medionotificacion { get; set; }
        public string Nacionalidad { get; set; }
        public string Ocupacion { get; set; }
        public string NivelEstudio { get; set; }
        public string Lengua { get; set; }
        public string Religion { get; set; }
        public Boolean Discapacidad { get; set; }
        public string TipoDiscapacidad { get; set; }
        public string Parentesco { get; set; }
        public Boolean DatosProtegidos { get; set; }
        public List<RAP> RAPs { get; set; }
        public string Numerornd { get; set; }
        public string InstitutoPolicial { get; set; }
        public string InformePolicial { get; set; }
        public Boolean Relacion { get; set; }
        public int Edad { get; set; }
        public Boolean DatosFalsos { get; set; } = false;
        public string DocPoderNotarial { get; set; }
        public Boolean InicioDetenido { get; set; }
        public string CumpleRequisitoLey { get; set; }
        public string DecretoLibertad { get; set; }
        public string DispusoLibertad { get; set; }
        public string Direccion { get; set; }
        public string Clasificacion { get; set; }
        public string Tipo { get; set; }
    }
}
