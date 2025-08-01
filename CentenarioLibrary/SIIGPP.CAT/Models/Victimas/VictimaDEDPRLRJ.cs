using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class VictimaDEDPRLRJ
    {
        public Guid PersonaId { get; set; }
        public string NombreVictima { get; set; }
        public string DireccionE { get; set; }
        public string DireccionP { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Telefono { get; set; }
        public string Alias { get; set; }
        public string Correo { get; set; }
        public string NombreRepresentanteLegal { get; set; }
        public string TelefonoReL { get; set; }
        public string CorreoReL { get; set; }
        public string NombreRepresentanteJuridico { get; set; }
        public string TelefonoReJ { get; set; }
        public string CorreoReJ { get; set; }
        public Boolean RepresentanteActivo { get; set; }
        public string DireccionRepresentante { get; set;}
    }
}
