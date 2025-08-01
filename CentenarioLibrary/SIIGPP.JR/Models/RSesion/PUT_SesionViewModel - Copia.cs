using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSesion
{
    public class PATCH_SesionViewModel
    {
        public Guid IdSesion { get; set; } 
        public string StatusSesion { get; set; }
        public string DescripcionSesion { get; set; }
        public string Asunto { get; set; }

    }
}
