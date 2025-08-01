using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PRegistro
{
    public class CrearCitasViewModel
    {
        public Guid PRegistroId { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public Guid AgenciaId { get; set; }
        public Guid distritoId { get; set; }       
    }
}
