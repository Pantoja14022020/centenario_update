using SIIGPP.JR.Models.RDelito;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SIIGPP.JR.Models.RSesion
{
    public class GET_ConjuntosViewModel
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

        /*
        Estos view model ya no se ocupan
        public List<POST_CrearRViewModel> Requeridos { get; set; }
        public List<POST_CrearDelitoViewModel> Delitos { get; set; }
        */



    }

    public class GET_ConjuntosViewModel1
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
        //SR
        public Guid IdRSolicitanteRequerido { get; set; }
        public Guid IdPersona { get; set; }
        public string Tipo { get; set; }
        public string NombreCompleto { get; set; }
        public Boolean SEC { get; set; }
        public string Telefono { get; set; }

        /*
         * 
        Estos view model ya no se ocupan  NombreDelito
        public List<POST_CrearRViewModel> Requeridos { get; set; }
        public List<POST_CrearDelitoViewModel> Delitos { get; set; }
        */
    }
    public class GET_ConjuntosViewModel2
    {
        public Guid IdDelitoDerivado { get; set; }
        public Guid EnvioId { get; set; }
        public Guid RDHId { get; set; }
        public Guid DelitoId { get; set; }
        public string Nombre { get; set; }
    }
}
