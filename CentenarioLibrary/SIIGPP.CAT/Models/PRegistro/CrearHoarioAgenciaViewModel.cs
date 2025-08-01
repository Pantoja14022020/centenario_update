using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PRegistro
{
    public class CrearHoarioAgenciaViewModel
    {
        public Guid distritoId { get; set; }
        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFinal { get; set; }
        public Guid AgenciaId { get; set; }
        public int densidadPorHora { get; set; }
    }
}
