using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Vehiculos.Modelo
{
    public class ActualizarViewModel
    {
        public Guid IdModelo { get; set; }
        public string Dato { get; set; }
        public Guid MarcaId { get; set; }
    }

}
