using SIIGPP.Entidades.M_PI.Direcciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RRespresentatne
{
    public class GET_RepresentenateVM
    {
        public Guid IdResponsable { get; set; }
        public Guid PersonaId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPa { get; set; }
        public string ApellidoMa { get; set; }
        public string Nacionalidad { get; set; }
        public int Edad { get; set; }
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
    public class GET_RepresentenateVM1
    {
        public Guid IdResponsable { get; set; }
        public Guid PersonaId { get; set; }
        public string NombreCompleto { get; set; }
        public string Nacionalidad { get; set; }
        public int Edad { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Direccion {  get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public Boolean Responsable { get; set; }
    }

    public class GET_RepresentanteAll
    {
        public Guid IdResponsable { get; set; }
        public Guid PersonaId { get; set; }        
        public string Nombre { get; set; }
        public string ApellidoPa { get; set; }
        public string ApellidoMa { get; set; }
        public string Nacionalidad { get; set; }
        public int Edad { get; set; }
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
    }

}
