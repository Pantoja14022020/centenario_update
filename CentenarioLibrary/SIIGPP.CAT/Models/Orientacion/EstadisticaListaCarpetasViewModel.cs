using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class EstadisticaListaCarpetasViewModel
    {
        public Guid RHechoId { get; set; }
        public Guid RAtencionId { get; set; }
        public string nuc { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public string DistritoInicial { get; set; }
        public string DspInicial { get; set; }
        public string AgenciaInicial { get; set; }
        public string EtapaCarpeta { get; set; }
        public string StatusCarpeta { get; set; }
        public int Tipo { get; set; }
        public DateTime Fechah { get; set; }



        public string Delito { get; set; }
        public string DelitoEspecifico { get; set; }
        public string ModalidadesDelito { get; set; }
        public string GradoEjecucion { get; set; }
        public string TipoRobado { get; set; }
        public string ConAlgunaParteCuerpo { get; set; }
        public string ViolenciaSinViolencia { get; set; }
        public string ConotroElemento { get; set; }
        public string ArmaFuego { get; set; }
        public string ArmaBlanca { get; set; }

        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public string Colonia { get; set; } 

        public int Victimas { get; set; }
        public int Imputados { get; set; }
    }
}
