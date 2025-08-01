using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Delito
{
   public class Delito
    {

        public Guid IdDelito { get; set; }
        public string CveDelito { get; set; }
        public string Nombre { get; set; }
        
        public Boolean SuceptibleMASC { get; set; }
        public Boolean TipoMontoRobo { get; set; }
        public Boolean AltoImpacto { get; set; }
        public string OfiNoOfi { get; set; }
   



    }
}
