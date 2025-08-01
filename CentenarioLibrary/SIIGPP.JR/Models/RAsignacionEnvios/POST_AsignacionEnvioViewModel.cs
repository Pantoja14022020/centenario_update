using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 
 namespace SIIGPP.JR.Models.RAsignacionEnvios
 {
    public class POST_AsignacionEnvioViewModel
    {
        public Guid ModuloServicioId { get; set; } 
        public Guid EnvioId { get; set; }


        public List<POST_AsignacionEnvioViewModel> Asignaciones { get; set; }

        public List<POST_AsignacionEnvioViewModel> ModuloServicioIdFacilitadorNotificador { get; set; }
    }

    public class AsignacionFacilitadorNotificador
    {
        public Guid ModuloServicioId { get; set; }
        public Guid EnvioId { get; set; }
        public List<AsignacionFacilitadorNotificador> ModuloServicioIdFacilitadorNotificador { get; set; }
    }
 }