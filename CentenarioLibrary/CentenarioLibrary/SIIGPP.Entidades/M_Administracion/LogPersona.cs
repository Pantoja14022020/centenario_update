 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIIGPP.Entidades.M_Cat.Registro;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogPersona
    {
 
        public Guid IdAdminPersona { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdPersona { get; set; }
        public  Boolean StatusAnonimo { get; set; }
        public string TipoPersona { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; } 
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Alias { get; set; }
        public Boolean StatusAlias { get; set; }
        public string FechaNacimiento { get; set; }
        public string EntidadFederativa { get; set; }
        public string DocIdentificacion { get; set; }
        public string CURP { get; set; }
        public string Sexo { get; set; }
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
        public string Parentesco { get; set; }
        public Boolean DatosProtegidos { get; set; }
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
        public string   DispusoLibertad { get; set; }
        
    }
}
