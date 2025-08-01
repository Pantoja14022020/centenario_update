using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RExpediente;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_JR.RAsignacionEnvios
{
    public class AsignacionEnvio
    {        
        public Guid IdAsingacionEnvio { get; set; }
        public Guid ModuloServicioId { get; set; }
        public ModuloServicio ModuloServicio { get; set; } 
        public Guid EnvioId { get; set; } 
        public Envio Envio { get; set; }

    }
}
