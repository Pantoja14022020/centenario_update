using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_JuzgadosAgencias
{
    public class CrearViewModel
    {
        
        public Guid DistritoId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Encargado { get; set; }
    }
}
