using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DireccionSuceso
{
    public class ActualizarViewModel
    {
        public Guid IdDDelito { get; set; }
        public Guid IdRHecho { get; set; }
        public string LugarEspecifico { get; set; }
        public string calle { get; set; }
        public string noint { get; set; }
        public string noext { get; set; }
        public string entrecalle1 { get; set; }
        public string entrecalle2 { get; set; }

        public string referencia { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
        public string localidad { get; set; }
        public int? cp { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int? tipoVialidad { get; set; }
        public int? tipoAsentamiento { get; set; }
    }
}
