using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ListarMPMesaViewModel
    {
        public Guid IdRHecho { get; set; }
        public string Mp { get; set; }
        public string Modulo { get; set; }
        public Guid RAtencionId { get; set; }
        public string RBreve { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public string NDenunciaOficio { get; set; }
        public Guid DistritoId { get; set; }
        public Guid IdModuloFusion { get; set; }
        public Guid IdAgenciaFusion { get; set; }
    }
    public class ListarDistritoOrigen
    {
        public Guid DistritoId { get; set; }
    }
    public class ListarDistritoOrigenRenvio
    {
        public Guid DistritoId { get; set; }
        public Guid AgenciaId { get; set; }
    }
}
