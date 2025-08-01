using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RArma
{
    public class ActualizarViewModel
    {
        public Guid IdRarma { get; set; }
        public string TipoAr { get; set; }
        public string NombreAr { get; set; }
        public string DescripcionAr { get; set; }
        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Calibre { get; set; }

    }
}
