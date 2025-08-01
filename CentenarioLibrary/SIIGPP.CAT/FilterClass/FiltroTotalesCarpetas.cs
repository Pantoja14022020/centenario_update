using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class FiltroTotalesCarpetas
    {
        //DATOS GENERALES

        [FromQuery(Name = "a")]
        public Guid Distrito { get; set; }
        [FromQuery(Name = "b")]
        public Boolean Distritoact  { get; set;}
        [FromQuery(Name = "c")]
        public Guid Dsp { get; set; }
        [FromQuery(Name = "d")]
        public Boolean Dspact { get; set; }
        [FromQuery(Name = "e")]
        public Guid Agencia { get; set; }
        [FromQuery(Name = "f")]
        public Boolean Agenciaact { get; set; }
        [FromQuery(Name = "g")]
        public DateTime Fechadesde { get; set; }
        [FromQuery(Name = "h")]
        public DateTime Fechahasta { get; set; }

        //ETAPA ACTUAL

        [FromQuery(Name = "i")]
        public string EtapaActual { get; set; }
        [FromQuery(Name = "j")]
        public string StatusActual { get; set; }


        //ETAPA HISTORICO

        [FromQuery(Name = "k")]
        public string EtapaHistorico { get; set; }
        [FromQuery(Name = "l")]
        public string StatusHistorico { get; set; }
        [FromQuery(Name = "m")]
        public Boolean FiltroActivoStatusHistorico { get; set; }

        //TIPO DE INICIO DE LA CARPETA

        [FromQuery(Name = "n")]
        public string Tipoinicio { get; set; }
        [FromQuery(Name = "o")]
        public string Mediollegada { get; set; }

        //DATOS DEL IMPUTADO-DETENIDO

        [FromQuery(Name = "p")]
        public string Cumplerequisitos { get; set; }
        [FromQuery(Name = "q")]
        public string Decretolibertad { get; set; }
        [FromQuery(Name = "r")]
        public string Dispusolibertad { get; set; }
        [FromQuery(Name = "s")]
        public Boolean Imputadoactivofiltro { get; set; }

        //INFORMACION DEL DELITO

        [FromQuery(Name = "t")]
        public Guid DelitoNombre { get; set; }
        [FromQuery(Name = "u")]
        public string Delitoespecifico { get; set; }
        [FromQuery(Name = "v")]
        public string Tipofuero { get; set; }
        [FromQuery(Name = "w")]
        public string Requisitoprocedibilidad { get; set; }
        [FromQuery(Name = "x")]
        public string Gradoejecucion { get; set; }
        [FromQuery(Name = "y")]
        public string Prisionpreventiva { get; set; }
        [FromQuery(Name = "z")]
        public string Formacomision { get; set; }
        [FromQuery(Name = "aa")]
        public string ViolenciaSinViolencia { get; set; }
        [FromQuery(Name = "ab")]
        public string Concurso { get; set; }
        [FromQuery(Name = "ac")]
        public string Modalidaddelito { get; set; }
        [FromQuery(Name = "ad")]
        public string ClasificaOrdenResult { get; set; }
        [FromQuery(Name = "ae")]
        public Decimal Montorobado { get; set; }
        [FromQuery(Name = "af")]
        public string Descripcionrobado { get; set; }
        [FromQuery(Name = "ag")]
        public string Armablanca { get; set; }
        [FromQuery(Name = "ah")]
        public string Armafuego { get; set; }
        [FromQuery(Name = "ai")]
        public string ConOtroElemento { get; set; }
        [FromQuery(Name = "aj")]
        public string ConAlgunaParteCuerpo { get; set; }
        [FromQuery(Name = "ak")]
        public Boolean FiltroActivoDelito { get; set; }
        [FromQuery(Name = "al")]
        public Boolean Delitoactivo { get; set; }

    }
}
