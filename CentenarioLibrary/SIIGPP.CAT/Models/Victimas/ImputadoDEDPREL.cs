using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class ImputadoDEDPREL
    { 
        public Guid PersonaId { get; set; }
        public string NombreImputado { get; set; }
        public string DireccionE { get; set; }
        public string DireccionP { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Telefono { get; set; }
        public string Alias { get; set; }
        public string Correo { get; set; }
        public string NombreRepresentanteParticular { get; set; }
        public string TelefonoRe { get; set; }    
        public string CorreoRe { get; set; }
        public Boolean RepresentanteActivo { get; set; }
        public string DireccionRepresentante { get; set; }
    }
}
