using System;

namespace SIIGPP.CAT.Models.Representantes
{
    public class GetRepresentantesActivosViewModel
    {
        public Guid IdRepresentante { get; set; }
        public Guid PersonaId { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPa { get; set; }
        public string ApellidoMa { get; set; }
        public string Sexo { get; set; }
        public string Curp { get; set; }
        public string MedioNotificacion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int Tipo1 { get; set; }
        public int Tipo2 { get; set; }
        public string CedulaProfesional { get; set; }
        public string Calle { get; set; }
        public string NoInt { get; set; }
        public string NoExt { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public string CP { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Tipo1Nombre { get; set; }
        public string Tipo2Nombre { get; set; }
        public int? TipoVialidad { get; set; }
        public int? TipoAsentamiento { get; set; }
    }
}
