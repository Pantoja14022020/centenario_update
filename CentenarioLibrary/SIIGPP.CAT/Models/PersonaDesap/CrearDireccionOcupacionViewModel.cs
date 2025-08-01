using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PersonaDesap
{
    public class CrearDireccionOcupacionViewModel
    {
        public Guid PersonaId { get; set; }
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
