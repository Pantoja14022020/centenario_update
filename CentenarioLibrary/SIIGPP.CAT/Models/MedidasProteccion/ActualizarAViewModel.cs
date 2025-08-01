using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.MedidasProteccion
{
    public class ActualizarAViewModel
    {
        public Guid IdMProteccion { get; set; }
        public Boolean Ampliacion { get; set; }
        public string FAmpliacion { get; set; }
        public string FterminoAm { get; set; }
        public string Ratificacion { get; set; }
    }
}
