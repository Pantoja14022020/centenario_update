using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DocumentosPersonas
{
    public class DocumentosPersonasViewModel
    {
        public Guid IdDocumentoPersona { get; set; }
        public Guid PersonaId { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Ruta { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
    }
}
