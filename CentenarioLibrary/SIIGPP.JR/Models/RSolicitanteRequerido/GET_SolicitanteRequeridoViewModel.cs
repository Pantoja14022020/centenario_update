using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSolicitanteRequerido
{
    public class GET_SolicitanteRequeridoViewModel
    {
        public Guid IdRSolicitanteRequerido { get; set; }
        public Guid EnvioId { get; set; } 
        public Guid PersonaId { get; set; } 
        public string Tipo { get; set; }
        public string Clasificacion { get; set; }
        public Guid rapId { get; set; }
        //*************************************************

        public string TipoPersona { get; set; }
        public Boolean StatusAnonimo { get; set; } 
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        //*********************************************************************
        
        public Boolean Seleccion { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Alias { get; set; }
        public string FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string EntidadFederativa { get; set; }
        public string DocIdentificacion { get; set; }

        public string CURP { get; set; }
        public string Sexo { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string direccion { get; set; }
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
        public Boolean DatosProtegidos { get; set; }
        public Boolean? VerR { get; set; }
        public Boolean? VerI { get; set; }
        public Boolean? Registro { get; set; }
        public string NombreS { get; set; }
        public string NombreR { get; set;}
        public string NombreD { get; set;}
         
    }

    public class GET_idSolicitanteRequeridoViewModel
    {
        public Guid IdRSolicitanteRequerido { get; set; }
        public Guid EnvioId { get; set; }
        public Guid PersonaId { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
    public class GET_ListarPersonasRepre
    {
        public Guid IdPersona { get; set; }
        public string NombreCompleto { get; set; }
        public string Tipo {  get; set; }
        public string Clasificacion { get; set; }
        public string RepresentanteJr { get; set; }
    }

    public class NombresConjuntosViewModel
    {
        public string SolicitadosC { get; set; }
        public string RequeridosC { get; set; }
        public string NombreS { get; set; }
        public string NombreR { get; set; }
    }
}
