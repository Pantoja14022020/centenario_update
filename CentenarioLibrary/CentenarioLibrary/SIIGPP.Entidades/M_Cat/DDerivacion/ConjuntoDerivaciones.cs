using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using SIIGPP.Entidades.M_JR.RSesion;
using System.Text;
using SIIGPP.Entidades.M_JR.REnvio;

namespace SIIGPP.Entidades.M_Cat.DDerivacion
{
    public class ConjuntoDerivaciones
    {
        //Nueva tabla de conjuntos
        public Guid IdConjuntoDerivaciones { get; set; }
        public Envio Envio { get; set; }
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

    }
}
