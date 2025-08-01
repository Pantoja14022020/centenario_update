using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RExpediente
{
    public class ListaExpedienteViewModel
    {
        public Guid IdExpediente { get; set; }
        public Guid RHechoId { get; set; }
        public string NoExpediente { get; set; }
        public string Sede { get; set; }
        public int Año { get; set; }
        public DateTime FechaDerivacion { get; set; }
        public string StatusAcepRech { get; set; }
    }
}
