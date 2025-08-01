using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class DENombreViewModel
    {

        public string DireccionE { get; set; }
        public string NombreCompleto { get; set; }
        public string Apaterno { get; set; }
        public string Amaterno { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public Guid PersonaId { get; set; }
        public string Alias { get; set; }
        public string Correo { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Genero { get; set; }
        public int? TipoVialidad { get; set; }
        public int? TipoAsentamiento { get; set; }
    }
}
