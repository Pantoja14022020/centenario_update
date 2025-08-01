using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RemisionesUI
{
    public class ActualizarViewModel
    {
        public Guid IdRemisionUI { get; set; }
        public string Status { get; set; }
        public string Rechazo { get; set; }
        public DateTime FechaRechazo { get; set; }

    }

    public class ActualizarViewModelDistrito
    {
        public Guid IdRemisionUI { get; set; }
        public string Status { get; set; }
        public string Rechazo { get; set; }
        public DateTime FechaRechazo { get; set; }
        public Guid distritoId { get; set; }

    }

    public class ActualizarViewModelReenviar
    {
        public Guid IdRemisionUI { get; set; }
        public string Status { get; set; }
        public string Rechazo { get; set; }
        public DateTime FechaRechazo { get; set; }
        public Boolean EnvioExitosoTF { get; set; }

    }
}
