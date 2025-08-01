using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Citatorio_Notificacion
{
    public class CrearViewModel
    {     

        public Guid RHechoId { get; set; }

        public string NombrePersona { get; set; }

        public string DomicilioPersona { get; set; }

        public string ReferenciaPersona { get; set; }

        public string TelefonoPersona { get; set; }

        public string LugarCita { get; set; }

        public string FechaCita { get; set; }
        public string Hora { get; set; }

        public string Nuc { get; set; }

        public string Delito { get; set; }

        public string Descripcion { get; set; }

        public string FechaReporte { get; set; }

        public DateTime FechaSis { get; set; }

        public Boolean Tipo { get; set; }

        public string Distrito { get; set; }

        public string Subproc { get; set; }

        public string Agencia { get; set; }

        public string Usuario { get; set; }

        public string Puesto { get; set; }
        public string Modulo { get; set; }
        public string Textofinal { get; set; }
        public string NumeroOficio { get; set; }

    }
}
