using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Desgloses
{
    public class CrearHDesgloseViewModel
    {
        public Guid IdDesgloses { get; set; }
        public Guid RHechoId { get; set; }
        public string nucg { get; set; }
        public string DistritoEnvia { get; set; }
        public string DirSubEnvia { get; set; }
        public string AgenciaEnvia { get; set; }
        public Guid ModuloServicioIdEnvia { get; set; }
        public string DistritoRecibe { get; set; }
        public string DirSubRecibe { get; set; }
        public string AgenciaRecibe { get; set; }
        public string ModuloRecibe { get; set; }
        public Guid ModuloServicioIdRecibe { get; set; }
        public string PersonaIdDesglosadas { get; set; }
        public string RDHIdDesglosados { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaDesglose { get; set; }
        public string UsuarioEnvia { get; set; }
        public string PuestoEnvia { get; set; }


    }
}
