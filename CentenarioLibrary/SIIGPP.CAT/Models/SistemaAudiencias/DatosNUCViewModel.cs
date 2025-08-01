using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.SistemaAudiencias
{
    public class DatosNUCViewModel
    {
        public Guid idHecho { get; set; }
        public Guid idAtencion { get; set; }
        public Guid idNuc { get; set; }
        public string nuc { get; set; }
        public string nombreMP { get; set; }
        public bool status { get; set; }

        public string aviso { get; set; }
        public Guid idDistrito { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public string DireccionSuceso { get; set; }

    }
}
