using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Representantes
{
    public class RepresentanteViewModel
    {
        public Guid IdRepresentante { get; set; }
        public Guid RHechoId { get; set; }
        public Guid PersonaId { get; set; }
        public string PersonaR { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPa { get; set; }
        public string ApellidoMa { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string EntidadFeNacimiento { get; set; }
        public string Curp { get; set; }
        public string MedioNotificacion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nacionalidad { get; set; }
        public string Genero { get; set; }
        public int Tipo1 { get; set; }
        public int Tipo2 { get; set; }
        public string TipoRep1 { get; set; }
        public string TipoRep2 { get; set; }
        public string CedulaProfesional { get; set; }
        public string Fecha { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string Calle { get; set; }
        public string NoInt { get; set; }
        public string NoExt { get; set; }
        public string EntreCalle1 { get; set; }
        public string EntreCalle2 { get; set; }
        public string Referencia { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public string CP { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string ArticulosPenales { get; set; }
        public string TipoDocumento { get; set; }
        public int? TipoVialidad { get; set; }
        public int? TipoAsentamiento { get; set; }
        public string Direccion { get; set; }
    }
    public class ViewModelMostrarRepresentanteJR
    {
        public Guid IdResponsable { get; set; }
        public Guid PersonaId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPa { get; set; }
        public string ApellidoMa { get; set; }
        public string Nacionalidad { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Calle { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public string EntreCalle1 { get; set; }
        public string EntreCalle2 { get; set; }
        public string Referencia { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public int? TipoVialidad { get; set; }
        public int? TipoAsentamiento { get; set; }
    }
}
