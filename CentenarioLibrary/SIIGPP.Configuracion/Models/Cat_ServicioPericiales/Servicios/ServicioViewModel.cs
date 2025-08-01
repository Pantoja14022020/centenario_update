using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_ServicioPericiales.Servicios
{
    public class ServicioViewModel
    {
        public Guid IdServicioPericial { get; set; }
        public string Codigo { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public Boolean AtencionVictimas { get; set; }
        public Boolean CancelableporJR { get; set; }
        public string Materia { get; set; }
    }
}
