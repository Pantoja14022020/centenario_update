using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Persona
{
    public class ActualizarViewModel
    {
        public Guid personaId { get; set; }
        public Guid rapId { get; set;}
        public string clasificacionPersona { get; set; }
        public Boolean statusAnonimo { get; set; }
        public string tipoPersona { get; set; }
        public string rfc { get; set; }
        public string razonSocial { get; set; } 
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string alias { get; set; }
        public Boolean statusAlias { get; set; }
        public string fechaNacimiento { get; set; }
        public string entidadFederativa { get; set; }
        public string curp { get; set; }
        //Integraciones que no estan en todos lados por ser nuevas
        public Boolean PoblacionAfro { get; set; }
        public string RangoEdad { get; set; }
        public Boolean RangoEdadTF { get; set; }
        //--------------------------------------------------------
        public string sexo { get; set; }
        public string docIdentificacion { get; set; }
        public string genero { get; set; }
        public Boolean registro { get; set; }
        public Boolean verR { get; set; }
        public Boolean verI { get; set; }
        public string estadoCivil { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string correo { get; set; }
        public string medioNotificacion { get; set; }
        public string nacionalidad { get; set; }
        public string ocupacion { get; set; }
        public Boolean relacion { get; set; }
        public int edad { get; set; }
        public Boolean datosProtegidos { get; set; }
        public string parentesco { get; set; }
        public string nivelEstudio { get; set; }
        public string lengua { get; set; }
        public string religion { get; set; }
        public Boolean discapacidad { get; set; }
        public string tipoDiscapacidad { get; set; }//
       
        //DIRECCION PERSONAL
        public string calle { get; set; }
        public string noInt { get; set; }
        public string noExt { get; set; }
        public string entreCalle1 { get; set; }
        public string entreCalle2 { get; set; }
        public string referencia { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
        public string localidad { get; set; }
        public int cp { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int? tipoVialidad { get; set; }
        public int? tipoAsentamiento { get; set; }
    }
}
