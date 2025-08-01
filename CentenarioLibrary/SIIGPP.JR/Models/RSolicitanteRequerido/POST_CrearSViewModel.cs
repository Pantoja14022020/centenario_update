using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSolicitanteRequerido
{
    public class POST_CrearSViewModel
    {
        //Crea los solicitantes, requeridos y delitos con su respectivo conjunto al que pertenece, todos del mismo envioid

        public Guid envioId { get; set; }
        public Guid personaId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public string SolicitantesC { get; set; }
        public string RequeridosC { get; set; }
        public string Tipo { get; set; }
        public string ClasificacionS { get; set; }
        public string ClasificacionR { get; set; }
        public string DelitosC { get; set; }

    }
    public class PUT_CrearSViewModel
    {

        public Guid IdRSolicitanteRequerido { get; set; }
        public Guid envioId { get; set; }
        public Guid personaId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public string SolicitantesC { get; set; }
        public string RequeridosC { get; set; }
        public string Tipo { get; set; }
        public string ClasificacionS { get; set; }
        public string ClasificacionR { get; set; }
        public string DelitosC { get; set; }

    }
}
