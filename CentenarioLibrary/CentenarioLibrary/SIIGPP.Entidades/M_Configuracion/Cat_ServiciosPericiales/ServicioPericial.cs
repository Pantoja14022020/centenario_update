using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales
{
    public  class ServicioPericial
    {
        public Guid IdServicioPericial { get; set; }
        public string Codigo { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public string EnMateriaDe { get; set; }
        public Boolean AtencionVictimas { get; set; } 
        public Boolean CancelableporJR { get; set; }
        public List<ASP> ASPs { get; set; }


    }
}
