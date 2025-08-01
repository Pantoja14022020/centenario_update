using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.MedidasProteccion
{
    public class ActualizarNViewModel
    {
        public Guid IdMProteccion { get; set; }
        public string Destinatarion { get; set; }
        public string Domicilion { get; set; }
        public Boolean Detalleactivo { get; set; }
        public string Textofinaldetalle { get; set; }
        public string NumeroOficioN { get; set; }
    }
}
