using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ComprobarRemision
    {
        public Guid RAtencionId { get; set; }
        public Guid RHechoId { get; set; }

        // RHECHO
        //***********************************************************
        public Guid Agenciaid { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Boolean Status { get; set; }
        public Guid? nucId { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public string RBreve { get; set; }



    }
}
