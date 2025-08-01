using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System.ComponentModel.DataAnnotations;
using System;

namespace SIIGPP.Entidades.M_JR.RSolicitanteRequerido
{
    public class SolicitanteRequerido
    {
        [Key]
        public Guid IdRSolicitanteRequerido { get; set; }
        public Guid EnvioId { get; set; }
        public Envio Envio { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string Tipo { get; set; }
        public string Clasificacion { get; set; }
 
    }
    public class SolicitanteRequerido1
    {
        [Key]
        public Guid IdRSolicitanteRequerido { get; set; }
        public Guid EnvioId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string Tipo { get; set; }
        public string Clasificacion { get; set; }
        public DelitoDerivado delitoderivado { get; set; }
        public Guid RDHId { get; set; }
        public RDH rdh { get; set; }
        public Delito delito { get; set; }

    }


}
