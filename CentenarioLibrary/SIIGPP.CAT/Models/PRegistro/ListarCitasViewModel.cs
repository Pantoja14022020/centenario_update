using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PRegistro
{
    public class ListarCitasViewModel
    {
        public Guid PreRegistroId { get; set; }
        public Guid IdCita { get; set; }
        public Guid IdPersona { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string telefono { get; set; }
        public int edad { get; set; }
        public string RBreve { get; set; }
        public DateTime fechaSuceso { get; set; }

    }
}
