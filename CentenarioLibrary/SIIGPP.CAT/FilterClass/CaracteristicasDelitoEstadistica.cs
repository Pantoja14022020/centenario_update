using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class CaracteristicasDelitoEstadistica
    {
        public Guid DelitoNombre { get; set; }
        public string Delitoespecifico { get; set; }
        public string Tipofuero { get; set; }
        public string Requisitoprocedibilidad { get; set; }
        public string Gradoejecucion { get; set; }
        public string Prisionpreventiva { get; set; }
        public string Formacomision { get; set; }
        public string ViolenciaSinViolencia { get; set; }
        public string Concurso { get; set; }
        public string Modalidaddelito { get; set; }
        public string ClasificaOrdenResult { get; set; }
        public Decimal Montorobado { get; set; }
        public string Descripcionrobado { get; set; }
        public string Armablanca { get; set; }
        public string Armafuego { get; set; }
        public string ConOtroElemento { get; set; }
        public string ConAlgunaParteCuerpo { get; set; }
        public Boolean FiltroActivoDelito { get; set; }
        public Boolean Delitoactivo { get; set; }
    }
}
