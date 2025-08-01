using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Registro
{
    public class CrearViewModel
    {
        //REGISTRO DE ATENCION
       
        public string DistritoInicial { get; set; }
        public string DirSubProcuInicial { get; set; }
        public string AgenciaInicial { get; set; } 
        
        public Boolean StatusAtencion { get; set; }
        public Boolean StatusRegistro { get; set; }
        public Guid racid { get; set; } 
        public Guid agenciaId { get; set; }
        public string MedioLlegada { get; set; }

        //PERSONA
        public Boolean StatusAnonimo { get; set; }
        public string TipoPersona { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string ClasificacionPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Alias { get; set; }
        public Boolean StatusAlias { get; set; }
        public string FechaNacimiento { get; set; }
        public string EntidadFederativa { get; set; }
        public string DocIdentificacion { get; set; }
        public string CURP { get; set; }
        //Integraciones que no estan en todos lados por ser nuevas
        public Boolean PoblacionAfro { get; set; }
        public string RangoEdad { get; set; }
        public Boolean RangoEdadTF { get; set; }
        public Guid PoliciaDetuvo { get; set; }
        //----------------------------------------------
        public string Sexo { get; set; }
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
        public Boolean PInicio { get; set; }
        public Boolean DatosProtegidos { get; set; }
        public Boolean Relacion { get; set; }
        public int Edad { get; set; }
        public string Parentesco { get; set; }
        //DOCUMENTOS DE PERSON 
        public string TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Ruta { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }

        //DIRECCION PERSONAL
        public string Calle { get; set; }
        public string NoInt { get; set; }
        public string NoExt { get; set; }
        public string EntreCalle1 { get; set; }
        public string EntreCalle2 { get; set; }
        public string  Referencia { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public int CP { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int? tipoVialidad { get; set; }
        public int? tipoAsentamiento { get; set; }
        //TURNO
        public string Modulo { get; set; }
        public string Numerooficio { get; set; }
        public string DocPoderNotarial { get; set; }
        public string MedioDenuncia { get; set; }
        public Boolean InicioDetenido { get; set; }



    }
}
