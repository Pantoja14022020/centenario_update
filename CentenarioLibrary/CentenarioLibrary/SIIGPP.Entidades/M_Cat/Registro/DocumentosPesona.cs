using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Registro
{
   public class DocumentosPesona
    {
        public Guid IdDocumentoPersona { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }
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
