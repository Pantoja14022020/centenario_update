using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_JR.consultas
{
    public class ConsultaRepresentantesenCat
    {
        [Key]
        public Guid IdPersona { get; set; }
        public Guid? IdRepresentante { get; set; }

        public string Nombres { get; set; }
        public string ApellidoPa { get; set; }
        public string ApellidoMa { get; set; }
        public string Nacionalidad { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
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
        public string CP { get; set; }
        public int HayRepreJR { get; set; }
        public int HayRepreCAT { get; set; }
        public int? TipoVialidad { get; set; }
        public int? TipoAsentamiento { get; set; }
    }
}
