using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Medidas.MedidasCautelares
{
    public class ActualizarViewModel
    {
        public Guid IdMedidasCautelaresC { get; set; }
        public string Clave { get; set; }
        public string Clasificacion { get; set; }
        public string Descripcion { get; set; }
    }
}
