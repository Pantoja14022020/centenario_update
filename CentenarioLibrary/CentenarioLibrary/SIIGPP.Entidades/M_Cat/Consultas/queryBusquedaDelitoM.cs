using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_Cat.Consultas
{
    public class queryBusquedaDelitoM
    {
        [Key]
        public Guid IdRDH { get; set; }
        /********************************************************/
        public Guid DelitoId { get; set; }
        public string Nombre { get; set; }
        public string OfiNoOfi { get; set; }
        public Boolean AltoImpacto { get; set; }
        public Boolean SuceptibleMASC { get; set; }
        public string Hipotesis { get; set; }


        /********************************************************/
        public Guid RHechoId { get; set; }
        public string TipoFuero { get; set; }
        public string TipoDeclaracion { get; set; }
        public string ResultadoDelito { get; set; }
        public string GraveNoGrave { get; set; }
        public string IntensionDelito { get; set; }
        public string ViolenciaSinViolencia { get; set; }
        public Boolean Equiparado { get; set; }
        public string Tipo { get; set; }
        public string Concurso { get; set; }
        public string ClasificaOrdenResult { get; set; }
        public Boolean ArmaFuego { get; set; }
        public Boolean ArmaBlanca { get; set; }
        public string Observaciones { get; set; }
        public string ConAlgunaParteCuerpo { get; set; }
        public string ConotroElemento { get; set; }
        public string TipoRobado { get; set; }
        public decimal MontoRobado { get; set; }
        public DateTime Fechasys { get; set; }
        public string DelitoEspecifico { get; set; }

    }
}
