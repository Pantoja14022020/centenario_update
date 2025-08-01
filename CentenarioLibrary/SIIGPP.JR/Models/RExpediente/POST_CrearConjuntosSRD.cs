using SIIGPP.JR.Models.RDelito;
using SIIGPP.JR.Models.RSolicitanteRequerido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RExpediente
{
    public class POST_CrearConjuntosSRD
    {

        // CREA LOS CONJUNTOS DE SOLICITANTES, REQUERIDOS Y DELITOS

        // CONJUNTOS

        public Guid IdConjuntoDerivaciones { get; set; }
        public Guid EnvioId { get; set; }
        public string SolicitadosC { get; set; }
        public string RequeridosC { get; set; }
        public string DelitosC { get; set; }
        public string NombreS { get; set; }
        public string DireccionS { get; set; }
        public string TelefonoS { get; set; }
        public string ClasificacionS { get; set; }
        public string NombreR { get; set; }
        public string DireccionR { get; set; }
        public string TelefonoR { get; set; }
        public string ClasificacionR { get; set; }
        public string NombreD { get; set; }
        public string NoOficio { get; set; }
        public string ResponsableJR { get; set; }
        public string NombreSubDirigido { get; set; }
        public string Validacion { get; set; }

        public string EspontaneoNoEspontaneo { get; set; }        
    }
}
