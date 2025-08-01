using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Registro;

namespace SIIGPP.Entidades.M_Cat.PersonaDesap
{
    public class DireccionOcupacion
    {
        public Guid IdDOcupacion { get; set; }
        public Guid PersonaId { get; set; }
        public Persona persona { get; set; }
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
        public int CP { get; set; }
        public int? TipoVialidad { get; set; }
        public int? TipoAsentamiento { get; set; }
    }
}
