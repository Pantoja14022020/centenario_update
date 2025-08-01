using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DireccionSuceso
{
    public class CrearViewModel
    {
        //DIRECCION DELITO

        public Guid IdRHecho { get; set; }
        public string LugarEspecifico { get; set; }
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
        public int? CP { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int? tipoVialidad { get; set; }
        public int? tipoAsentamiento { get; set; }
    }
}
