using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_JR.RCitatorioRecordatorio;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RSesion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_JR.RSesion
{
    public class SesionConjunto
    {
        [Key]
        public Guid IdSC { get; set; }
        public Guid SesionId { get; set; }
        public Sesion Sesion { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public ConjuntoDerivaciones Conjunto { get; set; }
    }
}
