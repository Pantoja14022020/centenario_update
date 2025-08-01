using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class CrearRAPViewModel
    {
        //REGISTRO DE ATENCION

        public string DistritoInicial { get; set; }
        public string DirSubProcu { get; set; }
        public string AgenciaInicial { get; set; }
        public Guid agenciaId { get; set; } 
        public Guid racid { get; set; } 
        //REGISTRO DE RAPs 
        public Guid PersonaId { get; set; }
        public string ClasificacionPersona { get; set; }
        public Boolean PInicio { get; set; }
        //TURNO
        public string Modulo { get; set; }

    }
}
