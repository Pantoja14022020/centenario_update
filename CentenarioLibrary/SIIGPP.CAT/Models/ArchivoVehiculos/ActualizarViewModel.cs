using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.ArchivoVehiculos
{
    public class ActualizarViewModel
    {
        public Guid IdArchivoVehiculos { get; set; }
        public Guid VehiculoId { get; set; }
        public string TipoDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public string Ruta { get; set; }
    }
}
