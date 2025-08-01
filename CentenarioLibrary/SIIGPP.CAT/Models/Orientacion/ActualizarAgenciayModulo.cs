using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ActualizarAgenciayModulo
    {

        public Guid agenciaId { get; set; }
        public Guid moduloServicioId { get; set; }
        public Guid IdRHecho { get; set; }

    }
    public class ActualizarAgenciayModuloEntreDistritos
    {

        public Guid agenciaId { get; set; }
        public Guid moduloServicioId { get; set; }
        public Guid IdRHecho { get; set; }
        public Guid distritoId { get; set; }

    }
}
